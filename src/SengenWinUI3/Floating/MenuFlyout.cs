using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>文字の項目と区切り線を並べ、ContextFlyout チェーンで部品に付ける。</summary>
public sealed class MenuFlyout : FloatingNode
{
    private static readonly EventBinding<WinUI.Controls.MenuFlyoutItem, WinUI.RoutedEventHandler, EventArgs> 押された出来事 = new(
        static (本物, 呼ぶ) => (送信元, 出来事) => 呼ぶ(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.Click += ハンドラ,
        static (本物, ハンドラ) => 本物.Click -= ハンドラ);

    private readonly CleanupList 項目の購読 = new();

    internal MenuFlyout(MenuFlyoutEntry[] items)
    {
        try
        {
            foreach (MenuFlyoutEntry 項目 in items)
            {
                Element.Items.Add(項目 switch
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

    internal WinUI.Controls.MenuFlyout Element { get; } = new();

    /// <summary>開いていれば閉じる。</summary>
    public void Hide() { ThrowIfDestroyed(); Element.Hide(); }

    internal override void OnDestroying()
    {
        CollectedExceptions 起きた例外 = new();
        起きた例外.Run(Element.Hide);
        項目の購読.DisposeAll(起きた例外);
        起きた例外.Run(Element.Items.Clear);
        起きた例外.ThrowIfAny();
    }

    private protected override void ReleaseContent(Node content) =>
        throw new InvalidOperationException("MenuFlyout は Node の中身を持たない");

    private WinUI.Controls.MenuFlyoutItem 押せる項目を作る(MenuFlyoutAction 操作)
    {
        WinUI.Controls.MenuFlyoutItem 項目 = new() { Text = 操作.Text };
        項目の購読.Add(押された出来事.Subscribe(項目, _ =>
        {
            if (!IsDestroyed) 操作.Handler();
        }));
        return 項目;
    }
}
