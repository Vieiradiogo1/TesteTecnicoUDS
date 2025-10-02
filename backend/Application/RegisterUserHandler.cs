using Domain;

public class RegisterUserHandler
{
    private readonly AppDbContext _db;

    public RegisterUserHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User> Handle(RegisterUser command)
    {
        if (_db.Users.Any(u => u.Email == command.Email))
            throw new Exception("E-mail já cadastrado.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Email = command.Email,
            Passkey = command.Passkey
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}