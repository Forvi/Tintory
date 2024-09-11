using COLOR.Domain.Etities;

namespace COLOR.Data.Repository;

public interface IColorRepository
{
    public Task<ColorEntity> CreateColor(string hexCode, CancellationToken ct);
    public Task<ColorEntity?> FindExistingColor(string hexCode, CancellationToken ct);

}