using SengenWinUI3;
using static SengenWinUI3.UI;

namespace Gallery;

// トーストを出す。4つの種類・時間で消える/閉じるまで残る・ボタンと詳細・文の書き換え・別のスレッドから出す・画面の中身を入れ替えても残る、を順に出す。
// 注意: この場面は起動して動かしていない。画面と動きの確かめは、実機で確かめていないことの一覧に書いてある。
internal sealed class ToastScene : Scene
{
    private static readonly Style 画面の見た目 = new() { Padding = new Thickness(16.Px), Spacing = 8.Px };
    private static readonly TimeSpan 短い時間 = TimeSpan.FromSeconds(1);

    private readonly ToastHost トースト置き場;
    private readonly ToastHandle 残るトースト;

    public ToastScene(SceneRun 場面の進め方) : base(場面の進め方)
    {
        トースト置き場 = Run.Window.AttachToastHost(new ToastHostSettings());
        残るトースト = トースト置き場.Show(new ToastMessage(ToastKind.Info, "残るトースト").WithLifetime(new UntilClosedLifetime()));
        Run.RunSteps(
            種類を4つ出す,
            時間で消えるトーストを出す,
            ボタンと詳細を付けて出す,
            文を書き換える,
            別のスレッドから出す,
            画面の中身を入れ替える,
            入れ替えた後に出す);
    }

    protected override Node Build() => (
        StackPanel(画面の見た目).Children(
            TextBlock("トーストの場面"),
            Button("成功を出す").OnClick(成功を出す),
            Button("全部閉じる").OnClick(トースト置き場.CloseAll))
    );

    private void 成功を出す() => _ = トースト置き場.Show(new ToastMessage(ToastKind.Success, "成功"));

    private void 種類を4つ出す()
    {
        成功を出す();
        _ = トースト置き場.Show(new ToastMessage(ToastKind.Info, "情報"));
        _ = トースト置き場.Show(new ToastMessage(ToastKind.Warning, "警告"));
        _ = トースト置き場.Show(new ToastMessage(ToastKind.Error, "エラー"));
        Run.Report("4つの種類を出した");
    }

    private void 時間で消えるトーストを出す()
    {
        _ = トースト置き場.Show(new ToastMessage(ToastKind.Warning, "1秒で消える").WithLifetime(new TimedLifetime(短い時間)));
        Run.Report("1秒で消えるトーストを出した");
    }

    private void ボタンと詳細を付けて出す()
    {
        _ = トースト置き場.Show(
            new ToastMessage(ToastKind.Error, "ボタンと詳細つき")
                .WithDetail("詳細の文")
                .WithButton("元に戻す", () => Run.Report("ボタンが押された"))
                .WithCloseButton(false));
        Run.Report("ボタンと詳細を付けて出した");
    }

    private void 文を書き換える()
    {
        残るトースト.SetText("残るトースト(書き換えた)");
        残るトースト.SetDetail("詳細を足した");
        Run.Report("残るトーストの文と詳細を書き換えた");
    }

    private void 別のスレッドから出す()
    {
        _ = Task.Run(() =>
        {
            ToastHandle 別のスレッドの札 = トースト置き場.Show(new ToastMessage(ToastKind.Info, "別のスレッドから出した"));
            別のスレッドの札.SetText("別のスレッドから書き換えた");
        });
        Run.Report("別のスレッドから出して書き換えた");
    }

    private void 画面の中身を入れ替える()
    {
        Run.Window.SetContent(TextBlock("入れ替えた中身"));
        Run.Report("画面の中身を入れ替えた");
    }

    private void 入れ替えた後に出す()
    {
        残るトースト.Close();
        _ = トースト置き場.Show(new ToastMessage(ToastKind.Success, "入れ替えた後に出した"));
        Run.Report("残るトーストを閉じ、入れ替えた後にも出した");
    }
}
