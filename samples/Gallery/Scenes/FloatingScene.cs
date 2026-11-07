using SengenWinUI3;
using static SengenWinUI3.UI;

namespace Gallery;

internal sealed class FloatingScene : Scene
{
    private static readonly Style 縦に並べる見た目 = new() { Spacing = 12.Px, Padding = new Thickness(16.Px) };
    private readonly Button ポップアップの基準 = Button("ポップアップを開く");
    private readonly Button ダイアログの基準 = Button("確認の画面を開く");
    private readonly Popup ポップアップ;
    private readonly ContentDialog 確認の画面;

    public FloatingScene(SceneRun 場面の進め方) : base(場面の進め方)
    {
        ポップアップ = Popup(StackPanel(縦に並べる見た目).Children(
            TextBlock("基準から横 0、縦 40 の位置"),
            Button("閉じる").OnClick(ポップアップを閉じる)), 0.Px, 40.Px, true);
        確認の画面 = ContentDialog("操作を確認する", TextBlock("3種類の結果を報告する"),
            ボタンの文字("主の操作"), ボタンの文字("2つ目の操作"), ボタンの文字("閉じる"));
        this.Own(ポップアップ).Own(確認の画面);
        ポップアップの基準.OnClick(ポップアップを開く).ToolTip("閉じた後も同じ基準から開ける");
        ダイアログの基準.OnClick(確認の画面を開く);
    }

    protected override Node Build() => StackPanel(縦に並べる見た目).Children(
        ポップアップの基準,
        Button("ポップアップを閉じる").OnClick(ポップアップを閉じる),
        ダイアログの基準,
        TextBlock("ここを右クリックするとメニューが開く").ContextFlyout(MenuFlyout(
            new MenuFlyoutAction("最初の項目", () => Run.Report("最初の項目を押した")),
            new MenuFlyoutSeparator(),
            new MenuFlyoutAction("次の項目", () => Run.Report("次の項目を押した")))),
        Button("押すとメニューが開く").Flyout(MenuFlyout(
            new MenuFlyoutAction("押して開いたメニューの項目", () => Run.Report("押して開いたメニューの項目を押した")))),
        TextBlock("この説明にポインターを重ねる").ToolTip("文字だけの説明"));

    private void ポップアップを開く()
    {
        ポップアップ.Open(ポップアップの基準);
        Run.Report("ポップアップを開いた");
    }

    private void ポップアップを閉じる()
    {
        ポップアップ.Close();
        Run.Report("ポップアップを閉じた");
    }

    private static DialogButtonText ボタンの文字(string 文字) => DialogButtonText.TryFromText(文字) switch
    {
        DialogButtonText 表示 => 表示,
        DialogButtonTextError => throw new InvalidOperationException("見本のボタンの文字が空である"),
    };

    private async void 確認の画面を開く()
    {
        try
        {
            ContentDialogResult 結果 = await 確認の画面.ShowAsync(ダイアログの基準);
            if (IsDestroyed) return;
            Run.Report(結果 switch
            {
                ContentDialogPrimaryResult => "主のボタンで閉じた",
                ContentDialogSecondaryResult => "2つ目のボタンで閉じた",
                ContentDialogCloseResult => "確認の画面を閉じた",
            });
        }
        catch (Exception 例外)
        {
            Run.Report($"確認の画面で例外: {例外.Message}");
        }
    }
}
