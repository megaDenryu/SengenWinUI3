using SengenWinUI3;
using static SengenWinUI3.UI;

namespace Gallery;

internal sealed class LayoutScene(SceneRun 場面の進め方) : Scene(場面の進め方)
{
    private static readonly Style 画面の見た目 = new() { Spacing = 12.Px, Padding = new Thickness(16.Px) };
    private static readonly Style 格子の見た目 = new() { Height = 240.Px, RowSpacing = 8.Px, ColumnSpacing = 8.Px };
    private static readonly Style 枠の見た目 = new()
    {
        Padding = new Thickness(8.Px),
        BorderThickness = new Thickness(1.Px),
        BorderBrush = new Color(255, 100, 140, 180),
        CornerRadius = 4.Px,
    };
    private static readonly Style 自由配置の見た目 = new()
    {
        Height = 120.Px,
        Background = new Color(255, 35, 45, 55),
    };

    private readonly Border 入れ替える枠 = Border(TextBlock("最初の中身"), 枠の見た目);

    protected override Node Build() => StackPanel(画面の見た目).Children(
        TextBlock("Grid: 自動・固定・比率と行列の指定"),
        Grid(
            rowDefinitions: [new GridAuto(), 64.Px, new GridStar(1)],
            columnDefinitions: [120.Px, new GridStar(2), new GridStar(1)],
            style: 格子の見た目).Children(
                Border(TextBlock("3列をまたぐ見出し"), 枠の見た目).GridColumnSpan(3),
                Border(TextBlock("2行をまたぐ枠"), 枠の見た目).GridRow(1).GridRowSpan(2),
                入れ替える枠.GridRow(1).GridColumn(1).GridColumnSpan(2),
                TextBlock("比率 2").GridRow(2).GridColumn(1),
                TextBlock("比率 1").GridRow(2).GridColumn(2)),
        Button("Border の中身を入れ替える").OnClick(中身を入れ替える),
        TextBlock("Canvas: 左端と上端からの位置"),
        Canvas(自由配置の見た目).Children(
            Border(TextBlock("左 16・上 12"), 枠の見た目).CanvasLeft(16.Px).CanvasTop(12.Px),
            Button("左 200・上 64").CanvasLeft(200.Px).CanvasTop(64.Px)
                .OnClick(() => Run.Report("Canvas のボタンを押した"))));

    private void 中身を入れ替える()
    {
        入れ替える枠.SetContent(TextBlock("入れ替えた中身"));
        Run.Report("Border の前の中身を破棄し、新しい中身を置いた");
    }
}
