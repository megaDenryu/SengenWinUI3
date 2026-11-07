using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>中身を1つ持ち、収まらない分をスクロールで見せる部品。既定は WinUI 3 と同じく縦にスクロールする。作る関数は <see cref="UI.ScrollViewer(Style?)"/>。</summary>
public sealed class ScrollViewer : Node, IControlNode
{
    private const double 末尾とみなす残り = 1; // 拡大率による端数で、末尾にいても残りが 0 にならないことがある

    private readonly WinUI.Controls.ScrollViewer 本物 = new();
    private bool 中身を持つか;
    private bool 末尾に留まるか;
    private bool 末尾にいるか = true;

    internal ScrollViewer() { }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>中身を置く。中身を替えるときは、前の中身を Destroy か Detach してから置く。中身があるのに呼ぶと例外を投げる。</summary>
    public ScrollViewer Content(Node content)
    {
        ThrowIfDestroyed();
        if (中身を持つか)
        {
            throw new InvalidOperationException("ScrollViewer はすでに中身を持つ。前の中身を Destroy か Detach してから置く");
        }
        AdoptChild(content);
        本物.Content = content.Element;
        中身を持つか = true;
        return this;
    }

    /// <summary>末尾(一番下)までスクロールする。</summary>
    public void ScrollToEnd()
    {
        ThrowIfDestroyed();
        本物.UpdateLayout(); // 注意: 直前に足した中身の高さを、スクロールできる量に入れてから動かす
        _ = 本物.ChangeView(null, 本物.ScrollableHeight, null, true);
    }

    /// <summary>末尾にいる間に中身が伸びたら、末尾へ付いていく。利用者が上へスクロールしている間は付いていかない。</summary>
    public ScrollViewer StickToEnd()
    {
        ThrowIfWiredTwice(ref 末尾に留まるか, nameof(StickToEnd));
        本物.ViewChanged += (_, _) => 末尾にいるか = 本物.ScrollableHeight - 本物.VerticalOffset <= 末尾とみなす残り;
        _ = 本物.RegisterPropertyChangedCallback(WinUI.Controls.ScrollViewer.ExtentHeightProperty, (_, _) => 伸びたら末尾へ付いていく());
        return this;
    }

    internal override void ApplyStyle(Style style)
    {
        style.ThrowIfUnsupported(StyleItems.Layout | StyleItems.ControlLook, this);
        style.ApplyLayout(本物);
        style.ApplyControlLook(本物);
    }

    internal override void RemoveChildElement(Node child)
    {
        本物.Content = null;
        中身を持つか = false;
    }

    private void 伸びたら末尾へ付いていく()
    {
        if (末尾にいるか)
        {
            _ = 本物.ChangeView(null, 本物.ExtentHeight - 本物.ViewportHeight, null, true);
        }
    }
}
