using COLOR.Domain.Entities;

namespace COLOR.Data.Repositories.Interfaces;

public interface IColorRepository
{
    public Task<ColorEntity> CreateColor(string hexCode, CancellationToken ct);
    public Task<ColorEntity?> FindExistingColor(string hexCode, CancellationToken ct);

}