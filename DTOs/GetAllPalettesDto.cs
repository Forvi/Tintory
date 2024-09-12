namespace COLOR.DTOs;

public record GetAllPalettesDto(Guid Id, string Name, IEnumerable<GetColorInPaletteDto> Colors);