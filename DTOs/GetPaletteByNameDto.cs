namespace COLOR.DTOs;

public record GetPaletteByNameDto(Guid Id, string Name, ICollection<byte[]> Colors);