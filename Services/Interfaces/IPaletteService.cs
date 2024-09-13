using COLOR.DTOs;

namespace COLOR.Services.Interfaces;

public interface IPaletteService
{
    public Task CreatePalette(string name, Guid userId, CancellationToken ct);
    public Task<List<GetAllPalettesDto>> GetAllPalettes(CancellationToken ct);


}