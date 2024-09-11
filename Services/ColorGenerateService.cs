using System.Drawing;

namespace COLOR.Services;

public class ColorGenerateService
{
    public string ColorGenerate()
    {
        var rnd = new Random();

        var color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
        var colorCode = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        return colorCode;
    }

    // public byte[] ConvertHexToBytes(string hex)
    // {
    //     hex = hex.TrimStart('#');
    //
    //     byte r = Convert.ToByte(hex.Substring(0, 2), 16);
    //     byte g = Convert.ToByte(hex.Substring(2, 2), 16);
    //     byte b = Convert.ToByte(hex.Substring(4, 2), 16);;
    //     
    //     return new byte[] {r, g, b};
    // }
    
    public byte[] ConvertHexToBytes(string hex)
    {
        return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    }

    public ICollection<byte[]> ConvertStringsToBytes(ICollection<string> hexColors)
    {
        return hexColors.Select(hex => ConvertHexToBytes(hex)).ToList();
    }


    public string ConvertBytesToHex(byte[] bytes)
    {
        if (bytes.Length != 3)
        {
            throw new ArgumentException("Invalid color code");
        }
        
        string hex = $"#{bytes[0]:X2}{bytes[1]:X2}{bytes[2]:X2}";
        return hex;
    }

}