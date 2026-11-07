using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>マウス・ペン・指の出来事の中身。位置は出来事を受けた部品の左上からの長さである。</summary>
public sealed class PointerInput
{
    private readonly WinUI.Input.PointerRoutedEventArgs 元の出来事;

    internal PointerInput(WinUI.Input.PointerRoutedEventArgs 元の出来事, WinUI.UIElement 受けた部品の本物)
    {
        this.元の出来事 = 元の出来事;
        Microsoft.UI.Input.PointerPoint 点 = 元の出来事.GetCurrentPoint(受けた部品の本物);
        X = new Px(点.Position.X);
        Y = new Px(点.Position.Y);
        IsLeftButtonPressed = 点.Properties.IsLeftButtonPressed;
        IsRightButtonPressed = 点.Properties.IsRightButtonPressed;
        IsMiddleButtonPressed = 点.Properties.IsMiddleButtonPressed;
    }

    /// <summary>部品の左端からの横の位置。</summary>
    public Px X { get; }

    /// <summary>部品の上端からの縦の位置。</summary>
    public Px Y { get; }

    /// <summary>左のボタンが押されているか。</summary>
    public bool IsLeftButtonPressed { get; }

    /// <summary>右のボタンが押されているか。</summary>
    public bool IsRightButtonPressed { get; }

    /// <summary>真ん中のボタンが押されているか。</summary>
    public bool IsMiddleButtonPressed { get; }

    /// <summary>true にすると、この出来事を親の部品へ伝えない。</summary>
    public bool Handled
    {
        get => 元の出来事.Handled;
        set => 元の出来事.Handled = value;
    }
}
