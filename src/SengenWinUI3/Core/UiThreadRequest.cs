using Microsoft.UI.Dispatching;

namespace SengenWinUI3;

// UI スレッドで1つの処理を呼ぶ依頼。UI スレッドからの依頼はその場で呼ぶ。別のスレッドからの依頼は、まだ送っていないときだけ待ち行列へ積み、
// 送った先で印を下ろしてから処理を呼ぶ。書き換えが続いても待ち行列が膨らまず、印を下ろした後の依頼は次の1回にまとまる。依頼のたびに割り当てをしない。
internal sealed class UiThreadRequest
{
    private readonly UiThread UIスレッド;
    private readonly Action 処理;
    private readonly DispatcherQueueHandler 送った先の処理;
    private int 送り待ち; // 1 なら待ち行列へ積んで、まだ呼んでいない

    internal UiThreadRequest(UiThread UIスレッド, Action 処理)
    {
        this.UIスレッド = UIスレッド;
        this.処理 = 処理;
        送った先の処理 = 送った先で呼ぶ;
    }

    internal void Request()
    {
        if (UIスレッド.IsCurrent)
        {
            処理();
            return;
        }
        if (Interlocked.Exchange(ref 送り待ち, 1) == 0)
        {
            UIスレッド.Post(送った先の処理);
        }
    }

    private void 送った先で呼ぶ()
    {
        Volatile.Write(ref 送り待ち, 0);
        処理();
    }
}
