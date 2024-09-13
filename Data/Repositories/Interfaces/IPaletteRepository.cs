using COLOR.Domain.Entities;
using COLOR.DTOs;

namespace COLOR.Data.Repositories.Interfaces;

public interface IPaletteRepository
{
    public Task<PaletteEntity> CreatePalette(string name, Guid userId, CancellationToken ct);
    public Task<List<GetAllPalettesDto>> GetAllPalettes(CancellationToken ct);
    public Task<PaletteEntity> FindPaletteByName(string name, CancellationToken ct);
    public Task<PaletteEntity> FindPaletteById(Guid id, CancellationToken ct);
    public Task AddColorToPalette(ColorEntity color, PaletteEntity palette, CancellationToken ct);

}