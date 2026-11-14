using Windows.Graphics;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>アプリの主ウインドウとは別に出す、OS のウインドウ。根の部品を1つ持ち、ウインドウが閉じたときに根を破棄する。</summary>
public sealed class SecondaryWindow
{
    private readonly WinUI.Window 本物 = new();
    private readonly Action<WindowClosingRequest> 閉じようとしたときに呼ぶ処理;
    private bool 破棄したか;

    /// <summary>ウインドウを作る。まだ表示しない。表示は <see cref="Show"/> で行う。親を持つ部品は <paramref name="content"/> に渡せない。<paramref name="onClosing"/> は、利用者や OS がウインドウを閉じようとしたときの処理で、<see cref="WindowClosingRequest.Cancel"/> を呼ばなければ閉じる。<see cref="Destroy"/> による閉じ方では呼ばれない。</summary>
    public SecondaryWindow(string title, ViewSize initialSize, Node content, Action<WindowClosingRequest> onClosing)
    {
        閉じようとしたときに呼ぶ処理 = onClosing;
        本物.Title = title;
        本物.AppWindow.Resize(new SizeInt32(Convert.ToInt32(initialSize.Width.Value), Convert.ToInt32(initialSize.Height.Value)));
        本物.SetContent(content);
        本物.AppWindow.Closing += On閉じようとした;
        本物.Closed += On閉じた;
    }

    /// <summary>閉じたか、<see cref="Destroy"/> を呼んだか。</summary>
    public bool IsDestroyed => 破棄したか;

    /// <summary>ウインドウの題名を替える。破棄した後には呼べない。</summary>
    public SecondaryWindow SetTitle(string title)
    {
        破棄済みなら例外を投げる();
        本物.Title = title;
        return this;
    }

    /// <summary>ウインドウを表示して手前へ出す。隠していたウインドウも出す。破棄した後には呼べない。</summary>
    public SecondaryWindow Show()
    {
        破棄済みなら例外を投げる();
        本物.AppWindow.Show();
        本物.Activate();
        return this;
    }

    /// <summary>ウインドウを隠す。根と購読は残る。破棄した後には呼べない。</summary>
    public SecondaryWindow Hide()
    {
        破棄済みなら例外を投げる();
        本物.AppWindow.Hide();
        return this;
    }

    /// <summary>ウインドウを閉じて根を破棄する。2回目は何もしない。</summary>
    public void Destroy()
    {
        if (破棄したか) return;
        破棄したか = true;
        本物.Close();
    }

    private void On閉じようとした(Microsoft.UI.Windowing.AppWindow 閉じるウインドウ, Microsoft.UI.Windowing.AppWindowClosingEventArgs 出来事)
    {
        if (破棄したか) return;
        閉じようとしたときに呼ぶ処理(new WindowClosingRequest(() => 出来事.Cancel = true));
    }

    private void On閉じた(object sender, WinUI.WindowEventArgs 出来事) => 破棄したか = true;

    private void 破棄済みなら例外を投げる()
    {
        if (破棄したか) throw new InvalidOperationException("SecondaryWindow は破棄した後に操作された");
    }
}
