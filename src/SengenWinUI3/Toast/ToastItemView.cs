using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 1件のトーストの画面。左から、印・文と詳細・(あれば)ボタン・(あれば)閉じるボタンを並べる。UI スレッドで作って使う。
internal sealed class ToastItemView
{
    private static readonly Thickness 既定の内側の余白 = new(14.Px, 18.Px);

    private readonly WinUI.Controls.Border 外枠 = new();
    private readonly WinUI.Controls.TextBlock 文 = new() { TextWrapping = WinUI.TextWrapping.Wrap };
    private readonly WinUI.Controls.TextBlock 詳細 = new() { TextWrapping = WinUI.TextWrapping.Wrap, Opacity = 0.8 };

    internal ToastItemView(ToastMessage message, string text, ToastDetail detail, ToastHostSettings settings, bool showCloseButton, Action close)
    {
        Style 見た目 = settings.KindStyles.Of(message.Kind).Style;
        double 文字の大きさ = (見た目.FontSize ?? 14.Px).Value;
        WinUI.Media.Brush? 文字の色 = 見た目.CachedForeground();
        外枠.Background = 見た目.CachedBackground();
        外枠.BorderBrush = 見た目.CachedBorderBrush();
        外枠.BorderThickness = (見た目.BorderThickness ?? new Thickness(1.Px)).ToWinUI();
        外枠.CornerRadius = new WinUI.CornerRadius((見た目.CornerRadius ?? 8.Px).Value);
        外枠.Padding = (見た目.Padding ?? 既定の内側の余白).ToWinUI();
        外枠.MinWidth = settings.MinWidth.Value;
        外枠.MaxWidth = settings.MaxWidth.Value;
        文.FontSize = 文字の大きさ;
        詳細.FontSize = 文字の大きさ - 2;
        if (文字の色 is not null)
        {
            文.Foreground = 文字の色;
            詳細.Foreground = 文字の色;
        }
        SetText(text);
        SetDetail(detail);
        外枠.Child = 並べる(message, settings, 文字の色, showCloseButton, close);
    }

    internal WinUI.FrameworkElement Element => 外枠;

    internal void SetText(string text) => 文.Text = text;

    internal void SetDetail(ToastDetail detail)
    {
        _ = detail switch
        {
            ToastDetailText 文あり => 詳細を見せる(文あり.Text),
            NoToastDetail => 詳細を隠す(),
        };
    }

    private WinUI.Controls.Grid 並べる(ToastMessage message, ToastHostSettings settings, WinUI.Media.Brush? 文字の色, bool showCloseButton, Action close)
    {
        WinUI.Controls.Grid 並び = new();
        WinUI.Controls.TextBlock 印 = new()
        {
            Text = settings.KindStyles.Of(message.Kind).Mark,
            FontWeight = Microsoft.UI.Text.FontWeights.Bold,
            Margin = new WinUI.Thickness(0, 0, 12, 0),
            VerticalAlignment = WinUI.VerticalAlignment.Top,
            FontSize = 文.FontSize,
        };
        if (文字の色 is not null)
        {
            印.Foreground = 文字の色;
        }
        WinUI.Controls.StackPanel 文の並び = new() { Spacing = 4 };
        文の並び.Children.Add(文);
        文の並び.Children.Add(詳細);
        列を足して置く(並び, 印, WinUI.GridLength.Auto);
        列を足して置く(並び, 文の並び, new WinUI.GridLength(1, WinUI.GridUnitType.Star));
        if (message.Button is ToastButtonContent ボタンの内容)
        {
            列を足して置く(並び, ToastItemButtons.CreateAction(ボタンの内容.Text, ボタンの内容.OnClick, close), WinUI.GridLength.Auto);
        }
        if (showCloseButton)
        {
            列を足して置く(並び, ToastItemButtons.CreateClose(文字の色, close), WinUI.GridLength.Auto);
        }
        return 並び;
    }

    private static void 列を足して置く(WinUI.Controls.Grid 並び, WinUI.FrameworkElement 部品, WinUI.GridLength 幅)
    {
        並び.ColumnDefinitions.Add(new WinUI.Controls.ColumnDefinition { Width = 幅 });
        WinUI.Controls.Grid.SetColumn(部品, 並び.ColumnDefinitions.Count - 1);
        並び.Children.Add(部品);
    }

    private bool 詳細を見せる(string 詳細の文)
    {
        詳細.Text = 詳細の文;
        詳細.Visibility = WinUI.Visibility.Visible;
        return true;
    }

    private bool 詳細を隠す()
    {
        詳細.Visibility = WinUI.Visibility.Collapsed;
        return false;
    }
}
