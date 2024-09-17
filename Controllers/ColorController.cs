using COLOR.DTOs;
using COLOR.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COLOR.Controllers;

public class ColorController : ControllerBase
{
    private readonly IColorService _colorService;
    private readonly ILogger<PaletteController> _logger;

    public ColorController(IColorService colorService, ILogger<PaletteController> logger)
    {
        _colorService = colorService;
        _logger = logger;
    }
    
    [HttpPost("GenerateColor")]
    public ActionResult<List<string>> ColorGenerate([FromQuery]int quantity)
    {
        var hex = _colorService.ColorGenerate(quantity);
        return Ok(hex);
    }

    [Authorize]
    [HttpPost("AddColorToPalette"), Authorize]
    public async Task<IActionResult> AddColorToPalette(HexPaletteIdDto request, CancellationToken ct)
    {
        try
        {
            var hex = request.HexCode;
            var paletteId = request.PaletteId;

           await _colorService.AddColorToPalette(hex, paletteId, ct);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            throw;
        }
    }
}