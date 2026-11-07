using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>文字だけの説明を部品に付ける。</summary>
public static class ToolTipExtensions
{
    /// <summary>説明を付け、部品と一緒に破棄する。同じ部品への2回目の登録は例外を投げる。</summary>
    public static T ToolTip<T>(this T node, string text) where T : Node
    {
        node.ThrowIfDestroyed();
        node.MarkWiredOnce(nameof(ToolTip));
        ToolTip 説明 = new(text);
        WinUI.Controls.ToolTipService.SetToolTip(node.Element, 説明.Element);
        説明.AttachTo(node, () =>
        {
            WinUI.Controls.ToolTipService.SetToolTip(node.Element, null);
            node.UnmarkWired(nameof(ToolTip));
        });
        return node;
    }
}
