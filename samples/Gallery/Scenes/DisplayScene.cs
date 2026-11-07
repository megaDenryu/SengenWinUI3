using System.Globalization;
using SengenWinUI3;
using static SengenWinUI3.UI;

namespace Gallery;

internal sealed class DisplayScene : Scene
{
    private static readonly Color 初期の色 = new(255, 40, 120, 210);
    private static readonly Color 設定する色 = new(255, 200, 80, 40);
    private static readonly Style 縦の見た目 = new() { Spacing = 12.Px, Padding = new Thickness(16.Px) };
    private static readonly Style 横の見た目 = new() { Orientation = Orientation.Horizontal, Spacing = 16.Px };
    private static readonly Style 字形の見た目 = new() { FontFamily = "Segoe Fluent Icons", FontSize = 24.Px };
    private static readonly Style 記号の見た目 = new() { Width = 32.Px, Height = 32.Px, Foreground = 初期の色 };
    private static readonly Style 棒の見た目 = new() { Width = 260.Px };
    private static readonly Style リングの見た目 = new() { Width = 40.Px, Height = 40.Px };
    private static readonly Style 図形の見た目 = new() { Width = 80.Px, Height = 40.Px, Fill = 初期の色, Stroke = 設定する色, StrokeThickness = 2.Px };
    private static readonly Style 線の見た目 = new() { Width = 100.Px, Height = 40.Px, Stroke = 初期の色, StrokeThickness = 3.Px };
    private readonly FontIcon 字形 = FontIcon("\uE710", 字形の見た目);
    private readonly SymbolIcon 記号 = SymbolIcon(Symbol.Add, 記号の見た目);
    private readonly ProgressBar 棒 = ProgressBar(new DeterminateProgress(new Ratio(0.25)), 棒の見た目);
    private readonly ProgressRing リング = ProgressRing(true, new IndeterminateProgress(), リングの見た目);
    private readonly Line 線 = Line(new Point(4.Px, 4.Px), new Point(96.Px, 36.Px), 線の見た目);
    private readonly CalendarDatePicker 日付 = CalendarDatePicker(new NoDate());
    private readonly ColorPicker 色 = ColorPicker(初期の色);

    public DisplayScene(SceneRun 場面の進め方) : base(場面の進め方)
    {
        日付.OnDateChanged(日付を報告する);
        色.OnColorChanged(色を報告する);
    }

    protected override Node Build() => ScrollViewer(StackPanel(縦の見た目).Children(
        TextBlock("アイコン・進み・図形・日付・色"),
        StackPanel(横の見た目).Children(字形, 記号),
        棒,
        リング,
        StackPanel(横の見た目).Children(Rectangle(図形の見た目), Ellipse(図形の見た目), 線),
        Button("字形・記号・線の座標を替える").OnClick(表示を替える),
        Button("進みを 0 と 1 にする").OnClick(進みの端を見せる),
        Button("進みを不確定にする").OnClick(進みを不確定にする),
        Button("リングを止める").OnClick(リングを止める),
        日付,
        Button("今日を設定する").OnClick(今日を設定する),
        Button("日付を未選択にする").OnClick(日付を未選択にする),
        色,
        Button("色を設定する").OnClick(色を設定する),
        TextBlock("日付と色を操作すると利用者の変更を報告する。設定ボタンでは変更通知を報告しない。")));

    private void 表示を替える()
    {
        字形.SetGlyph("\uE711");
        記号.SetSymbol(Symbol.Accept);
        線.SetStart(new Point(4.Px, 36.Px)).SetEnd(new Point(96.Px, 4.Px));
        Run.Report("字形・記号・線の座標を設定した");
    }

    private void 進みの端を見せる()
    {
        棒.SetProgress(new DeterminateProgress(new Ratio(0)));
        リング.SetIsActive(true).SetProgress(new DeterminateProgress(new Ratio(1)));
        Run.Report("棒を 0、リングを 1 に設定した");
    }

    private void 進みを不確定にする()
    {
        棒.SetProgress(new IndeterminateProgress());
        リング.SetProgress(new IndeterminateProgress()).SetIsActive(true);
        Run.Report("棒とリングを不確定に設定した");
    }

    private void リングを止める()
    {
        リング.SetIsActive(false);
        Run.Report("リングの IsActive を false に設定した");
    }

    private void 今日を設定する()
    {
        日付.SetDate(new SelectedDate(DateOnly.FromDateTime(DateTime.Today)));
        Run.Report("今日を設定した。OnDateChanged の報告は増えない");
    }

    private void 日付を未選択にする()
    {
        日付.SetDate(new NoDate());
        Run.Report("日付を未選択にした。OnDateChanged の報告は増えない");
    }

    private void 色を設定する()
    {
        色.SetColor(設定する色);
        Run.Report("色を設定した。OnColorChanged の報告は増えない");
    }

    private void 日付を報告する(DateSelection 選択) => Run.Report(選択 switch
    {
        NoDate _ => "利用者の日付変更: 未選択",
        SelectedDate 日 => $"利用者の日付変更: {日.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}",
    });

    private void 色を報告する(Color 選択) => Run.Report($"利用者の色変更: {選択}");
}
