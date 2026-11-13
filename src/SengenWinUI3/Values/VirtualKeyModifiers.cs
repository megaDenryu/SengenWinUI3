namespace SengenWinUI3;

/// <summary>キーの組み合わせで一緒に押す修飾キー。複数の修飾キーは <c>|</c> で組み合わせる。名前は WinUI の VirtualKeyModifiers にそろえる。</summary>
[Flags]
public enum VirtualKeyModifiers
{
    None = 0,
    Control = 1,
    Menu = 2,
    Shift = 4,
    Windows = 8,
}
