using COLOR.Domain.Etities;
using COLOR.DTOs;

namespace COLOR.Data.Repository;

public interface IPaletteRepository
{
    public Task CreatePalette(string name, CancellationToken ct);
    public Task<List<GetAllPalettesDto>> GetAllPalettes(CancellationToken ct);
    public Task<PaletteEntity> FindPaletteByName(string name, CancellationToken ct);
    public Task<PaletteEntity> FindPaletteById(Guid id, CancellationToken ct);
    public Task AddColorToPalette(ColorEntity color, PaletteEntity palette, CancellationToken ct);

}