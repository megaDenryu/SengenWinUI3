namespace SengenWinUI3;

/// <summary>押されたキー。名前は WinUI の VirtualKey にそろえる。ここに無いキーは <see cref="Other"/> になる。</summary>
public enum Key
{
    Other,
    A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
    Number0, Number1, Number2, Number3, Number4, Number5, Number6, Number7, Number8, Number9,
    NumberPad0, NumberPad1, NumberPad2, NumberPad3, NumberPad4, NumberPad5, NumberPad6, NumberPad7, NumberPad8, NumberPad9,
    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
    Enter,
    Escape,
    Tab,
    Space,
    Back,
    Delete,
    Insert,
    Home,
    End,
    PageUp,
    PageDown,
    Left,
    Up,
    Right,
    Down,
    Shift,
    Control,
    Menu,
}
