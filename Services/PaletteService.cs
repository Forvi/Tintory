using COLOR.Data.Repositories.Interfaces;
using COLOR.Data.Validation;
using COLOR.DTOs;
using COLOR.Services.Interfaces;
using FluentValidation;

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

    public async Task CreatePalette(string name, Guid userId, CancellationToken ct)
    {
        try
        {
            var validate = new PaletteValidation();
            var palette = await _repository.CreatePalette(name, userId, ct);

            var validateResult = await validate.ValidateAsync(palette, ct);
            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                    _logger.LogError($"Property: {error.PropertyName}; Error: {error.ErrorMessage}");

                throw new ValidationException("Validation passed");
            }
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
            
            return palettes;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            throw;
        }
    }
}