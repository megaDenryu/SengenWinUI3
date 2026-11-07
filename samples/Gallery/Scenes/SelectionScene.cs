using SengenWinUI3;
using static SengenWinUI3.UI;

namespace Gallery;

internal sealed class SelectionScene : Scene
{
    private static readonly Style 画面の見た目 = new() { Padding = new Thickness(16.Px), Spacing = 12.Px };
    private static readonly Style 部品の見た目 = new() { MinWidth = 240.Px };
    private static readonly SelectionIndex 先頭の位置 = 位置を作る(0);
    private static readonly SelectionIndex 次の位置 = 位置を作る(1);

    private readonly TextBlock 外せる項目 = TextBlock("取り外せる項目");
    private readonly ListView 一覧 = ListView(部品の見た目).Children(TextBlock("項目 A"), TextBlock("項目 B"));
    private readonly ComboBox 候補欄 = ComboBox([TextBlock("項目 A"), TextBlock("項目 B")], 先頭の位置, 部品の見た目);
    private readonly RadioButtons 選択肢 = RadioButtons([TextBlock("項目 A"), TextBlock("項目 B")], new NoSelection(), 部品の見た目);

    public SelectionScene(SceneRun 場面の進め方) : base(場面の進め方)
    {
        _ = 一覧.AddChild(外せる項目).OnSelectionChanged(位置 => 選択を書く("ListView", 位置));
        _ = 候補欄.OnSelectionChanged(位置 => 選択を書く("ComboBox", 位置));
        _ = 選択肢.OnSelectionChanged(位置 => 選択を書く("RadioButtons", 位置));
    }

    protected override Node Build() => StackPanel(画面の見た目).Children(
        TextBlock("選択の通知は場面の結果に出る。設定ボタンでは選択の通知が出ないことを確認する。"),
        TextBlock("ListView"), 一覧,
        TextBlock("ComboBox"), 候補欄,
        TextBlock("RadioButtons"), 選択肢,
        Button("2番目を設定する").OnClick(次の位置を設定する),
        Button("未選択を設定する").OnClick(未選択を設定する),
        Button("先頭へ項目を差し込む").OnClick(先頭へ項目を差し込む),
        Button("一覧の項目を外して末尾へ戻す").OnClick(項目を外して戻す),
        Button("項目を全部破棄する").OnClick(項目を全部破棄する));

    private void 次の位置を設定する()
    {
        _ = 一覧.SetSelectedIndex(次の位置);
        _ = 候補欄.SetSelectedIndex(次の位置);
        _ = 選択肢.SetSelectedIndex(次の位置);
        Run.Report("3部品へ2番目を設定した");
    }

    private void 未選択を設定する()
    {
        _ = 一覧.SetSelectedIndex(new NoSelection());
        _ = 候補欄.SetSelectedIndex(new NoSelection());
        _ = 選択肢.SetSelectedIndex(new NoSelection());
        Run.Report("3部品へ未選択を設定した");
    }

    private void 先頭へ項目を差し込む()
    {
        _ = 一覧.InsertChild(0, TextBlock("差し込んだ項目"));
        _ = 候補欄.InsertChild(0, TextBlock("差し込んだ項目"));
        _ = 選択肢.InsertChild(0, TextBlock("差し込んだ項目"));
        Run.Report("3部品の先頭へ項目を差し込んだ");
    }

    private void 項目を外して戻す()
    {
        if (外せる項目.IsDestroyed)
        {
            Run.Report("取り外せる項目は全破棄で破棄済みである");
            return;
        }
        外せる項目.Detach();
        _ = 一覧.AddChild(外せる項目);
        Run.Report("一覧の項目を外して末尾へ戻した");
    }

    private void 項目を全部破棄する()
    {
        _ = 一覧.DestroyChildren();
        _ = 候補欄.DestroyChildren();
        _ = 選択肢.DestroyChildren();
        Run.Report("3部品の項目を全部破棄した。位置の設定は項目を2つ以上差し込んでから行う。");
    }

    private void 選択を書く(string 部品名, SelectionIndex 位置) => Run.Report(位置 switch
    {
        NoSelection _ => $"{部品名}: 未選択",
        SelectedIndex 選択済み => $"{部品名}: 0 始まりの位置 {選択済み.Index}",
    });

    private static SelectionIndex 位置を作る(int 位置) => SelectedIndex.TryFromIndex(位置) switch
    {
        SelectedIndex 選択済み => 選択済み,
        SelectedIndexError エラー => throw new ArgumentOutOfRangeException(nameof(位置), エラー.Index, "見本の選択の位置が負である"),
    };
}
