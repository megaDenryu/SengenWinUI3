using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品の根で、値の変更にかかる時間を WinUI の暗黙の動きへ渡すチェーン。</summary>
public static class TransitionExtensions
{
    /// <summary>不透明度の変更にかかる時間を設定する。再設定でき、不透明度そのものは Style で変える。</summary>
    public static T OpacityTransition<T>(this T node, TimeSpan duration) where T : Node
    {
        node.ThrowIfDestroyed();
        node.Element.OpacityTransition = new WinUI.ScalarTransition { Duration = duration };
        return node;
    }

    /// <summary>横縦と奥行きの位置変更にかかる時間を設定する。再設定できる。</summary>
    public static T TranslationTransition<T>(this T node, TimeSpan duration) where T : Node
    {
        node.ThrowIfDestroyed();
        node.Element.TranslationTransition = new WinUI.Vector3Transition { Duration = duration };
        return node;
    }

    /// <summary>拡大率の変更にかかる時間を設定する。再設定できる。</summary>
    public static T ScaleTransition<T>(this T node, TimeSpan duration) where T : Node
    {
        node.ThrowIfDestroyed();
        node.Element.ScaleTransition = new WinUI.Vector3Transition { Duration = duration };
        return node;
    }
}
