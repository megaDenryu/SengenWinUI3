using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Rectangle の塗りの色を、Storyboard の ColorAnimationUsingKeyFrames で止めるまで繰り返し動かす。
// 動かす色は、動きを始めるたびに作る専用の SolidColorBrush が持つ。Style の Fill のブラシは同じ Style を当てた部品で共有されるので、そのブラシの色は動かさない。
// 前提: UI スレッドで作る。
internal sealed class FillColorAnimation : IDisposable
{
    private readonly WinUI.Shapes.Rectangle 形の元;
    private Storyboard 動き = new();

    internal FillColorAnimation(WinUI.Shapes.Rectangle 形の元) => this.形の元 = 形の元;

    internal void Begin(TimeSpan duration, ColorKeyFrame[] keyFrames)
    {
        if (keyFrames.Length == 0)
        {
            throw new ArgumentException("途中経過は1つ以上要る", nameof(keyFrames));
        }
        動き.Stop();
        SolidColorBrush 専用のブラシ = new(keyFrames[0].Value.ToWinUI());
        形の元.Fill = 専用のブラシ;
        動き = 動きを作る(専用のブラシ, duration, keyFrames);
        動き.Begin();
    }

    internal void Stop() => 動き.Stop();

    public void Dispose() => 動き.Stop();

    private static Storyboard 動きを作る(SolidColorBrush 対象のブラシ, TimeSpan 一周の長さ, ColorKeyFrame[] 途中経過List)
    {
        ColorAnimationUsingKeyFrames 色の動き = new() { Duration = new WinUI.Duration(一周の長さ), RepeatBehavior = RepeatBehavior.Forever };
        foreach (ColorKeyFrame 途中経過 in 途中経過List)
        {
            色の動き.KeyFrames.Add(new LinearColorKeyFrame { KeyTime = KeyTime.FromTimeSpan(途中経過.KeyTime), Value = 途中経過.Value.ToWinUI() });
        }
        Storyboard.SetTarget(色の動き, 対象のブラシ);
        Storyboard.SetTargetProperty(色の動き, "Color");
        Storyboard 組 = new();
        組.Children.Add(色の動き);
        return 組;
    }
}
