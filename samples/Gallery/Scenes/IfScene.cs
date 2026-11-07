using SengenWinUI3;
using SengenWinUI3.State;
using static SengenWinUI3.UI;

namespace Gallery;

// If をボタンで切り替える。部品を受ける版は2つの側を初めから持ち、作る関数を受ける版は初めて真になったときに1回だけ作ることを確かめる。
internal sealed class IfScene : Scene
{
    private static readonly Style 画面の見た目 = new() { Padding = new Thickness(16.Px), Spacing = 8.Px };

    private readonly Cell<bool> 真か = new(false);
    private int 重い部品を作った回数;

    public IfScene(SceneRun 場面の進め方) : base(場面の進め方) =>
        Run.RunSteps(
            () => 作った回数を書く("始め(偽)"),
            切り替える,
            () => 作った回数を書く("1回目の切り替えの後(真)"),
            切り替える,
            () => 作った回数を書く("2回目の切り替えの後(偽)"),
            切り替える,
            () => 作った回数を書く("3回目の切り替えの後(真)"));

    protected override Node Build() => (
        StackPanel(画面の見た目).Children(
            Button("切り替える").OnClick(切り替える),
            If(真か, TextBlock("真の側").OnSizeChanged(_ => Run.Report("真の側が配置された")), TextBlock("偽の側").OnSizeChanged(_ => Run.Report("偽の側が配置された"))),
            If(真か, 重い部品を作る),
            If(真か, TextBlock("偽の側を省いた If の真の側")))
    );

    private void 切り替える() => 真か.Value = !真か.Value;

    private TextBlock 重い部品を作る()
    {
        重い部品を作った回数++;
        return TextBlock("初めて真になったときに作った部品");
    }

    private void 作った回数を書く(string いつ) => Run.Report($"{いつ}: 作る関数を受けた If が部品を作った回数 {重い部品を作った回数}");
}
