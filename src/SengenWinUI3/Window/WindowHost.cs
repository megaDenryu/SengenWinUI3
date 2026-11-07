using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 1つのウインドウに付ける状態の持ち主。今は根の部品の置き場を持つ。トーストの置き場もここへ足す。
// 根は持ち主を持つ部品として扱われるので、ほかの親へ入れると例外になる。ウインドウが閉じたら根を破棄する。
internal sealed class WindowHost : IChildOwner
{
    private readonly ContentSlot 根の置き場;

    internal WindowHost(WinUI.Window ウインドウ)
    {
        根の置き場 = new ContentSlot(this, 根の本物 => ウインドウ.Content = 根の本物);
        ウインドウ.Closed += 根を破棄する;
    }

    // 2回目は、新しい根を置いた後に前の根を破棄する。
    internal void SetRoot(Node root) => 根の置き場.SetContent(root);

    void IChildOwner.AdoptChild(Node child, Action 本物へ入れる) => child.BecomeChildOf(this, 本物へ入れる);

    void IChildOwner.ReleaseChild(Node child) => 根の置き場.Release(child);

    private void 根を破棄する(object sender, WinUI.WindowEventArgs 出来事) => 根の置き場.DestroyContent();
}
