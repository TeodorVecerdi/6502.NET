using System.Text;
using Pastel;

namespace Emulator;

public static class Colorizer {
    public const string BLUE = "#3daee9";
    public const string CYAN = "#1abc9c";
    public const string YELLOW = "#fdbc4b";
    public const string DARK_BLUE = "#1d99f3";
    public const string DARK_CYAN = "#16a085";
    public const string DARK_YELLOW = "#bd7f23";

    public static bool IsColorOutputEnabled { get; set; } = true;

    public static string Fg(this string text, string color) => IsColorOutputEnabled ? text.Pastel(color) : text;
    public static string Bg(this string text, string color) => IsColorOutputEnabled ? text.PastelBg(color) : text;

    public static string Blue(this string text) => text.Fg(BLUE);
    public static string Cyan(this string text) => text.Fg(CYAN);
    public static string Yellow(this string text) => text.Fg(YELLOW);
    public static string DarkBlue(this string text) => text.Fg(DARK_BLUE);
    public static string DarkCyan(this string text) => text.Fg(DARK_CYAN);
    public static string DarkYellow(this string text) => text.Fg(DARK_YELLOW);

    public static string Stylize(this string text, params GraphicsMode[] modes) {
        if (!IsColorOutputEnabled) return text;
        return GetSetString(modes) + text + GetResetString(modes);
    }

    private static string GetSetString(params GraphicsMode[] modes) {
        const char ESC = (char)0x1B;
        StringBuilder sb = new();
        sb.Append(ESC).Append('[');

        for (int i = 0, len = modes.Length; i < len; ++i) {
            if (i != 0) {
                sb.Append(';');
            }

            sb.Append((int)modes[i]);
        }

        sb.Append('m');
        return sb.ToString();
    }

    private static string GetResetString(params GraphicsMode[] modes) {
        const char ESC = (char)0x1B;
        StringBuilder sb = new();
        sb.Append(ESC).Append('[');

        for (int i = 0, len = modes.Length; i < len; ++i) {
            if (i != 0) {
                sb.Append(';');
            }

            sb.Append(GetResetCode(modes[i]));
        }

        sb.Append('m');
        return sb.ToString();
    }

    private static int GetResetCode(GraphicsMode mode) {
        if (mode == GraphicsMode.BOLD) return 22;
        return 20 + (int)mode;
    }
}

public enum GraphicsMode {
    BOLD = 1,
    DIM = 2,
    ITALIC = 3,
    UNDERLINE = 4,
    BLINK = 5,
    REVERSE = 7,
    HIDDEN = 8,
    STRIKETHROUGH = 9,
}