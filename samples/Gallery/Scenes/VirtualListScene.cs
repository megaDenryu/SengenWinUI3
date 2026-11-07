using System.Globalization;
using SengenWinUI3;
using SengenWinUI3.State;
using static SengenWinUI3.UI;

namespace Gallery;

// VirtualList に 10000 件を出し、外側の ScrollViewer の中で、見えている行だけが作られ、スクロールで行が使い回されることを確かめる。
// 行は見出しのセルの見張りを作るときに1回だけ作り、Show で項目の見出しのセルを Watch する。見張りは一覧が Release の前に解く。見えていない項目のセルを変えても、行へ反映されないことも確かめる。
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
            見出しのセルを変える,
            一覧の項目を変える,
            () => 集計を書く("項目を足し・差し込み・置き換え・外した後"));
        Run.ReportAfterClose(() => $"閉じた後: 作った行 {集計.作った数}・Show {集計.見せた回数}・Release {集計.離れた回数}");
    }

    protected override Node Build() => 外側のスクロール;

    private void 集計を書く(string いつ) =>
        Run.Report($"{いつ}: 作った行 {集計.作った数}・Show {集計.見せた回数}・使い回し {集計.使い回した回数}・Release {集計.離れた回数}");

    // 末尾の項目は見えていて、先頭の項目は見えていない。行へ反映されるのは末尾の1回だけのはずである。
    private void 見出しのセルを変える()
    {
        int 前の回数 = 集計.見出しを反映した回数;
        項目List[^1].見出し.Value = "末尾の見出しを変えた";
        項目List[0].見出し.Value = "先頭の見出しを変えた";
        Run.Report($"見出しのセルを末尾と先頭で変えた: 行へ反映した回数の増え {集計.見出しを反映した回数 - 前の回数}");
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
        internal int 見出しを反映した回数;
    }

    private sealed record 番号の項目(string 番号の文字, Cell<string> 見出し)
    {
        internal static 番号の項目 From番号(int 番号) => new(番号.ToString(CultureInfo.InvariantCulture), new Cell<string>($"項目 {番号}"));
    }

    // 使い回す行。作るときに1回だけ組み、見出しのセルの見張りも作る。Show で項目の見出しのセルを Watch する。
    private sealed class 番号の行 : Component, IRecyclableRow<番号の項目>
    {
        private readonly 行の集計 集計;
        private readonly TextBlock 番号の文字部品 = TextBlock("");
        private readonly TextBlock 見出しの文字部品 = TextBlock("");
        private readonly CellWatcher<string> 見出しの見張り;
        private bool 見せたことがあるか;

        internal 番号の行(行の集計 集計)
        {
            this.集計 = 集計;
            集計.作った数++;
            見出しの見張り = WatchCell<string>(見出しを反映する);
        }

        public void Show(番号の項目 item)
        {
            集計.見せた回数++;
            集計.使い回した回数 += 見せたことがあるか ? 1 : 0;
            見せたことがあるか = true;
            _ = 番号の文字部品.SetText(item.番号の文字);
            見出しの見張り.Watch(item.見出し);
        }

        // 見出しの見張りは一覧が解いてあるので、ここでは数えるだけである。
        public void Release() => 集計.離れた回数++;

        protected override Node Build() => StackPanel(行の見た目).Children(番号の文字部品, 見出しの文字部品);

        private void 見出しを反映する(string 見出し)
        {
            集計.見出しを反映した回数++;
            _ = 見出しの文字部品.SetText(見出し);
        }
    }
}
