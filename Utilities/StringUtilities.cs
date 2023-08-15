using System.Text;

namespace Utilities;

public static class StringUtilities {
    public static string Repeat(this string value, int count) {
        if (count <= 0) {
            return string.Empty;
        }

        if (count == 1) {
            return value;
        }

        StringBuilder sb = new(value.Length * count);
        for (int i = 0; i < count; i++) {
            sb.Append(value);
        }

        return sb.ToString();
    }
}