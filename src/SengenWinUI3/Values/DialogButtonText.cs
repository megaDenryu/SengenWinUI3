namespace SengenWinUI3;

/// <summary>ダイアログに表示するボタンの文字。空白だけの文字は受けない。</summary>
public sealed class DialogButtonText
{
    /// <summary>ボタンに表示する文字。</summary>
    public string Text { get; }

    private DialogButtonText(string text) => Text = text;

    /// <summary>空白だけの文字を受けず、表示する文字か失敗を返す。</summary>
    public static DialogButtonTextResult TryFromText(string text) =>
        string.IsNullOrWhiteSpace(text) ? new DialogButtonTextError() : new DialogButtonText(text);
}
