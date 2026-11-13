using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// マウス・ペン・指の出来事の中身。X と Y は出来事を受けた部品の左上からの長さである。
/// 出来事のたびに割り当てないよう構造体にした。本物の点(WinUI の PointerPoint)は作るときに1回だけ読む。
/// </summary>
public readonly struct PointerInput
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
        UpdateKind = 点.Properties.PointerUpdateKind.ToPointerUpdateKind();
        MouseWheelDelta = 点.Properties.MouseWheelDelta;
    }

    internal WinUI.Input.Pointer Pointer => 元の出来事.Pointer;

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

    /// <summary>この出来事を起こしたボタンの変化。ボタンの押下と離しの出来事では、どのボタンかが分かる。</summary>
    public PointerUpdateKind UpdateKind { get; }

    /// <summary>ホイールの回転の量。奥へ回すと正、手前へ回すと負になる。ホイールの出来事でなければ 0 になる。</summary>
    public int MouseWheelDelta { get; }

    /// <summary>true にすると、この出来事を親の部品へ伝えない。</summary>
    public bool Handled
    {
        get => 元の出来事.Handled;
        set => 元の出来事.Handled = value;
    }

    /// <summary>ポインターの位置を、<paramref name="relativeTo"/> の左上からの長さで返す。出来事を受けた処理の中で呼ぶ。</summary>
    public Point GetPosition(Node relativeTo)
    {
        relativeTo.ThrowIfDestroyed();
        Windows.Foundation.Point 位置 = 元の出来事.GetCurrentPoint(relativeTo.Element).Position;
        return new Point(new Px(位置.X), new Px(位置.Y));
    }
}
