﻿using COLOR.Domain.Etities;
using Microsoft.EntityFrameworkCore;

namespace COLOR.Data.Repository;

public class ColorRepository : IColorRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<PaletteRepository> _logger;

    public ColorRepository(AppDbContext dbContext, ILogger<PaletteRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<ColorEntity> CreateColor(string hexCode, CancellationToken ct)
    {
        try
        {
            var color = new ColorEntity
            {
                Id = Guid.NewGuid(),
                HexCode = hexCode,
            };
            
            await _dbContext.ColorEntities.AddAsync(color, ct);
            await _dbContext.SaveChangesAsync(ct);
            return color;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Operation was cancelled");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<ColorEntity?> FindExistingColor(string hexCode, CancellationToken ct)
    {
        var existingColor = await _dbContext.ColorEntities
            .FirstOrDefaultAsync(c => c.HexCode == hexCode, ct);
        
        return existingColor;
    }
}