using SengenWinUI3;
using static SengenWinUI3.UI;

namespace Gallery;

internal sealed class InputScene : Scene
{
    private static readonly Style 画面の見た目 = new() { Padding = new Thickness(16.Px), Spacing = 8.Px };
    private static readonly Style 入力欄の見た目 = new() { Width = 320.Px };
    private readonly TextBox 文字入力 = TextBox("初期値", placeholderText: "文字を入力", style: 入力欄の見た目);
    private readonly PasswordBox パスワード入力 = PasswordBox("sample", "パスワード", 入力欄の見た目);
    private readonly NumberBox 数値入力 = NumberBox(5, minimum: 0, maximum: 10, style: 入力欄の見た目);
    private readonly Slider スライダー = Slider(4, minimum: 0, maximum: 10, stepFrequency: 2, style: 入力欄の見た目);
    private readonly CheckBox チェック欄 = CheckBox(false, "チェックを入れる");
    private readonly ToggleButton 切り替えボタン = ToggleButton(false, "押し込みを切り替える");
    private readonly ToggleSwitch スイッチ = ToggleSwitch(false, "スイッチ");

    public InputScene(SceneRun 場面の進め方) : base(場面の進め方)
    {
        文字入力.OnTextChanged(文字 => Run.Report($"TextBox: {文字}"));
        パスワード入力.OnPasswordChanged(文字 => Run.Report($"PasswordBox: {文字.Length} 文字"));
        数値入力.OnValueChanged(変わった後の数値 => Run.Report($"NumberBox: {変わった後の数値}"));
        スライダー.OnValueChanged(変わった後の数値 => Run.Report($"Slider: {変わった後の数値}"));
        チェック欄.OnCheckedChanged(変わった後の状態 => Run.Report($"CheckBox: {変わった後の状態}"));
        切り替えボタン.OnCheckedChanged(変わった後の状態 => Run.Report($"ToggleButton: {変わった後の状態}"));
        スイッチ.OnToggled(変わった後の状態 => Run.Report($"ToggleSwitch: {変わった後の状態}"));
    }

    protected override Node Build() => ScrollViewer(StackPanel(画面の見た目).Children(
        TextBlock("入力部品"),
        文字入力,
        TextBox("複数行\n2行目", acceptsReturn: true, textWrapping: TextWrapping.Wrap, style: 入力欄の見た目)
            .OnTextChanged(文字 => Run.Report($"複数行: {文字}")),
        パスワード入力,
        数値入力,
        スライダー,
        チェック欄,
        切り替えボタン,
        スイッチ,
        HyperlinkButton("処理を呼ぶ").OnClick(() => Run.Report("HyperlinkButton: Click")),
        HyperlinkButton("Microsoft Learn を開く", new Uri("https://learn.microsoft.com/")),
        HyperlinkButton("説明を開く").Flyout(Flyout(TextBlock("入力部品の見本"))),
        Button("プログラムから値を設定する").OnClick(値を設定する),
        Button("範囲外の数を設定する").OnClick(範囲外の数を設定する)));

    private void 値を設定する()
    {
        文字入力.SetText("プログラムの設定値");
        パスワード入力.SetPassword("updated");
        数値入力.SetValue(8);
        スライダー.SetValue(8);
        チェック欄.SetIsChecked(true);
        切り替えボタン.SetIsChecked(true);
        スイッチ.SetIsOn(true);
        Run.Report("プログラムから7種を設定した。変更の通知が出ないことを確認する");
    }

    private void 範囲外の数を設定する()
    {
        数値入力.SetValue(-10).SetValue(20);
        スライダー.SetValue(-10).SetValue(20);
        Run.Report("範囲外の数を連続で設定した。表示が10になり、変更の通知が出ないことを確認する");
    }
}
