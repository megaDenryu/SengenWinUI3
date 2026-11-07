using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>全部の部品に共通のチェーン。どれも受けた部品をそのままの型で返す。</summary>
public static class NodeExtensions
{
    /// <summary>見た目を当てる。同じ部品に2回当てると、後の Style の null でない項目で上書きする。部品が持たない項目を当てると例外を投げる。</summary>
    public static T Style<T>(this T node, Style style) where T : Node
    {
        node.ThrowIfDestroyed();
        node.ApplyStyle(style);
        return node;
    }

    /// <summary>チェーンを切らずに、部品を受け取る処理を呼ぶ。フィールドへ部品を受けるときに使う。</summary>
    public static T Tap<T>(this T node, Action<T> action) where T : Node
    {
        action(node);
        return node;
    }

    /// <summary>見せるか隠すかを決める。隠した部品は場所も取らない。</summary>
    public static T Visible<T>(this T node, bool visible) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.Visibility 見せ方 = visible ? WinUI.Visibility.Visible : WinUI.Visibility.Collapsed;
        if (node.Element.Visibility != 見せ方)
        {
            node.Element.Visibility = 見せ方;
        }
        return node;
    }

    /// <summary>ポインターを重ねたときに出す説明の文字を付ける。</summary>
    public static T ToolTip<T>(this T node, string text) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.Controls.ToolTipService.SetToolTip(node.Element, text);
        return node;
    }
}
