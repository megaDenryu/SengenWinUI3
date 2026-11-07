using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>Canvas の子の左端と上端の位置を指定するチェーン。元の具象型を保つ。</summary>
public static class CanvasPlacementExtensions
{
    /// <summary>Canvas の左端からの位置を指定する。</summary>
    public static T CanvasLeft<T>(this T node, Px left) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.Controls.Canvas.SetLeft(node.Element, left.Value);
        return node;
    }

    /// <summary>Canvas の上端からの位置を指定する。</summary>
    public static T CanvasTop<T>(this T node, Px top) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.Controls.Canvas.SetTop(node.Element, top.Value);
        return node;
    }
}
