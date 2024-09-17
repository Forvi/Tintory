using COLOR.Domain.Entities;

namespace COLOR.DTOs;

public record GetAllPalettesDto(Guid Id, string Name, string UserName, IEnumerable<GetColorInPaletteDto> Colors);