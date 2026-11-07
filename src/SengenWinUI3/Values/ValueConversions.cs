using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 自前の列挙を WinUI の値へ変える。列挙はメソッドを持てないため、拡張メソッドで値の側に付ける。
internal static class ValueConversions
{
    internal static WinUI.HorizontalAlignment ToWinUI(this HorizontalAlignment 寄せ方) => 寄せ方 switch
    {
        HorizontalAlignment.Left => WinUI.HorizontalAlignment.Left,
        HorizontalAlignment.Center => WinUI.HorizontalAlignment.Center,
        HorizontalAlignment.Right => WinUI.HorizontalAlignment.Right,
        HorizontalAlignment.Stretch => WinUI.HorizontalAlignment.Stretch,
        _ => throw new ArgumentOutOfRangeException(nameof(寄せ方), 寄せ方, null),
    };

    internal static WinUI.VerticalAlignment ToWinUI(this VerticalAlignment 寄せ方) => 寄せ方 switch
    {
        VerticalAlignment.Top => WinUI.VerticalAlignment.Top,
        VerticalAlignment.Center => WinUI.VerticalAlignment.Center,
        VerticalAlignment.Bottom => WinUI.VerticalAlignment.Bottom,
        VerticalAlignment.Stretch => WinUI.VerticalAlignment.Stretch,
        _ => throw new ArgumentOutOfRangeException(nameof(寄せ方), 寄せ方, null),
    };

    internal static WinUI.Controls.Orientation ToWinUI(this Orientation 向き) => 向き switch
    {
        Orientation.Vertical => WinUI.Controls.Orientation.Vertical,
        Orientation.Horizontal => WinUI.Controls.Orientation.Horizontal,
        _ => throw new ArgumentOutOfRangeException(nameof(向き), 向き, null),
    };

    internal static WinUI.TextWrapping ToWinUI(this TextWrapping 折り返し方) => 折り返し方 switch
    {
        TextWrapping.NoWrap => WinUI.TextWrapping.NoWrap,
        TextWrapping.Wrap => WinUI.TextWrapping.Wrap,
        _ => throw new ArgumentOutOfRangeException(nameof(折り返し方), 折り返し方, null),
    };

    internal static Windows.UI.Text.FontWeight ToWinUI(this FontWeight 太さ) => 太さ switch
    {
        FontWeight.Thin => Microsoft.UI.Text.FontWeights.Thin,
        FontWeight.ExtraLight => Microsoft.UI.Text.FontWeights.ExtraLight,
        FontWeight.Light => Microsoft.UI.Text.FontWeights.Light,
        FontWeight.SemiLight => Microsoft.UI.Text.FontWeights.SemiLight,
        FontWeight.Normal => Microsoft.UI.Text.FontWeights.Normal,
        FontWeight.Medium => Microsoft.UI.Text.FontWeights.Medium,
        FontWeight.SemiBold => Microsoft.UI.Text.FontWeights.SemiBold,
        FontWeight.Bold => Microsoft.UI.Text.FontWeights.Bold,
        FontWeight.ExtraBold => Microsoft.UI.Text.FontWeights.ExtraBold,
        FontWeight.Black => Microsoft.UI.Text.FontWeights.Black,
        _ => throw new ArgumentOutOfRangeException(nameof(太さ), 太さ, null),
    };

    internal static WinUI.Controls.Symbol ToWinUI(this Symbol 記号) => 記号 switch
    {
        Symbol.Add => WinUI.Controls.Symbol.Add,
        Symbol.Remove => WinUI.Controls.Symbol.Remove,
        Symbol.Delete => WinUI.Controls.Symbol.Delete,
        Symbol.Edit => WinUI.Controls.Symbol.Edit,
        Symbol.Save => WinUI.Controls.Symbol.Save,
        Symbol.Accept => WinUI.Controls.Symbol.Accept,
        Symbol.Cancel => WinUI.Controls.Symbol.Cancel,
        Symbol.Play => WinUI.Controls.Symbol.Play,
        Symbol.Pause => WinUI.Controls.Symbol.Pause,
        Symbol.Stop => WinUI.Controls.Symbol.Stop,
        Symbol.Back => WinUI.Controls.Symbol.Back,
        Symbol.Forward => WinUI.Controls.Symbol.Forward,
        Symbol.Home => WinUI.Controls.Symbol.Home,
        Symbol.Setting => WinUI.Controls.Symbol.Setting,
        Symbol.Refresh => WinUI.Controls.Symbol.Refresh,
        _ => throw new ArgumentOutOfRangeException(nameof(記号), 記号, null),
    };
}
