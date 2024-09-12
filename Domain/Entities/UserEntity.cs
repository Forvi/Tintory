namespace COLOR.Domain.Entities;

public class UserEntity
{
    public UserEntity(Guid id, string name, string passwordHash, string email)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
        Email = email;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public IList<PaletteEntity> Palettes { get; set; }

    public static UserEntity Create(Guid id, string name, string passwordHash, string email)
    {
        return new UserEntity(id, name, passwordHash, email);
    }
}