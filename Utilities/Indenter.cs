namespace Utilities;

public static class Indenter {
    public static string Indent(this string str, int spaces, bool firstLine = true) {
        string indent = new(' ', spaces);
        if (firstLine) {
            return indent + str.Replace("\n", "\n" + indent);
        } else {
            return str.Replace("\n", "\n" + indent);
        }
    }

    public static string IndentUsing(this string str, string reference, bool firstLine = true) => Indent(str, reference.Length, firstLine);
}