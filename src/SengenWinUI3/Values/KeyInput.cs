using Microsoft.UI.Input;
using Windows.System;
using Windows.UI.Core;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>キーが押された出来事の中身。出来事のたびに割り当てないよう構造体にした。修飾キーの状態は、押された時点の値を作るときに読む。</summary>
public readonly struct KeyInput
{
    private readonly WinUI.Input.KeyRoutedEventArgs 元の出来事;

    internal KeyInput(WinUI.Input.KeyRoutedEventArgs 元の出来事)
    {
        this.元の出来事 = 元の出来事;
        IsShiftKeyDown = 押されているか(VirtualKey.Shift);
        IsControlKeyDown = 押されているか(VirtualKey.Control);
    }

    /// <summary>押されたキー。</summary>
    public Key Key => 元の出来事.Key.ToKey();

    /// <summary>Shift キーが押されたままか。</summary>
    public bool IsShiftKeyDown { get; }

    /// <summary>Ctrl キーが押されたままか。</summary>
    public bool IsControlKeyDown { get; }

    /// <summary>Alt キーが押されたままか。</summary>
    public bool IsMenuKeyDown => 元の出来事.KeyStatus.IsMenuKeyDown;

    /// <summary>true にすると、この出来事を親の部品へ伝えない。</summary>
    public bool Handled
    {
        get => 元の出来事.Handled;
        set => 元の出来事.Handled = value;
    }

    /// <summary>このキーの出来事を最初に受けたのが <paramref name="node"/> 自身か。<paramref name="node"/> の中の部品が受けた出来事なら false になる。</summary>
    public bool IsOriginalSource(Node node)
    {
        node.ThrowIfDestroyed();
        return ReferenceEquals(元の出来事.OriginalSource, node.Element);
    }

    private static bool 押されているか(VirtualKey キー) =>
        (InputKeyboardSource.GetKeyStateForCurrentThread(キー) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
}
