using SengenWinUI3;
using static SengenWinUI3.UI;
using WinUI = Microsoft.UI.Xaml;

namespace Gallery;

// 確認の画面(ContentDialog)とポップアップ(Popup)が開いているときに、トーストが上に出るかを見る場面。
// SengenWinUI3 は確認の画面とポップアップをまだ持たないので、この場面だけ WinUI の部品を直接作る。
// 注意: この場面は起動して動かしていない。上に出るかどうかは、実機で確かめていないことの一覧に書いてある。
internal sealed class ToastOverPopupScene : Scene
{
    private readonly ToastHost トースト置き場;
    private readonly WinUI.Controls.ContentDialog 確認の画面 = new() { Title = "確認の画面", Content = "トーストはこの上に出るはずである", CloseButtonText = "閉じる" };
    private readonly WinUI.Controls.Primitives.Popup 重ねたポップアップ = new();

    public ToastOverPopupScene(SceneRun 場面の進め方) : base(場面の進め方)
    {
        トースト置き場 = Run.Window.AttachToastHost(new ToastHostSettings());
        Run.RunSteps(ポップアップと確認の画面を開く, トーストを出す, 開いているポップアップを書く, 閉じる);
    }

    protected override Node Build() => TextBlock("ポップアップと確認の画面の上にトーストを出す場面");

    private void ポップアップと確認の画面を開く()
    {
        WinUI.XamlRoot 画面の根 = Run.Window.Content.XamlRoot;
        重ねたポップアップ.XamlRoot = 画面の根;
        重ねたポップアップ.Child = new WinUI.Controls.Border { Width = 400, Height = 300, Background = new WinUI.Media.SolidColorBrush(Microsoft.UI.Colors.SteelBlue) };
        重ねたポップアップ.IsOpen = true;
        確認の画面.XamlRoot = 画面の根;
        _ = 確認の画面.ShowAsync(); // 注意: 結果は待たない。閉じる手順で Hide する
        Run.Report("ポップアップと確認の画面を開いた");
    }

    private void トーストを出す()
    {
        _ = トースト置き場.Show(new ToastMessage(ToastKind.Warning, "確認の画面の上に出るトースト").WithLifetime(new UntilClosedLifetime()));
        Run.Report("トーストを出した");
    }

    private void 開いているポップアップを書く()
    {
        var 開いているポップアップList = WinUI.Media.VisualTreeHelper.GetOpenPopupsForXamlRoot(Run.Window.Content.XamlRoot);
        Run.Report($"開いているポップアップ {開いているポップアップList.Count} 個: {string.Join("・", 開いているポップアップList.Select(static 開いているもの => 開いているもの.Child?.GetType().Name ?? "中身なし"))}");
    }

    private void 閉じる()
    {
        確認の画面.Hide();
        重ねたポップアップ.IsOpen = false;
        トースト置き場.CloseAll();
    }
}
