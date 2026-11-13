using Windows.System;

namespace SengenWinUI3;

// 注意: 範囲で変える4つの組は、Key と VirtualKey の両方で途切れず同じ順に並んでいることに頼る(VirtualKeyConversion と同じ)。
internal static class KeyConversion
{
    internal static VirtualKey ToVirtualKey(this Key キー) => キー switch
    {
        >= Key.A and <= Key.Z => VirtualKey.A + (キー - Key.A),
        >= Key.Number0 and <= Key.Number9 => VirtualKey.Number0 + (キー - Key.Number0),
        >= Key.NumberPad0 and <= Key.NumberPad9 => VirtualKey.NumberPad0 + (キー - Key.NumberPad0),
        >= Key.F1 and <= Key.F12 => VirtualKey.F1 + (キー - Key.F1),
        Key.Enter => VirtualKey.Enter,
        Key.Escape => VirtualKey.Escape,
        Key.Tab => VirtualKey.Tab,
        Key.Space => VirtualKey.Space,
        Key.Back => VirtualKey.Back,
        Key.Delete => VirtualKey.Delete,
        Key.Insert => VirtualKey.Insert,
        Key.Home => VirtualKey.Home,
        Key.End => VirtualKey.End,
        Key.PageUp => VirtualKey.PageUp,
        Key.PageDown => VirtualKey.PageDown,
        Key.Left => VirtualKey.Left,
        Key.Up => VirtualKey.Up,
        Key.Right => VirtualKey.Right,
        Key.Down => VirtualKey.Down,
        Key.Shift => VirtualKey.Shift,
        Key.Control => VirtualKey.Control,
        Key.Menu => VirtualKey.Menu,
        _ => throw new ArgumentOutOfRangeException(nameof(キー), キー, "対応する WinUI のキーが無い"),
    };
}
