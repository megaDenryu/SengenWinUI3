using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// トーストの出入りの動き。横へ滑りながら不透明度を変える。
internal static class ToastMotion
{
    internal static void SlideIn(WinUI.FrameworkElement element, double distance, TimeSpan duration) =>
        Run(element, distance, 0, 0, 1, duration, static () => { });

    internal static void SlideOut(WinUI.FrameworkElement element, double distance, TimeSpan duration, Action completed) =>
        Run(element, 0, distance, 1, 0, duration, completed);

    private static void Run(WinUI.FrameworkElement element, double fromX, double toX, double fromOpacity, double toOpacity, TimeSpan duration, Action completed)
    {
        WinUI.Media.TranslateTransform 移動 = element.RenderTransform is WinUI.Media.TranslateTransform 今の移動 ? 今の移動 : new WinUI.Media.TranslateTransform();
        element.RenderTransform = 移動;
        element.Opacity = fromOpacity;
        WinUI.Media.Animation.Storyboard 動き = new();
        動き.Children.Add(アニメーションを作る(移動, "X", fromX, toX, duration));
        動き.Children.Add(アニメーションを作る(element, "Opacity", fromOpacity, toOpacity, duration));
        動き.Completed += (_, _) => completed();
        動き.Begin();
    }

    private static WinUI.Media.Animation.DoubleAnimation アニメーションを作る(WinUI.DependencyObject 対象, string 動かす項目, double 始め, double 終わり, TimeSpan 時間)
    {
        WinUI.Media.Animation.DoubleAnimation アニメーション = new() { From = 始め, To = 終わり, Duration = new WinUI.Duration(時間) };
        WinUI.Media.Animation.Storyboard.SetTarget(アニメーション, 対象);
        WinUI.Media.Animation.Storyboard.SetTargetProperty(アニメーション, 動かす項目);
        return アニメーション;
    }
}
