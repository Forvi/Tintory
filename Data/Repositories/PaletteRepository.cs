using COLOR.Data.Repositories.Interfaces;
using COLOR.Domain.Entities;
using COLOR.DTOs;
using Microsoft.EntityFrameworkCore;

namespace COLOR.Data.Repositories;

public class PaletteRepository : IPaletteRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<PaletteRepository> _logger;

    public PaletteRepository(AppDbContext dbContext, ILogger<PaletteRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PaletteEntity> CreatePalette(string name, Guid userId, CancellationToken ct)
    {
        try
        {
            var palette = new PaletteEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                UserId = userId
            };
            
            await _dbContext.AddAsync(palette, ct);
            await _dbContext.SaveChangesAsync(ct);
            return palette;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Operation was cancelled");
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating palette");
            throw;
        }
    }

    public async Task<List<GetAllPalettesDto>> GetAllPalettes(CancellationToken ct)
    {
        try
        {
            var palettes = await _dbContext.PaletteEntities
                .Select(p => 
                    new GetAllPalettesDto(
                    p.Id, 
                    p.Name, 
                    p.Colors.Select(c => new GetColorInPaletteDto(c.Id, c.HexCode))))
                .ToListAsync(ct);
            
            return palettes;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Operation was cancelled");
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving all palettes");
            throw;
        }
    }

    public async Task<PaletteEntity> FindPaletteByName(string name, CancellationToken ct)
    {
        try
        {
            var palette = await _dbContext.PaletteEntities
                .Include(p => p.Colors)
                .FirstOrDefaultAsync(p => p.Name == name, ct);

            if (palette == null)
                throw new InvalidOperationException("Palette not found");
            
            return palette;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Operation was cancelled");
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while finding the palette by name");
            throw;
        }
    }
    
    public async Task<PaletteEntity> FindPaletteById(Guid id, CancellationToken ct)
    {
        try
        {
            var palette = await _dbContext.PaletteEntities
                .Include(p => p.Colors)
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (palette == null)
                throw new ArgumentNullException();
            
            return palette;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Operation was cancelled");
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "FindPaletteById error");
            throw;
        }
    }

    public async Task AddColorToPalette(ColorEntity color, PaletteEntity palette, CancellationToken ct)
    {
        if (color == null) 
            throw new ArgumentNullException(nameof(color), "Color entity cannot be null");

        if (palette == null) 
            throw new ArgumentNullException(nameof(palette), "Palette entity cannot be null");
        
        palette.Colors.Add(color);
        await _dbContext.SaveChangesAsync(ct);
        return;
    }
}