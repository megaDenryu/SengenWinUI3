using Microsoft.UI.Dispatching;
using WinUI = Microsoft.UI.Xaml;

namespace Gallery;

// 場面を動かして確かめる道具。結果を書く・破棄されたことを書く・自動のときに手順を順に呼んでウインドウを閉じる。
internal sealed class SceneRun(WinUI.Window ウインドウ, Action<string> 結果を書く, bool 自動で進めるか)
{
    private static readonly TimeSpan 手順の間 = TimeSpan.FromMilliseconds(500);

    private readonly List<Func<string>> 閉じた後に文を作る関数List = [];
    private readonly DispatcherQueueTimer 時計 = DispatcherQueue.GetForCurrentThread().CreateTimer();
    private Action[] 手順List = [];
    private int 次の手順の位置;

    // トースト置き場を付けるなど、場面がウインドウそのものに触るときに使う。
    internal WinUI.Window Window => ウインドウ;

    internal void Report(string 文) => 結果を書く(文);

    internal IDisposable ReportOnDispose(string 文) => new 破棄されたら書く札(this, 文);

    // 自動のとき、ウインドウを閉じた後(根を破棄した後)に文を作って書く。破棄で起きたことを確かめるためである。
    internal void ReportAfterClose(Func<string> 文を作る) => 閉じた後に文を作る関数List.Add(文を作る);

    // 自動のときだけ、手順の間を空けて順に呼び、最後にウインドウを閉じる。間を空けるのは、前の手順の後の配置と描画を済ませるためである。
    // 時計は場面の根でなくウインドウの寿命に合わせて止める。場面が手順の途中で画面の中身を入れ替えても(根を破棄しても)、残りの手順を続けるためである。
    internal void RunSteps(params Action[] 手順List)
    {
        if (!自動で進めるか)
        {
            return;
        }
        this.手順List = 手順List;
        時計.Interval = 手順の間;
        時計.Tick += 次の手順を呼ぶ;
        ウインドウ.Closed += 閉じたら手順を止める;
        時計.Start();
    }

    private void 次の手順を呼ぶ(DispatcherQueueTimer 鳴った時計, object 出来事)
    {
        if (次の手順の位置 < 手順List.Length)
        {
            手順を呼ぶ(手順List[次の手順の位置++]);
            return;
        }
        時計.Stop();
        Report("手順を終えたのでウインドウを閉じる");
        ウインドウ.Close();
        foreach (Func<string> 文を作る in 閉じた後に文を作る関数List)
        {
            Report(文を作る());
        }
    }

    // 利用者が手でウインドウを閉じたときも、残りの手順を呼ばない。
    private void 閉じたら手順を止める(object sender, WinUI.WindowEventArgs 出来事) => 時計.Stop();

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
