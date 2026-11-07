using Microsoft.UI.Input;
using Windows.System;
using Windows.UI.Core;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>キーが押された出来事の中身。</summary>
public sealed class KeyInput
{
    private readonly WinUI.Input.KeyRoutedEventArgs 元の出来事;

    internal KeyInput(WinUI.Input.KeyRoutedEventArgs 元の出来事)
    {
        this.元の出来事 = 元の出来事;
        Key = 元の出来事.Key.ToKey();
        IsShiftKeyDown = 押されているか(VirtualKey.Shift);
        IsControlKeyDown = 押されているか(VirtualKey.Control);
        IsMenuKeyDown = 元の出来事.KeyStatus.IsMenuKeyDown;
    }

    /// <summary>押されたキー。</summary>
    public Key Key { get; }

    /// <summary>Shift キーが押されたままか。</summary>
    public bool IsShiftKeyDown { get; }

    /// <summary>Ctrl キーが押されたままか。</summary>
    public bool IsControlKeyDown { get; }

    /// <summary>Alt キーが押されたままか。</summary>
    public bool IsMenuKeyDown { get; }

    /// <summary>true にすると、この出来事を親の部品へ伝えない。</summary>
    public bool Handled
    {
        get => 元の出来事.Handled;
        set => 元の出来事.Handled = value;
    }

    private static bool 押されているか(VirtualKey キー) =>
        (InputKeyboardSource.GetKeyStateForCurrentThread(キー) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
}
