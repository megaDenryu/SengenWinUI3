using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>長方形を表示する部品。大きさ・塗り・輪郭・角の丸み(CornerRadius)は Style で指定し、色つきの光は <see cref="SetDropShadow"/> で出す。</summary>
public sealed class Rectangle : Node
{
    private readonly WinUI.Shapes.Rectangle 本物 = new();
    private readonly Lazy<ShadowLayer> 光;

    internal Rectangle() => 光 = new(光を作って預ける);

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>長方形の描かれた形から、色つきの光を出す。塗りか輪郭が無い長方形には光が出ない。繰り返し呼べる。</summary>
    public Rectangle SetDropShadow(ColorShadow shadow)
    {
        ThrowIfDestroyed();
        光.Value.Show(shadow);
        return this;
    }

    /// <summary>出している光を消す。出していなければ何もしない。</summary>
    public Rectangle ClearDropShadow()
    {
        ThrowIfDestroyed();
        if (光.IsValueCreated)
        {
            光.Value.Hide();
        }
        return this;
    }

    internal override void ApplyStyle(Style style) => RectangleStyle.All.Apply(style, 本物, this);

    private ShadowLayer 光を作って預ける()
    {
        ShadowLayer 層 = new(本物);
        OwnDisposable(層);
        return 層;
    }
}
