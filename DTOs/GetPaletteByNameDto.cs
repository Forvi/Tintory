using COLOR.Domain.Entities;

namespace COLOR.DTOs;

public record GetPaletteByNameDto(Guid Id, string Name, IList<ColorEntity> Colors);