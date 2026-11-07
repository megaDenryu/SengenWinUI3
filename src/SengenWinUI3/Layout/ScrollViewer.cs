using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>中身を1つ持ち、収まらない分をスクロールで見せる部品。既定は WinUI 3 と同じく縦にスクロールする。作る関数は <see cref="UI.ScrollViewer(Node, Style?)"/>。</summary>
public sealed class ScrollViewer : Node, IControlNode
{
    private const double 末尾とみなす残り = 1; // 拡大率による端数で、末尾にいても残りが 0 にならないことがある

    private readonly WinUI.Controls.ScrollViewer 本物 = new();
    private readonly ContentSlot 中身置き場;
    private bool 末尾にいるか = true;

    internal ScrollViewer(Node content)
    {
        中身置き場 = new ContentSlot(this, 中身の本物 => 本物.Content = 中身の本物);
        中身置き場.SetContent(content);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>中身を入れ替える。前の中身は破棄する。親を持つ部品を渡すと例外を投げる。</summary>
    public ScrollViewer SetContent(Node content)
    {
        ThrowIfDestroyed();
        中身置き場.SetContent(content);
        return this;
    }

    /// <summary>末尾(一番下)までスクロールする。</summary>
    public void ScrollToEnd()
    {
        ThrowIfDestroyed();
        本物.UpdateLayout(); // 注意: 直前に足した中身の高さを、スクロールできる量に入れてから動かす
        _ = 本物.ChangeView(null, 本物.ScrollableHeight, null, true);
    }

    /// <summary>末尾にいる間に中身が伸びたら、末尾へ付いていく。利用者が上へスクロールしている間は付いていかない。2回目は例外を投げる。</summary>
    public ScrollViewer StickToEnd()
    {
        EventHandler<WinUI.Controls.ScrollViewerViewChangedEventArgs> 位置が変わったら末尾にいるかを見直す = (_, _) =>
        {
            if (!IsDestroyed) 末尾にいるか = 本物.ScrollableHeight - 本物.VerticalOffset <= 末尾とみなす残り;
        };
        WinUI.DependencyPropertyChangedCallback 中身の高さが変わったら末尾へ付いていく = (_, _) => 末尾へ付いていく();
        long 高さを購読した番号 = 0;
        WireEvent(
            nameof(StickToEnd),
            () =>
            {
                本物.ViewChanged += 位置が変わったら末尾にいるかを見直す;
                高さを購読した番号 = 本物.RegisterPropertyChangedCallback(WinUI.Controls.ScrollViewer.ExtentHeightProperty, 中身の高さが変わったら末尾へ付いていく);
            },
            () =>
            {
                本物.ViewChanged -= 位置が変わったら末尾にいるかを見直す;
                本物.UnregisterPropertyChangedCallback(WinUI.Controls.ScrollViewer.ExtentHeightProperty, 高さを購読した番号);
            });
        return this;
    }

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    internal override void RemoveChildElement(Node child) => 中身置き場.Release(child);

    private void 末尾へ付いていく()
    {
        if (!IsDestroyed && 末尾にいるか)
        {
            _ = 本物.ChangeView(null, 本物.ExtentHeight - 本物.ViewportHeight, null, true);
        }
    }
}
