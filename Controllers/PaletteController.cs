using System.Security.Claims;
using COLOR.Domain.Entities;
using COLOR.DTOs;
using COLOR.Services;
using COLOR.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COLOR.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaletteController : ControllerBase
{
    private readonly IPaletteService _paletteService;
    private readonly ILogger<PaletteController> _logger;

    public PaletteController(IPaletteService service, ILogger<PaletteController> logger)
    {
        _paletteService = service;
        _logger = logger;
    }
    
    [HttpPost("CreatePalette"), Authorize]
    public async Task<IActionResult> CreatePalette(CreatePaletteDto request, CancellationToken ct)
    {
        try
        {
            var user = HttpContext.User;
            var userIdClaim = user.FindFirst("userId")?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim))
                return BadRequest("id not found");

            if (!Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("invalid user ID format");
            
            await _paletteService.CreatePalette(request.Name, userId, ct);
            return Ok(200);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogWarning(e, "Palette creation failed: {Message}", e.Message);
            return BadRequest(new { Message = e.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "error");
            return StatusCode(500, new { Message = "An internal server error occurred." });
        }
    }
    
   [HttpGet("GetAllPalettes"), Authorize]
    public async Task<ActionResult<GetAllPalettesDto>> GetAllPalettes(CancellationToken ct)
    {
        var generator = new ColorGenerateService();
        var list = new List<byte[]>();
        
        var palettes = await _paletteService.GetAllPalettes(ct);

        return Ok(palettes);
    }

    [HttpGet("GetPaletteByUserName"), Authorize]
    public async Task<List<GetPalettesByUserNameDto>> GetPaletteByUserName(string userName, CancellationToken ct)
    {
        try
        {
            var palette = await _paletteService.GetPaletteByUserName(userName, ct);
            return palette;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "error");
            throw;
        }
    }
}