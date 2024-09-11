namespace COLOR.Services;

public interface IColorService
{
    public Task AddColorToPalette(string hexCode, Guid paletteId, CancellationToken ct);
    public string ColorGenerate();

}