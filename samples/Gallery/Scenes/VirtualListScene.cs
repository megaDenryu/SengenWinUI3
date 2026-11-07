using System.Globalization;
using SengenWinUI3;
using SengenWinUI3.State;
using static SengenWinUI3.UI;

namespace Gallery;

// VirtualList に 10000 件を出し、外側の ScrollViewer の中で、見えている行だけが作られ、スクロールで行が使い回されることを確かめる。
// 行は項目の見出しのセルを Show で Observe し、Release で解く。見えていない項目のセルを変えても、行へ反映されないことも確かめる。
internal sealed class VirtualListScene : Scene
{
    private const int 項目の数 = 10000;
    private static readonly Style 行の見た目 = new() { Orientation = Orientation.Horizontal, Spacing = 12.Px, Padding = new Thickness(4.Px, 8.Px) };

    private readonly 行の数え 数え = new();
    private readonly List<番号の項目> 項目List = [.. Enumerable.Range(0, 項目の数).Select(番号の項目.From番号)];
    private readonly VirtualList<番号の項目, 番号の行> 一覧;
    private readonly ScrollViewer 外側のスクロール;

    public VirtualListScene(SceneRun 場面の進め方) : base(場面の進め方)
    {
        一覧 = VirtualList<番号の項目, 番号の行>(() => new 番号の行(数え)).SetItems(項目List);
        外側のスクロール = ScrollViewer(一覧);
        Run.RunSteps(
            () => 数えを書く("始めの配置の後"),
            外側のスクロール.ScrollToEnd,
            () => 数えを書く("末尾までスクロールした後"),
            見出しのセルを変える,
            一覧の項目を変える,
            () => 数えを書く("項目を足し・差し込み・置き換え・外した後"));
        Run.ReportAfterClose(() => $"閉じた後: 作った行 {数え.作った数}・Show {数え.Showの回数}・Release {数え.Releaseの回数}");
    }

    protected override Node Build() => 外側のスクロール;

    private void 数えを書く(string いつ) =>
        Run.Report($"{いつ}: 作った行 {数え.作った数}・Show {数え.Showの回数}・使い回し {数え.使い回した回数}・Release {数え.Releaseの回数}");

    // 末尾の項目は見えていて、先頭の項目は見えていない。行へ反映されるのは末尾の1回だけのはずである。
    private void 見出しのセルを変える()
    {
        int 前の回数 = 数え.見出しを反映した回数;
        項目List[^1].見出し.Value = "末尾の見出しを変えた";
        項目List[0].見出し.Value = "先頭の見出しを変えた";
        Run.Report($"見出しのセルを末尾と先頭で変えた: 行へ反映した回数の増え {数え.見出しを反映した回数 - 前の回数}");
    }

    private void 一覧の項目を変える()
    {
        _ = 一覧.AddItem(番号の項目.From番号(項目の数))
            .InsertItem(0, 番号の項目.From番号(項目の数 + 1))
            .ReplaceItem(項目の数, 番号の項目.From番号(項目の数 + 2))
            .RemoveItemAt(0);
    }

    private sealed class 行の数え
    {
        internal int 作った数;
        internal int Showの回数;
        internal int 使い回した回数;
        internal int Releaseの回数;
        internal int 見出しを反映した回数;
    }

    private sealed record 番号の項目(string 番号の文字, Cell<string> 見出し)
    {
        internal static 番号の項目 From番号(int 番号) => new(番号.ToString(CultureInfo.InvariantCulture), new Cell<string>($"項目 {番号}"));
    }

    // 使い回す行。作るときに1回だけ組み、Show で項目の見出しを結び、Release で解く。
    private sealed class 番号の行 : Component, IRecyclableRow<番号の項目>
    {
        private readonly 行の数え 数え;
        private readonly TextBlock 番号の文字 = TextBlock("");
        private readonly TextBlock 見出しの文字 = TextBlock("");
        private readonly Action<string> 見出しを反映する;
        private IDisposable 見出しの購読 = 解くものの無い札.Instance;
        private bool 見せたことがあるか;

        internal 番号の行(行の数え 数え)
        {
            this.数え = 数え;
            数え.作った数++;
            見出しを反映する = 見出し =>
            {
                数え.見出しを反映した回数++;
                _ = 見出しの文字.SetText(見出し);
            };
        }

        public void Show(番号の項目 item)
        {
            数え.Showの回数++;
            数え.使い回した回数 += 見せたことがあるか ? 1 : 0;
            見せたことがあるか = true;
            _ = 番号の文字.SetText(item.番号の文字);
            見出しの購読 = Observe(item.見出し, 見出しを反映する);
        }

        public void Release()
        {
            数え.Releaseの回数++;
            見出しの購読.Dispose();
            見出しの購読 = 解くものの無い札.Instance;
        }

        protected override Node Build() => StackPanel(行の見た目).Children(番号の文字, 見出しの文字);
    }

    private sealed class 解くものの無い札 : IDisposable
    {
        internal static readonly 解くものの無い札 Instance = new();

        public void Dispose() { }
    }
}
