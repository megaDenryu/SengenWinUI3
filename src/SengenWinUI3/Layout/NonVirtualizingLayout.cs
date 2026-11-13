namespace SengenWinUI3;

/// <summary>
/// <see cref="LayoutPanel"/> の子の測り方と置き方を書く基底。利用する側が派生して <see cref="MeasureOverride"/> と <see cref="ArrangeOverride"/> を書く。
/// 呼ぶ時と回数はレイアウトの仕組みが決める。
/// </summary>
public abstract class NonVirtualizingLayout
{
    /// <summary>派生から呼ぶ。</summary>
    protected NonVirtualizingLayout() { }

    /// <summary>子を測り、全体が望む大きさを返す。子は <paramref name="context"/> で測る。</summary>
    protected abstract ViewSize MeasureOverride(NonVirtualizingLayoutContext context, ViewSize availableSize);

    /// <summary>子を置き、使った大きさを返す。子は <paramref name="context"/> で置く。</summary>
    protected abstract ViewSize ArrangeOverride(NonVirtualizingLayoutContext context, ViewSize finalSize);

    internal ViewSize Measure(NonVirtualizingLayoutContext context, ViewSize availableSize) => MeasureOverride(context, availableSize);

    internal ViewSize Arrange(NonVirtualizingLayoutContext context, ViewSize finalSize) => ArrangeOverride(context, finalSize);
}
