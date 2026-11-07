namespace SengenWinUI3;

/// <summary>
/// 出したトーストを、後で閉じる・文を書き換えるための値。どのスレッドから呼んでもよく、UI スレッドでないときは置き場が UI スレッドへ送る。
/// すでに閉じたトーストへの操作は何もしない。
/// </summary>
public sealed class ToastHandle
{
    private readonly ToastEntry 出した1件;
    private readonly ToastHost 置き場;

    internal ToastHandle(ToastEntry entry, ToastHost host)
    {
        出した1件 = entry;
        置き場 = host;
    }

    /// <summary>トーストを閉じる。</summary>
    public void Close() => 置き場.RunOnUiThread(出した1件.Close);

    /// <summary>文を書き換える。</summary>
    public void SetText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        置き場.RunOnUiThread(() => 出した1件.SetText(text));
    }

    /// <summary>詳細の文を書き換える。詳細を持たずに出したトーストには、詳細を足す。</summary>
    public void SetDetail(string detail)
    {
        ArgumentNullException.ThrowIfNull(detail);
        置き場.RunOnUiThread(() => 出した1件.SetDetail(detail));
    }
}
