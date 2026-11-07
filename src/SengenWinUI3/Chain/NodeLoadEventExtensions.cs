using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品の読み込みと取り外しの出来事。同じ部品の同じ出来事への2回目の登録は例外になる。</summary>
public static class NodeLoadEventExtensions
{
    /// <summary>部品が木へ読み込まれるたびに処理を呼ぶ。登録は部品の破棄で解く。</summary>
    public static T OnLoaded<T>(this T node, Action handler) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        WinUI.RoutedEventHandler 出来事が起きたら呼ぶ = (_, _) =>
        {
            if (!node.IsDestroyed) handler();
        };
        node.WireEvent(nameof(OnLoaded), () => 本物.Loaded += 出来事が起きたら呼ぶ, () => 本物.Loaded -= 出来事が起きたら呼ぶ);
        return node;
    }

    /// <summary>部品が木から取り外されるたびに処理を呼ぶ。登録は部品の破棄で解く。</summary>
    public static T OnUnloaded<T>(this T node, Action handler) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        WinUI.RoutedEventHandler 出来事が起きたら呼ぶ = (_, _) =>
        {
            if (!node.IsDestroyed) handler();
        };
        node.WireEvent(nameof(OnUnloaded), () => 本物.Unloaded += 出来事が起きたら呼ぶ, () => 本物.Unloaded -= 出来事が起きたら呼ぶ);
        return node;
    }
}
