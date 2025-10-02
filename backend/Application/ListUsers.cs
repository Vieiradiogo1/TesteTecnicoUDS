using Domain;
public class ListUsers
{
    private readonly AppDbContext _db;

    public ListUsers(AppDbContext db)
    {
        _db = db;
    }

    public List<User> Handle()
    {
        return _db.Users.ToList();
    }
}