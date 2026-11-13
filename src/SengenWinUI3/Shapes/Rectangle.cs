using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>長方形を表示する部品。大きさ・塗り・輪郭・角の丸み(CornerRadius)は Style で指定し、色つきの光は <see cref="SetDropShadow"/> で出す。</summary>
public sealed class Rectangle : Node
{
    private readonly WinUI.Shapes.Rectangle 本物 = new();
    private readonly Lazy<ShadowLayer> 光;

    private readonly Lazy<FillColorAnimation> 塗りの色の動き;

    internal Rectangle()
    {
        光 = new(光を作って預ける);
        塗りの色の動き = new(塗りの色の動きを作って預ける);
    }

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

    /// <summary>
    /// 塗りの色を、<paramref name="keyFrames"/> の途中経過を直線でつないだ動きで、止めるまで繰り返し変える。<paramref name="duration"/> が1周の長さである。
    /// 動かす色はこの長方形専用のブラシが持ち、Style の Fill のブラシの色は変えない。長方形の塗りはこのブラシに替わる。
    /// 繰り返し呼ぶと、前の動きを新しい動きで置き換える。途中経過が1つも無ければ例外を投げる。
    /// </summary>
    public Rectangle BeginFillColorAnimation(TimeSpan duration, ColorKeyFrame[] keyFrames)
    {
        ThrowIfDestroyed();
        塗りの色の動き.Value.Begin(duration, keyFrames);
        return this;
    }

    /// <summary>塗りの色の動きを止める。塗りは最初の途中経過の色に戻る。動かしていなければ何もしない。</summary>
    public Rectangle StopFillColorAnimation()
    {
        ThrowIfDestroyed();
        if (塗りの色の動き.IsValueCreated)
        {
            塗りの色の動き.Value.Stop();
        }
        return this;
    }

    internal override void ApplyStyle(Style style) => RectangleStyle.All.Apply(style, 本物, this);

    private FillColorAnimation 塗りの色の動きを作って預ける()
    {
        FillColorAnimation 動き = new(本物);
        OwnDisposable(動き);
        return 動き;
    }

    private ShadowLayer 光を作って預ける()
    {
        ShadowLayer 層 = new(本物);
        OwnDisposable(層);
        return 層;
    }
}
