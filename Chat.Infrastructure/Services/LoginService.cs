using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Users;
using Chat.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Chat.Infrastructure.Services;

public class LoginService : ILoginService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public LoginService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthenticatedUserResponse> TryLogin(LoginRequest model)
    {
        ValidateLoginModel(model);
        
        var user = await _userManager.FindByEmailAsync(model.Email!);

        if (!await _userManager.CheckPasswordAsync(user!, model.Password!))
        {
            return new AuthenticatedUserResponse { JwtToken = new JwtTokenResponse(null, null, false) };
        }
        
        var userRoles = await _userManager.GetRolesAsync(user!);

        var authClaims = new List<Claim>
        {
            new (ClaimTypes.Email, user!.Email!),
            new (CustomClaimTypes.Index, user!.Index.ToString()),
            new (CustomClaimTypes.Id, user!.Id.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString().ToUpper()),
        };

        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var jwtSecurityToken = GetToken(authClaims);

        return new AuthenticatedUserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            JwtToken = new JwtTokenResponse(Token: new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ValidTo: jwtSecurityToken.ValidTo, Success: true)
        };
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest model, bool retry = true, int retries = 10, int trie = 0)
    {
        ValidateRegistrationModel(model);
        
        User user = new()
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (result.Succeeded)
        {
            return new RegistrationResponse(default, null) { Success = true, Message = "User created successfully!" };
        }
        
        if (retry && trie < retries)
        {
            return await Register(model, retry, retries, trie + 1);
        }
        
        return new RegistrationResponse(default, null) { Success = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) };

    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var secret = _configuration["JWT:Secret"]!;
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"]?.Split(";").FirstOrDefault(), //_httpContextAccessor.HttpContext?.Request.GetBaseUrl()?.TrimEnd('/') ?? _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private void ValidateLoginModel(LoginRequest model)
    {
        if (model is null)
        {
            throw new ArgumentException($"{nameof(model)} can't be null");
        }        
        
        if (model.Email is null)
        {
            throw new ArgumentException($"{nameof(model.Email)} can't be null");
        }
        
        if (model.Password is null)
        {
            throw new ArgumentException($"{nameof(model.Password)} can't be null");
        }
    }
    
    private void ValidateRegistrationModel(RegistrationRequest model)
    {
        if (model is null)
        {
            throw new ArgumentException($"{nameof(model)} can't be null");
        }
    }
}