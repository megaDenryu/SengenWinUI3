using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// トーストを出す層。ウインドウの XamlRoot に付けた Popup と、その中でトーストを積む StackPanel を持つ。
// Popup の中身は何も塗らないので、トーストの無い所では後ろの画面を押せる。画面の中身を入れ替えても、Popup は残る。
internal sealed class ToastLayer
{
    private readonly WinUI.Window ウインドウ;
    private readonly ToastHostSettings 設定;
    private readonly WinUI.Controls.Primitives.Popup ポップアップ = new();
    private readonly WinUI.Controls.Grid 全面 = new();
    private readonly WinUI.Controls.StackPanel 積む場所 = new();
    private WinUI.XamlRoot? 見ているXamlRoot; // 開いたときだけある

    internal ToastLayer(WinUI.Window window, ToastHostSettings settings)
    {
        ウインドウ = window;
        設定 = settings;
        積む場所.Spacing = settings.Spacing.Value;
        積む場所.MaxWidth = settings.MaxWidth.Value;
        積む場所.HorizontalAlignment = IsRight ? WinUI.HorizontalAlignment.Right : WinUI.HorizontalAlignment.Left;
        積む場所.VerticalAlignment = IsTop ? WinUI.VerticalAlignment.Top : WinUI.VerticalAlignment.Bottom;
        積む場所.Margin = new WinUI.Thickness(IsRight ? 0 : settings.EdgeMargin.Value, IsTop ? settings.EdgeMargin.Value : 0, IsRight ? settings.EdgeMargin.Value : 0, IsTop ? 0 : settings.EdgeMargin.Value);
        全面.Children.Add(積む場所);
        ポップアップ.Child = 全面;
    }

    internal bool IsOpen => ポップアップ.IsOpen;

    // 滑って出入りする向きと距離。右の隅なら右へ、左の隅なら左へ、幅と余白の分だけ滑る。
    internal double SlideDistance => (設定.MaxWidth.Value + 設定.EdgeMargin.Value) * (IsRight ? 1 : -1);

    private bool IsRight => 設定.Position is ToastPosition.TopRight or ToastPosition.BottomRight;

    private bool IsTop => 設定.Position is ToastPosition.TopRight or ToastPosition.TopLeft;

    // 画面の中身がまだ無い間は開けない。開けるようになったときに、この操作をもう一度呼ぶ。
    // 開いた後に別のポップアップ(確認の画面など)が開いていれば、開き直して一番上へ出す。
    internal void EnsureOpen()
    {
        if (ウインドウ.Content?.XamlRoot is not { } root)
        {
            return;
        }
        if (!ポップアップ.IsOpen)
        {
            見ているXamlRoot = root;
            ポップアップ.XamlRoot = root;
            root.Changed += 大きさを合わせる;
            大きさを合わせる(root, null);
            ポップアップ.IsOpen = true;
        }
        else if (WinUI.Media.VisualTreeHelper.GetOpenPopupsForXamlRoot(root).Count > 1)
        {
            ポップアップ.IsOpen = false;
            ポップアップ.IsOpen = true;
        }
    }

    internal void Add(WinUI.FrameworkElement element)
    {
        if (設定.StackOrder == ToastStackOrder.NewestAtTop)
        {
            積む場所.Children.Insert(0, element);
            return;
        }
        積む場所.Children.Add(element);
    }

    internal void Remove(WinUI.FrameworkElement element) => _ = 積む場所.Children.Remove(element);

    internal void Close()
    {
        ポップアップ.IsOpen = false;
        if (見ているXamlRoot is { } root)
        {
            root.Changed -= 大きさを合わせる;
        }
    }

    private void 大きさを合わせる(WinUI.XamlRoot root, WinUI.XamlRootChangedEventArgs? 出来事)
    {
        全面.Width = root.Size.Width;
        全面.Height = root.Size.Height;
    }
}
