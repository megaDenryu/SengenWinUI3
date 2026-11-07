using Windows.System;

namespace SengenWinUI3;

// 注意: 範囲で変える4つの組は、Key と VirtualKey の両方で途切れず同じ順に並んでいることに頼る。Key の並びを変えるときは、この組を崩さない。
internal static class VirtualKeyConversion
{
    internal static Key ToKey(this VirtualKey キー) => キー switch
    {
        >= VirtualKey.A and <= VirtualKey.Z => Key.A + (キー - VirtualKey.A),
        >= VirtualKey.Number0 and <= VirtualKey.Number9 => Key.Number0 + (キー - VirtualKey.Number0),
        >= VirtualKey.NumberPad0 and <= VirtualKey.NumberPad9 => Key.NumberPad0 + (キー - VirtualKey.NumberPad0),
        >= VirtualKey.F1 and <= VirtualKey.F12 => Key.F1 + (キー - VirtualKey.F1),
        VirtualKey.Enter => Key.Enter,
        VirtualKey.Escape => Key.Escape,
        VirtualKey.Tab => Key.Tab,
        VirtualKey.Space => Key.Space,
        VirtualKey.Back => Key.Back,
        VirtualKey.Delete => Key.Delete,
        VirtualKey.Insert => Key.Insert,
        VirtualKey.Home => Key.Home,
        VirtualKey.End => Key.End,
        VirtualKey.PageUp => Key.PageUp,
        VirtualKey.PageDown => Key.PageDown,
        VirtualKey.Left => Key.Left,
        VirtualKey.Up => Key.Up,
        VirtualKey.Right => Key.Right,
        VirtualKey.Down => Key.Down,
        VirtualKey.Shift or VirtualKey.LeftShift or VirtualKey.RightShift => Key.Shift,
        VirtualKey.Control or VirtualKey.LeftControl or VirtualKey.RightControl => Key.Control,
        VirtualKey.Menu or VirtualKey.LeftMenu or VirtualKey.RightMenu => Key.Menu,
        _ => Key.Other,
    };
}
