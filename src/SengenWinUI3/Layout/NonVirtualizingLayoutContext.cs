using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// <see cref="NonVirtualizingLayout"/> が子を測り、置くための入り口。子は追加した順の番号で指す。
/// 配置の呼び出しの間だけ使え、呼び出しの外へ持ち出さない。
/// </summary>
public sealed class NonVirtualizingLayoutContext
{
    private readonly WinUI.Controls.Panel 本物;

    internal NonVirtualizingLayoutContext(WinUI.Controls.Panel 本物) => this.本物 = 本物;

    /// <summary>子の数。</summary>
    public int ChildCount => 本物.Children.Count;

    /// <summary>子を <paramref name="availableSize"/> の中で測り、子が望む大きさを返す。長さを限らないときは <c>new Px(double.PositiveInfinity)</c> を渡す。</summary>
    public ViewSize Measure(int index, ViewSize availableSize)
    {
        WinUI.UIElement 子 = 本物.Children[index];
        子.Measure(new Windows.Foundation.Size(availableSize.Width.Value, availableSize.Height.Value));
        return DesiredSize(index);
    }

    /// <summary>子を測った結果の、子が望む大きさ。</summary>
    public ViewSize DesiredSize(int index)
    {
        Windows.Foundation.Size 望む大きさ = 本物.Children[index].DesiredSize;
        return new ViewSize(new Px(望む大きさ.Width), new Px(望む大きさ.Height));
    }

    /// <summary>子を <paramref name="bounds"/> の場所に置く。</summary>
    public void Arrange(int index, Rect bounds) =>
        本物.Children[index].Arrange(new Windows.Foundation.Rect(bounds.X.Value, bounds.Y.Value, bounds.Width.Value, bounds.Height.Value));
}
