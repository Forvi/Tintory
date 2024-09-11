using COLOR.Data.Repository;
using COLOR.Domain.Etities;
using COLOR.DTOs;

namespace COLOR.Services;

public class PaletteService : IPaletteService
{
    private readonly IPaletteRepository _repository;
    private readonly ILogger<PaletteService> _logger;

    public PaletteService(IPaletteRepository repository, ILogger<PaletteService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreatePalette(string name, CancellationToken ct)
    {
        try
        {
            await _repository.CreatePalette(name, ct);
            return;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            throw;
        }
    }

    public async Task<List<GetAllPalettesDto>> GetAllPalettes(CancellationToken ct)
    {
        ColorGenerateService generator = new ColorGenerateService();
        
        try
        {
            var palettes = await _repository.GetAllPalettes(ct);
            if (palettes.Count == 0)
                _logger.LogError("No palettes found");

            // foreach (var palette in palettes)
            //     palette.Colors = palette.Colors.Select(color => generator.ConvertBytesToHex(color)).ToList();
            return palettes;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            throw;
        }
    }
}