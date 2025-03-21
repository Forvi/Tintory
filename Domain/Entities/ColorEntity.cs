﻿namespace COLOR.Domain.Entities;

public class ColorEntity
{
    public Guid Id { get; set; }
    public string HexCode { get; set; }
    public IList<PaletteEntity> Palettes { get; set; } = new List<PaletteEntity>();
}