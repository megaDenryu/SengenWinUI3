using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>文字の項目と区切り線を並べ、ContextFlyout チェーンで部品に付ける。</summary>
public sealed class MenuFlyout : FloatingNode, IFlyoutNode
{
    private static readonly EventBinding<WinUI.Controls.MenuFlyoutItem, WinUI.RoutedEventHandler, EventArgs> 押された出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.Click += ハンドラ,
        static (本物, ハンドラ) => 本物.Click -= ハンドラ);

    private readonly WinUI.Controls.MenuFlyout 本物 = new();

    internal MenuFlyout(MenuFlyoutEntry[] items)
    {
        try
        {
            foreach (MenuFlyoutEntry 項目 in items)
            {
                本物.Items.Add(項目 switch
                {
                    MenuFlyoutAction 操作 => 押せる項目を作る(操作),
                    MenuFlyoutSeparator => new WinUI.Controls.MenuFlyoutSeparator(),
                });
            }
        }
        catch
        {
            Destroy();
            throw;
        }
    }

    WinUI.Controls.Primitives.FlyoutBase IFlyoutNode.FlyoutElement => 本物;

    /// <summary>開いていれば閉じる。</summary>
    public void Hide() { ThrowIfDestroyed(); 本物.Hide(); }

    // 項目の購読は、OnDestroying の前に寿命の後片付けで解けている。
    internal override void OnDestroying()
    {
        CollectedExceptions 集めた例外 = new();
        集めた例外.Run(本物.Hide);
        集めた例外.Run(本物.Items.Clear);
        集めた例外.ThrowIfAny();
    }

    private WinUI.Controls.MenuFlyoutItem 押せる項目を作る(MenuFlyoutAction 操作)
    {
        WinUI.Controls.MenuFlyoutItem 項目 = new() { Text = 操作.Text };
        this.WireEventOfOwnElement(項目, 押された出来事, 操作.Handler);
        return 項目;
    }
}
