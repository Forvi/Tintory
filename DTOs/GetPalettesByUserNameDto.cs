using COLOR.Domain.Entities;

namespace COLOR.DTOs;

public record GetPalettesByUserNameDto(IList<GetColorInPaletteDto> Colors);