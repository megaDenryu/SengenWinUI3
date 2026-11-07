using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 説明の本物と寿命だけを持つ。公開の作る関数は設けず、文字だけのチェーンから作る。
internal sealed class ToolTip : FloatingNode
{
    internal ToolTip(string text) => Element.Content = text;

    internal WinUI.Controls.ToolTip Element { get; } = new();

    internal override void OnDestroying()
    {
        Element.IsOpen = false;
        Element.Content = null;
    }

    private protected override void ReleaseContent(Node content) =>
        throw new InvalidOperationException("ToolTip は Node の中身を持たない");
}
