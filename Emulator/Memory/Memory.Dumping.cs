using System.Text;
using B = Utilities.BoxArt;

namespace Emulator;

public readonly partial struct Memory {
    public void DumpPage(int pageNumber, StringBuilder stringBuilder, int width = 1, bool writeHeader = true, bool writeFooter = true) {
        const int bytesPerLine = 16;
        int startingAddress = pageNumber * 0x100;

        int columns = bytesPerLine * width;
        int rows = 0x100 / columns;

        if (writeHeader) {
            stringBuilder.Append("        ");
            AppendHeader(stringBuilder, width);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"      {B.TopLeft()}{B.Horizontal(48 * width + 1)}{B.TopRight()}");
        }

        for (int row = 0; row < rows; row++) {
            int address = startingAddress + row * columns;
            stringBuilder.Append($" {address.ToString("X4").Cyan()} {B.Vertical()}");
            for (int col = 0; col < columns; col++) {
                stringBuilder.Append($" {this[address + col].ToString("X2").DarkBlue()}");
            }

            stringBuilder.Append($" {B.Vertical()}");
            stringBuilder.AppendLine();
        }

        if (writeFooter) {
            stringBuilder.AppendLine($"      {B.BottomLeft()}{B.Horizontal(48 * width + 1)}{B.BottomRight()}");
        }
    }

    public void DumpPages(Range range, StringBuilder stringBuilder, int width = 1) {
        int pageCount = this.Size / 0xFF;
        (int offset, int length) = range.GetOffsetAndLength(pageCount);
        for (int pageNumber = offset; pageNumber < offset + length; pageNumber++) {
            this.DumpPage(pageNumber, stringBuilder, width, pageNumber == offset, pageNumber == offset + length - 1);
        }
    }

    private static void AppendHeader(StringBuilder stringBuilder, int width) {
        for (int i = 0; i < width * 16; i++) {
            stringBuilder.Append($"{i.ToString("X").PadRight(3).Cyan()}");
        }
    }
}