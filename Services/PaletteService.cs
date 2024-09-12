﻿using COLOR.Data.Repository;
using COLOR.Data.Validation;
using COLOR.Domain.Etities;
using COLOR.DTOs;
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

    public async Task CreatePalette(string name, CancellationToken ct)
    {
        try
        {
            var validate = new PaletteValidation();
            var palette = await _repository.CreatePalette(name, ct);

            var validateResult = await validate.ValidateAsync(palette, ct);
            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                    Console.WriteLine($"Property: {error.PropertyName}; Error: {error.ErrorMessage}");

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