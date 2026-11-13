using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品の表示を長方形で切り抜くチェーン。元の具象型を保つ。</summary>
public static class ClipExtensions
{
    /// <summary>
    /// 部品の表示を <paramref name="rect"/> の範囲に切り抜く。範囲は部品の左上からの長さで、範囲の外は描かれず、ポインターの出来事も受けない。
    /// 繰り返し呼ぶと、前の範囲を新しい範囲で置き換える。
    /// </summary>
    public static T Clip<T>(this T node, Rect rect) where T : Node
    {
        node.ThrowIfDestroyed();
        node.Element.Clip = new WinUI.Media.RectangleGeometry { Rect = rect.ToWinUI() };
        return node;
    }
}
