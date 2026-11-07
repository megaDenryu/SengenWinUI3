using SengenWinUI3.State;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>文字を見せる部品。固定の文字か、セルの文字を見せる。作る関数は <see cref="UI.TextBlock(string, Style?)"/>。</summary>
public sealed class TextBlock : Node
{
    private static readonly StyleTarget<WinUI.Controls.TextBlock> 当て方 =
        new(FrameworkElementStyle.Layout, TextBlockStyle.Padding, TextBlockStyle.Font, TextBlockStyle.Text);

    private readonly WinUI.Controls.TextBlock 本物 = new();
    private readonly BindableValue<WinUI.Controls.TextBlock, string> 文字;

    internal TextBlock(string text) : this() => 文字.Set(text);

    internal TextBlock(IReadOnlyCell<string> text) : this() => 文字.Bind(text);

    private TextBlock() => 文字 = new(this, 本物, static (本物, 見せる文字) => 本物.Text == 見せる文字, static (本物, 見せる文字) => 本物.Text = 見せる文字);

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>見せる文字を替える。セルに結んだ TextBlock へ呼ぶと、値の出どころが2つになるので例外を投げる。</summary>
    public TextBlock SetText(string text)
    {
        ThrowIfDestroyed();
        文字.Set(text);
        return this;
    }

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);
}
