namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>見出し、中身、3種類のボタンの表示を指定して確認の画面を作る。</summary>
    public static ContentDialog ContentDialog(string title, Node content, ContentDialogButtons buttons) =>
        new(title, content, buttons);
}
