using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// ウインドウに1つ付け、トーストを積んで出す置き場。作るのは <c>window.AttachToastHost(settings)</c>。
/// 画面の中身やポップアップより上に出て、トーストの無い所では後ろの画面を押せる。<see cref="Show"/> と <see cref="CloseAll"/> はどのスレッドから呼んでもよい。
/// </summary>
public sealed class ToastHost
{
    private readonly UiThread UIスレッド;
    private readonly ToastHostSettings 設定;
    private readonly ToastLayer 層;
    private readonly List<ToastEntry> 出している件List = [];
    private bool 終えたか;

    internal ToastHost(WinUI.Window window, ToastHostSettings settings)
    {
        UIスレッド = UiThread.ForCurrentThread();
        設定 = settings;
        層 = new ToastLayer(window, settings);
        層.EnsureOpen();
    }

    /// <summary>トーストを出し、後で閉じる・書き換えるための札を返す。UI スレッドでないときは、UI スレッドへ送って出す。</summary>
    public ToastHandle Show(ToastMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        ToastEntry 出す1件 = new(message, 設定, 層, 閉じ始めた件を外す);
        RunOnUiThread(() => 出す(出す1件));
        return new ToastHandle(出す1件, this);
    }

    /// <summary>出しているトーストを全部閉じる。</summary>
    public void CloseAll() => RunOnUiThread(全部閉じる);

    internal void RunOnUiThread(Action action)
    {
        if (UIスレッド.IsCurrent)
        {
            action();
            return;
        }
        UIスレッド.Post(() => action());
    }

    // 画面の中身が置かれたとき(ウインドウの根が読み込まれたとき)に、まだ開けていなければ開く。
    internal void ContentPlaced() => 層.EnsureOpen();

    // ウインドウが閉じたときに、動きを待たずに片付ける。
    internal void Shutdown()
    {
        終えたか = true;
        foreach (ToastEntry 件 in 出している件List)
        {
            件.Abandon();
        }
        出している件List.Clear();
        層.Close();
    }

    private void 出す(ToastEntry 出す1件)
    {
        if (終えたか || !出す1件.Show())
        {
            return;
        }
        出している件List.Add(出す1件);
        層.EnsureOpen();
        while (出している件List.Count > 上限の件数())
        {
            出している件List[0].Close();
        }
    }

    private int 上限の件数() => 設定.CountLimit switch
    {
        LimitedToasts 上限あり => 上限あり.Count,
        UnlimitedToasts => int.MaxValue,
    };

    private void 全部閉じる()
    {
        foreach (ToastEntry 件 in 出している件List.ToArray())
        {
            件.Close();
        }
    }

    private void 閉じ始めた件を外す(ToastEntry 件) => _ = 出している件List.Remove(件);
}
