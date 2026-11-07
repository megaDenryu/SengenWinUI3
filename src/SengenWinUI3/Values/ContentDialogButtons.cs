namespace SengenWinUI3;

/// <summary>確認の画面の3つのボタン(主・2つ目・閉じる)の表示。3つとも名前を付けて指定するので、順序を取り違えない。</summary>
public sealed class ContentDialogButtons
{
    /// <summary>主の操作のボタン。</summary>
    public required ContentDialogButton Primary { get; init; }

    /// <summary>2つ目の操作のボタン。</summary>
    public required ContentDialogButton Secondary { get; init; }

    /// <summary>閉じるボタン。</summary>
    public required ContentDialogButton Close { get; init; }
}
