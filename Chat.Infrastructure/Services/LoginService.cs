using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Services;
using Chat.Domain.User;
using Chat.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Chat.Infrastructure.Services;

public class LoginService : ILoginService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public LoginService(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    public async Task<(bool succes, JwtSecurityToken? token)> TryLogin(LoginRequest model)
    {
        ValidateLoginModel(model);
        
        var user = await _userManager.FindByEmailAsync(model.Email!);

        if (!await _userManager.CheckPasswordAsync(user!, model.Password!))
        {
            return (false, null);
        }
        
        var userRoles = await _userManager.GetRolesAsync(user!);

        var authClaims = new List<Claim>
        {
            new (ClaimTypes.Email, user!.Email!),
            new (CustomClaimTypes.Index, user!.Index.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString().ToUpper()),
        };

        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var jwtSecurityToken = GetToken(authClaims);

        return (true, jwtSecurityToken);
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest model, bool retry = true, int retries = 10, int trie = 0)
    {
        ValidateRegistrationModel(model);
        
        User user = new()
        {
            UserName = model.Email, //$"{model.FirstName?.ReplaceWhitespace()}{model.LastName?.ReplaceWhitespace()}@{RandomGenerator.FourDigits()}",
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (result.Succeeded)
        {
            return new RegistrationResponse { Success = true, Message = "User created successfully!" };
        }
        
        if (retry && trie < retries)
        {
            return await Register(model, retry, retries, trie + 1);
        }
        
        return new RegistrationResponse
            { Success = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) };

    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

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