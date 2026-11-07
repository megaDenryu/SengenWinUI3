using SengenWinUI3.State;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>文字を見せる部品。固定の文字か、セルの文字を見せる。作る関数は <see cref="UI.TextBlock(string, Style?)"/>。</summary>
public sealed class TextBlock : Node
{
    private const StyleItems 持つ項目 = StyleItems.Layout | StyleItems.Padding | StyleItems.Foreground | StyleItems.FontSize
        | StyleItems.FontWeight | StyleItems.TextWrapping | StyleItems.IsTextSelectionEnabled;

    private readonly WinUI.Controls.TextBlock 本物 = new();
    private readonly bool セルに結んだか;
    private string 見せている文字 = ""; // 本物の Text を読むと WinRT の境目で文字列を作るので、本物へ設定した文字を手元にも持つ

    internal TextBlock(string text) => 文字を見せる(text);

    internal TextBlock(IReadOnlyCell<string> text)
    {
        セルに結んだか = true;
        SubscribeCell(text, 文字を見せる);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>見せる文字を替える。セルに結んだ TextBlock へ呼ぶと、値の出どころが2つになるので例外を投げる。</summary>
    public TextBlock SetText(string text)
    {
        ThrowIfDestroyed();
        if (セルに結んだか)
        {
            throw new InvalidOperationException("セルに結んだ TextBlock の文字は、セルの値を変えて替える");
        }
        文字を見せる(text);
        return this;
    }

    internal override void ApplyStyle(Style style)
    {
        style.ThrowIfUnsupported(持つ項目, this);
        style.ApplyLayout(本物);
        if (style.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        if (style.ForegroundBrush() is { } 文字の色) 本物.Foreground = 文字の色;
        if (style.FontSize is { } 文字の大きさ) 本物.FontSize = 文字の大きさ.Value;
        if (style.FontWeight is { } 文字の太さ) 本物.FontWeight = 文字の太さ.ToWinUI();
        if (style.TextWrapping is { } 折り返すか) 本物.TextWrapping = 折り返すか ? WinUI.TextWrapping.Wrap : WinUI.TextWrapping.NoWrap;
        if (style.IsTextSelectionEnabled is { } 選べるか) 本物.IsTextSelectionEnabled = 選べるか;
    }

    private void 文字を見せる(string 文字)
    {
        if (文字 == 見せている文字)
        {
            return;
        }
        見せている文字 = 文字;
        本物.Text = 文字;
    }
}
