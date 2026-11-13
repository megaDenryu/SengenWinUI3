using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>キーの組み合わせが押された出来事の中身。出来事のたびに割り当てないよう構造体にした。</summary>
public readonly struct KeyboardAcceleratorInput
{
    private readonly WinUI.Input.KeyboardAcceleratorInvokedEventArgs 元の出来事;

    internal KeyboardAcceleratorInput(WinUI.Input.KeyboardAcceleratorInvokedEventArgs 元の出来事) => this.元の出来事 = 元の出来事;

    /// <summary>true にすると、この出来事をほかの処理へ伝えない。</summary>
    public bool Handled
    {
        get => 元の出来事.Handled;
        set => 元の出来事.Handled = value;
    }
}
