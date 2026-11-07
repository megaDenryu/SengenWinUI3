using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// ウインドウに置いた根の部品の持ち主。根は親を持つ部品として扱われ、ほかの親へ入れると例外になる。ウインドウが閉じたら根を破棄する。
internal sealed class WindowRoot : IChildOwner
{
    private readonly WinUI.Window ウインドウ;
    private readonly Node 根;
    private readonly Action 手放したら知らせる;

    internal WindowRoot(WinUI.Window ウインドウ, Node 根, Action 手放したら知らせる)
    {
        this.ウインドウ = ウインドウ;
        this.根 = 根;
        this.手放したら知らせる = 手放したら知らせる;
        根.SetOwner(this);
        ウインドウ.Content = 根.Element;
        ウインドウ.Closed += 閉じたら根を破棄する;
    }

    internal void DestroyRoot() => 根.Destroy();

    void IChildOwner.ReleaseChild(Node child)
    {
        ウインドウ.Closed -= 閉じたら根を破棄する;
        ウインドウ.Content = null;
        手放したら知らせる();
    }

    private void 閉じたら根を破棄する(object sender, WinUI.WindowEventArgs 出来事) => 根.Destroy();
}
