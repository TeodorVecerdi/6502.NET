namespace Utilities;

public static class BoxArt {
    public static BoxArtStyle Style { get; set; } = BoxArtStyle.Rounded;

    private static char HorizontalChar => Style switch {
        BoxArtStyle.Ascii => '-',
        BoxArtStyle.Rounded or BoxArtStyle.Light => '\u2500',
        BoxArtStyle.Heavy => '\u2501',
        _ => throw new ArgumentOutOfRangeException(nameof(Style), Style, null),
    };

    private static char VerticalChar => Style switch {
        BoxArtStyle.Ascii => '|',
        BoxArtStyle.Rounded or BoxArtStyle.Light => '\u2502',
        BoxArtStyle.Heavy => '\u2503',
        _ => throw new ArgumentOutOfRangeException(nameof(Style), Style, null),
    };

    private static char TopLeftCornerChar => Style switch {
        BoxArtStyle.Ascii => '+',
        BoxArtStyle.Light => '\u250C',
        BoxArtStyle.Heavy => '\u250F',
        BoxArtStyle.Rounded => '\u256D',
        _ => throw new ArgumentOutOfRangeException(nameof(Style), Style, null),
    };

    private static char TopRightCornerChar => Style switch {
        BoxArtStyle.Ascii => '+',
        BoxArtStyle.Light => '\u2510',
        BoxArtStyle.Heavy => '\u2513',
        BoxArtStyle.Rounded => '\u256E',
        _ => throw new ArgumentOutOfRangeException(nameof(Style), Style, null),
    };

    private static char BottomLeftCornerChar => Style switch {
        BoxArtStyle.Ascii => '+',
        BoxArtStyle.Light => '\u2514',
        BoxArtStyle.Heavy => '\u2517',
        BoxArtStyle.Rounded => '\u2570',
        _ => throw new ArgumentOutOfRangeException(nameof(Style), Style, null),
    };

    private static char BottomRightCornerChar => Style switch {
        BoxArtStyle.Ascii => '+',
        BoxArtStyle.Light => '\u2518',
        BoxArtStyle.Heavy => '\u251B',
        BoxArtStyle.Rounded => '\u256F',
        _ => throw new ArgumentOutOfRangeException(nameof(Style), Style, null),
    };

    public static string Horizontal(int length = 1) => new(HorizontalChar, length);
    public static string Vertical(int length = 1) => new(VerticalChar, length);

    public static string TopLeft() => TopLeftCornerChar.ToString();
    public static string TopRight() => TopRightCornerChar.ToString();
    public static string BottomLeft() => BottomLeftCornerChar.ToString();
    public static string BottomRight() => BottomRightCornerChar.ToString();

}

public enum BoxArtStyle {
    Ascii,
    Light,
    Heavy,
    Rounded,
}