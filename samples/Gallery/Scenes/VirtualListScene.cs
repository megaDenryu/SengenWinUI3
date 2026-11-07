using System.Globalization;
using SengenWinUI3;
using static SengenWinUI3.UI;

namespace Gallery;

// VirtualList に 10000 件を出し、外側の ScrollViewer の中で、見えている行だけが作られ、スクロールで行が使い回されることを確かめる。
// 行は Show で項目の文字を設定する。項目を変えるときは ReplaceItem で置き換える。
internal sealed class VirtualListScene : Scene
{
    private const int 項目の数 = 10000;
    private static readonly Style 行の見た目 = new() { Orientation = Orientation.Horizontal, Spacing = 12.Px, Padding = new Thickness(4.Px, 8.Px) };

    private readonly 行の集計 集計 = new();
    private readonly List<番号の項目> 項目List = [.. Enumerable.Range(0, 項目の数).Select(番号の項目.From番号)];
    private readonly VirtualList<番号の項目, 番号の行> 一覧;
    private readonly ScrollViewer 外側のスクロール;

    public VirtualListScene(SceneRun 場面の進め方) : base(場面の進め方)
    {
        一覧 = VirtualList<番号の項目, 番号の行>(() => new 番号の行(集計)).SetItems(項目List);
        外側のスクロール = ScrollViewer(一覧);
        Run.RunSteps(
            () => 集計を書く("始めの配置の後"),
            外側のスクロール.ScrollToEnd,
            () => 集計を書く("末尾までスクロールした後"),
            見出しを変える,
            一覧の項目を変える,
            () => 集計を書く("項目を足し・差し込み・置き換え・外した後"));
        Run.ReportAfterClose(() => $"閉じた後: 作った行 {集計.作った数}・Show {集計.見せた回数}・Release {集計.離れた回数}");
    }

    protected override Node Build() => 外側のスクロール;

    private void 集計を書く(string いつ) =>
        Run.Report($"{いつ}: 作った行 {集計.作った数}・Show {集計.見せた回数}・使い回し {集計.使い回した回数}・Release {集計.離れた回数}");

    private void 見出しを変える()
    {
        項目List[^1] = 項目List[^1] with { 見出し = "末尾の見出しを変えた" };
        項目List[0] = 項目List[0] with { 見出し = "先頭の見出しを変えた" };
        _ = 一覧.ReplaceItem(項目の数 - 1, 項目List[^1]).ReplaceItem(0, 項目List[0]);
        Run.Report("末尾と先頭の項目を置き換えて見出しを変えた");
    }

    private void 一覧の項目を変える()
    {
        _ = 一覧.AddItem(番号の項目.From番号(項目の数))
            .InsertItem(0, 番号の項目.From番号(項目の数 + 1))
            .ReplaceItem(項目の数, 番号の項目.From番号(項目の数 + 2))
            .RemoveItemAt(0);
    }

    private sealed class 行の集計
    {
        internal int 作った数;
        internal int 見せた回数;
        internal int 使い回した回数;
        internal int 離れた回数;
    }

    private sealed record 番号の項目(string 番号の文字, string 見出し)
    {
        internal static 番号の項目 From番号(int 番号) => new(番号.ToString(CultureInfo.InvariantCulture), $"項目 {番号}");
    }

    // 使い回す行。作るときに1回だけ組み、Show で項目の値を設定する。
    private sealed class 番号の行 : Component, IRecyclableRow<番号の項目>
    {
        private readonly 行の集計 集計;
        private readonly TextBlock 番号の文字部品 = TextBlock("");
        private readonly TextBlock 見出しの文字部品 = TextBlock("");
        private bool 見せたことがあるか;

        internal 番号の行(行の集計 集計)
        {
            this.集計 = 集計;
            集計.作った数++;
        }

        public void Show(番号の項目 item)
        {
            集計.見せた回数++;
            集計.使い回した回数 += 見せたことがあるか ? 1 : 0;
            見せたことがあるか = true;
            _ = 番号の文字部品.SetText(item.番号の文字);
            _ = 見出しの文字部品.SetText(item.見出し);
        }

        // この行は項目に結び付いた資源を持たないので、ここでは数えるだけである。
        public void Release() => 集計.離れた回数++;

        protected override Node Build() => StackPanel(行の見た目).Children(番号の文字部品, 見出しの文字部品);
    }
}
