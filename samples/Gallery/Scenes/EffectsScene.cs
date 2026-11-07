using SengenWinUI3;
using static SengenWinUI3.UI;

namespace Gallery;

internal sealed class EffectsScene : Scene
{
    private static readonly TimeSpan 動く時間 = TimeSpan.FromMilliseconds(300);
    private static readonly Style 場面の見た目 = new() { Spacing = 12.Px, Padding = new Thickness(32.Px) };
    private static readonly Style 対象の見た目 = new()
    {
        Width = 180.Px, Height = 64.Px, Margin = new Thickness(24.Px),
        Background = new Color(255, 65, 105, 170), Padding = new Thickness(12.Px),
    };
    private static readonly Style 濃い見た目 = new() { Opacity = new Ratio(1) };
    private static readonly Style 薄い見た目 = new() { Opacity = new Ratio(0.35) };

    private readonly 影を付ける部品 対象 = new 影を付ける部品()
        .Shadow(16.Px).OpacityTransition(動く時間)
        .TranslationTransition(動く時間).ScaleTransition(動く時間);
    private bool 動かしたか;
    private bool 薄いか;
    private bool 影があるか = true;

    public EffectsScene(SceneRun 場面の進め方) : base(場面の進め方) { }

    protected override Node Build() => StackPanel(場面の見た目).Children(
        TextBlock("動きと影: Component の根に設定する"),
        対象,
        Button("位置と拡大率を切り替える").OnClick(位置と拡大率を変える),
        Button("不透明度を切り替える").OnClick(不透明度を変える),
        Button("影を切り替える").OnClick(影を変える),
        Button("動く時間を設定し直す").OnClick(動く時間を設定し直す));

    private void 位置と拡大率を変える()
    {
        動かしたか = !動かしたか;
        _ = 対象.Translation(動かしたか ? 48.Px : 0.Px, 動かしたか ? 8.Px : 0.Px)
            .Scale(動かしたか ? 1.15 : 1, 動かしたか ? 1.15 : 1);
        Run.Report(動かしたか ? "位置を横48・縦8へ変え、1.15倍にした" : "位置と拡大率を元へ戻した");
    }

    private void 不透明度を変える()
    {
        薄いか = !薄いか;
        _ = 対象.Style(薄いか ? 薄い見た目 : 濃い見た目);
        Run.Report(薄いか ? "不透明度を0.35にした" : "不透明度を1にした");
    }

    private void 影を変える()
    {
        影があるか = !影があるか;
        if (影があるか)
        {
            _ = 対象.Shadow(16.Px);
        }
        else
        {
            _ = 対象.NoShadow();
        }
        Run.Report(影があるか ? "奥行き16の影を付けた" : "影を外して奥行きを0にした");
    }

    private void 動く時間を設定し直す()
    {
        _ = 対象.OpacityTransition(動く時間).TranslationTransition(動く時間).ScaleTransition(動く時間);
        Run.Report("不透明度・位置・拡大率の動く時間を300ミリ秒に設定し直した");
    }

    private sealed class 影を付ける部品 : Component
    {
        protected override Node Build() => StackPanel(対象の見た目).Children(TextBlock("影と動きの対象"));
    }
}
