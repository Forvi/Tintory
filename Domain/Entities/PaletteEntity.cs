namespace COLOR.Domain.Etities;

public class PaletteEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<ColorEntity> Colors { get; set; } = new List<ColorEntity>();
}