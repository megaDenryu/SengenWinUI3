using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Hosting;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品の不透明度を、Composition の動きで繰り返し上下させるチェーン。</summary>
public static class PulseExtensions
{
    private const string 不透明度の名前 = "Opacity";

    /// <summary>
    /// 不透明度を、今の不透明度から <paramref name="lowest"/> まで下げて戻す動きを、<paramref name="period"/> を1周期として止めるまで繰り返す。
    /// 周期は 1 ミリ秒以上にする。繰り返し呼ぶと、前の動きを新しい動きで置き換える。
    /// </summary>
    public static T PulseOpacity<T>(this T node, Ratio lowest, TimeSpan period) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        Visual 見た目 = ElementCompositionPreview.GetElementVisual(本物);
        float 高い不透明度 = float.CreateChecked(本物.Opacity);
        ScalarKeyFrameAnimation 動き = 見た目.Compositor.CreateScalarKeyFrameAnimation();
        動き.InsertKeyFrame(0f, 高い不透明度);
        動き.InsertKeyFrame(0.5f, float.CreateChecked(lowest.Value));
        動き.InsertKeyFrame(1f, 高い不透明度);
        動き.Duration = period;
        動き.IterationBehavior = AnimationIterationBehavior.Forever;
        見た目.StartAnimation(不透明度の名前, 動き);
        return node;
    }

    /// <summary>
    /// <see cref="PulseOpacity{T}"/> の動きを止め、不透明度を部品の Opacity に戻す。動かしていなければ何もしない。
    /// </summary>
    public static T StopPulse<T>(this T node) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        Visual 見た目 = ElementCompositionPreview.GetElementVisual(本物);
        見た目.StopAnimation(不透明度の名前);
        見た目.Opacity = float.CreateChecked(本物.Opacity);
        return node;
    }
}
