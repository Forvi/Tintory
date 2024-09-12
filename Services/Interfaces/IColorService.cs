namespace COLOR.Services.Interfaces;

public interface IColorService
{
    public Task AddColorToPalette(string hexCode, Guid paletteId, CancellationToken ct);
    public string ColorGenerate();

}