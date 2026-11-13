using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 安定版の WinUI 3 に LayoutPanel が無いため、本物は Panel の派生で代わりをする。
/// <summary>子の測り方と置き方を <see cref="NonVirtualizingLayout"/> に任せるパネル。子は Children などで入れる。作る関数は <see cref="UI.LayoutPanel(NonVirtualizingLayout, Style?)"/>。</summary>
public sealed class LayoutPanel : Node, IHasChildren
{
    private static readonly StyleTarget<WinUI.Controls.Panel> 当て方 =
        new(FrameworkElementStyle.Layout, PanelStyle.Background);

    private readonly 配置を任せるPanel 本物;

    internal LayoutPanel(NonVirtualizingLayout layout) => 本物 = new 配置を任せるPanel(layout);

    internal override WinUI.FrameworkElement Element => 本物;

    int IHasChildren.ChildCount => 本物.Children.Count;

    void IHasChildren.InsertChildElement(int index, Node child) => 本物.Children.Insert(index, child.Element);

    void IHasChildren.RemoveChildElement(Node child) => _ = 本物.Children.Remove(child.Element);

    void IHasChildren.ClearChildElements() => 本物.Children.Clear();

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);

    // WinUI の Panel は MeasureOverride と ArrangeOverride を上書きして子の測り方と置き方を決めるので、その2つを NonVirtualizingLayout へ渡す。
    private sealed class 配置を任せるPanel : WinUI.Controls.Panel
    {
        private readonly NonVirtualizingLayout 配置;
        private readonly NonVirtualizingLayoutContext 子への入り口;

        internal 配置を任せるPanel(NonVirtualizingLayout layout)
        {
            配置 = layout;
            子への入り口 = new NonVirtualizingLayoutContext(this);
        }

        protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize)
        {
            ViewSize 望む大きさ = 配置.Measure(子への入り口, ToViewSize(availableSize));
            return new Windows.Foundation.Size(望む大きさ.Width.Value, 望む大きさ.Height.Value);
        }

        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            ViewSize 使った大きさ = 配置.Arrange(子への入り口, ToViewSize(finalSize));
            return new Windows.Foundation.Size(使った大きさ.Width.Value, 使った大きさ.Height.Value);
        }

        private static ViewSize ToViewSize(Windows.Foundation.Size 大きさ) => new(new Px(大きさ.Width), new Px(大きさ.Height));
    }
}
