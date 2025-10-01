using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;
    private readonly ListUsers _listUsersHandler;
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public UserController(
        RegisterUserHandler registerUserHandler,
        ListUsers listUsersHandler,
        AppDbContext db,
        IConfiguration config)
    {
        _registerUserHandler = registerUserHandler;
        _listUsersHandler = listUsersHandler;
        _db = db;
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegisterUser command)
    {
        var user = await _registerUserHandler.Handle(command);
        return Ok(new { user.Id, user.Name, user.Email });
    }

    [HttpGet]
    public IActionResult Get()
    {
        var users = _listUsersHandler.Handle();
        return Ok(users.Select(u => new { u.Id, u.Name, u.Email }));
    }

    // ----------------------

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        // buscador de usuario via email e senha
        var user = _db.Users.FirstOrDefault(u => u.Email == login.Email && u.Passkey == login.Passkey);

        if (user == null)
            return Unauthorized("E-mail ou senha inválidos, tente novamente!");

        var jwtSection = _config.GetSection("JwtSettings");
        var secretKey = jwtSection.GetValue<string>("SecretKey");

        var claims = new[]
        {
            new Claim("codigo_do_usuario", user.Id.ToString()),
            new Claim("apelido", user.Name ?? ""),
            new Claim("email", user.Email ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            mensagem = $"Bem-vindo, {user.Name}!",
            token = tokenString
        });
    }
// Model para o login
public class LoginModel
{
    public string Email { get; set; }
    public string Passkey { get; set; }
}
}
