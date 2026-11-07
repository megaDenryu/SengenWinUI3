using Microsoft.UI.Dispatching;
using WinUI = Microsoft.UI.Xaml;

namespace Gallery;

// 場面を動かして確かめる道具。結果を書く・破棄されたことを書く・自動のときに手順を順に呼んでウインドウを閉じる。
internal sealed class SceneRun(WinUI.Window ウインドウ, Action<string> 結果を書く, bool 自動で進めるか)
{
    private static readonly TimeSpan 手順の間 = TimeSpan.FromMilliseconds(500);

    private readonly List<Func<string>> 閉じた後に書く文List = [];

    internal void Report(string 文) => 結果を書く(文);

    internal IDisposable ReportOnDispose(string 文) => new 破棄されたら書く札(this, 文);

    // 自動のとき、ウインドウを閉じた後(根を破棄した後)に文を作って書く。破棄で起きたことを確かめるためである。
    internal void ReportAfterClose(Func<string> 文を作る) => 閉じた後に書く文List.Add(文を作る);

    // 自動のときだけ、手順の間を空けて順に呼び、最後にウインドウを閉じる。間を空けるのは、前の手順の後の配置と描画を済ませるためである。
    internal void RunSteps(params Action[] 手順List)
    {
        if (!自動で進めるか)
        {
            return;
        }
        DispatcherQueueTimer 時計 = DispatcherQueue.GetForCurrentThread().CreateTimer();
        時計.Interval = 手順の間;
        int 次の手順 = 0;
        時計.Tick += (_, _) =>
        {
            if (次の手順 < 手順List.Length)
            {
                手順を呼ぶ(手順List[次の手順++]);
                return;
            }
            時計.Stop();
            Report("手順を終えたのでウインドウを閉じる");
            ウインドウ.Close();
            foreach (Func<string> 文を作る in 閉じた後に書く文List)
            {
                Report(文を作る());
            }
        };
        時計.Start();
    }

    // 注意: 1つの手順の例外で残りの手順と閉じる操作を止めず、例外を結果に書いて続ける。
    private void 手順を呼ぶ(Action 手順)
    {
        try
        {
            手順();
        }
        catch (Exception 例外)
        {
            Report($"手順で例外: {例外}");
        }
    }

    private sealed class 破棄されたら書く札(SceneRun 場面の進め方, string 文) : IDisposable
    {
        public void Dispose() => 場面の進め方.Report(文);
    }
}
