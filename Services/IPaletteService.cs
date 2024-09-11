using COLOR.Domain.Etities;
using COLOR.DTOs;

namespace COLOR.Services;

public interface IPaletteService
{
    public Task CreatePalette(string name, CancellationToken ct);
    public Task<List<GetAllPalettesDto>> GetAllPalettes(CancellationToken ct);


}