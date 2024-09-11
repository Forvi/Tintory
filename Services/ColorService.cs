using COLOR.Data;
using COLOR.Data.Repository;
using COLOR.Domain.Etities;

namespace COLOR.Services;

public class ColorService : IColorService
{
    private readonly IColorRepository _colorRepository;
    private readonly IPaletteRepository _paletteRepository;
    private readonly ILogger<ColorService> _logger;

    public ColorService(IColorRepository colorRepository, IPaletteRepository paletteRepository, ILogger<ColorService> logger)
    {
        _colorRepository = colorRepository;
        _logger = logger;
        _paletteRepository = paletteRepository;
    }

    public string ColorGenerate()
    {
        var generator = new ColorGenerateService();
        var hex = generator.ColorGenerate();
        return hex;
    }
    
    public async Task AddColorToPalette(string hexCode, Guid paletteId, CancellationToken ct)
    {
        try
        {
            var palette = await _paletteRepository.FindPaletteById(paletteId, ct);
            var color = await _colorRepository.FindExistingColor(hexCode, ct);
            if (color == null)
            {
                var newColor = await _colorRepository.CreateColor(hexCode, ct);
                await _paletteRepository.AddColorToPalette(newColor, palette, ct);
                return;
            }

            await _paletteRepository.AddColorToPalette(color, palette, ct);
            return;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Operation was cancelled");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "AddColorToPalette error");
            throw;
        }
    }
}