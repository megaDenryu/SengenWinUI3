using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品が押された(Tapped)出来事の中身。出来事のたびに割り当てないよう構造体にした。</summary>
public readonly struct TapInput
{
    private readonly WinUI.Input.TappedRoutedEventArgs 元の出来事;

    internal TapInput(WinUI.Input.TappedRoutedEventArgs 元の出来事) => this.元の出来事 = 元の出来事;

    /// <summary>true にすると、この出来事を親の部品へ伝えない。</summary>
    public bool Handled
    {
        get => 元の出来事.Handled;
        set => 元の出来事.Handled = value;
    }
}
