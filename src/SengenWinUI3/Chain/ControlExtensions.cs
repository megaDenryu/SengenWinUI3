namespace SengenWinUI3;

/// <summary>本物が Control の部品(<see cref="IControlNode"/>)のチェーン。使えるかどうかとフォーカスは見た目でなく状態なので、Style に入れない。</summary>
public static class ControlExtensions
{
    /// <summary>利用者が操作できるかを決める。</summary>
    public static T IsEnabled<T>(this T node, bool isEnabled) where T : Node, IControlNode
    {
        node.ThrowIfDestroyed();
        if (node.Control.IsEnabled != isEnabled)
        {
            node.Control.IsEnabled = isEnabled;
        }
        return node;
    }

    /// <summary>キーボードのフォーカスをこの部品へ移す。移せたかを返す。</summary>
    public static bool Focus<T>(this T node) where T : Node, IControlNode
    {
        node.ThrowIfDestroyed();
        return node.Control.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
    }
}
