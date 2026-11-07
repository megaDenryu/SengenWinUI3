using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 1つのウインドウに付ける状態の持ち主。根の部品の置き場とトースト置き場を持つ。
// 根は持ち主を持つ部品として扱われるので、ほかの親へ入れると例外になる。ウインドウが閉じたら根を破棄する。
internal sealed class WindowHost : IChildOwner
{
    private readonly WinUI.Window ウインドウ;
    private readonly ContentSlot 根置き場;
    private ToastHost? トースト置き場; // 付けたときだけある

    internal WindowHost(WinUI.Window ウインドウ)
    {
        this.ウインドウ = ウインドウ;
        根置き場 = new ContentSlot(this, 根の本物 => ウインドウ.Content = 根の本物);
        ウインドウ.Closed += 閉じたら後片付けをする;
    }

    // 2回目は、新しい根を置いた後に前の根を破棄する。
    internal void SetRoot(Node root)
    {
        根置き場.SetContent(root);
        root.Element.Loaded += 根が読み込まれたら呼ぶ;
    }

    // 1つのウインドウに1つだけ付けられる。
    internal ToastHost AttachToastHost(ToastHostSettings settings)
    {
        if (トースト置き場 is not null)
        {
            throw new InvalidOperationException("このウインドウには、すでにトースト置き場を付けてある");
        }
        ToastHost 付ける置き場 = new(ウインドウ, settings);
        トースト置き場 = 付ける置き場;
        return 付ける置き場;
    }

    void IChildOwner.AdoptChild(Node child, Action 本物へ入れる) => child.BecomeChildOf(this, 本物へ入れる);

    void IChildOwner.ReleaseChild(Node child) => 根置き場.Release(child);

    private void 根が読み込まれたら呼ぶ(object sender, WinUI.RoutedEventArgs 出来事) => トースト置き場?.ContentPlaced();

    private void 閉じたら後片付けをする(object sender, WinUI.WindowEventArgs 出来事)
    {
        トースト置き場?.Shutdown();
        根置き場.DestroyContent();
    }
}
