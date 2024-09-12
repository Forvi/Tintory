namespace COLOR.Domain.Entities;

public class PaletteEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<ColorEntity> Colors { get; set; } = new List<ColorEntity>();
    public UserEntity User { get; set; }
    public Guid UserId { get; set; }
}