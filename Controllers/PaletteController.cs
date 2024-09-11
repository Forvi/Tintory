using COLOR.Domain.Etities;
using COLOR.DTOs;
using COLOR.Services;
using Microsoft.AspNetCore.Mvc;

namespace COLOR.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaletteController : ControllerBase
{
    private readonly IPaletteService _paletteServiceService;
    private readonly ILogger<PaletteController> _logger;

    public PaletteController(IPaletteService service, ILogger<PaletteController> logger)
    {
        _paletteServiceService = service;
        _logger = logger;
    }

    [HttpPost("CreatePalette")]
    public async Task<IActionResult> CreatePalette(CreatePaletteDto request, CancellationToken ct)
    {
        try
        {
            await _paletteServiceService.CreatePalette(request.Name, ct);
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

    [HttpGet("GetAllPalettes")]
    public async Task<ActionResult<GetAllPalettesDto>> GetAllPalettes(CancellationToken ct)
    {
        var generator = new ColorGenerateService();
        var list = new List<byte[]>();
        
        var palettes = await _paletteServiceService.GetAllPalettes(ct);
        // var a = palettes.Select(s => s.Colors);

        return Ok(palettes);
    }

}