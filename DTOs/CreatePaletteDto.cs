namespace COLOR.DTOs;

public record CreatePaletteDto(Guid? Id, string Name, ICollection<string> Colors);