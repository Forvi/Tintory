using COLOR.Domain.Entities;

namespace COLOR.DTOs;

public record NamePaletteDto(string Name, IList<ColorEntity> Colors);