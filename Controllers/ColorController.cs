using COLOR.DTOs;
using COLOR.Services;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public ActionResult<string> ColorGenerate()
    {
        var hex = _colorService.ColorGenerate();
        return hex;
    }

    [HttpPost("AddColorToPalette")]
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
            Console.WriteLine(e);
            throw;
        }
    }
}