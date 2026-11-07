using SengenWinUI3;
using static SengenWinUI3.UI;
using WinUI = Microsoft.UI.Xaml;

namespace Gallery;

// 起動の引数で場面を選ばなかったときに見せる、場面の一覧。押した場面をウインドウの中身にする。
internal sealed class SceneMenu(WinUI.Window ウインドウ, SceneRun 場面の進め方) : Component
{
    private static readonly Style 一覧の見た目 = new() { Padding = new Thickness(16.Px), Spacing = 8.Px };

    protected override Node Build() => (
        StackPanel(一覧の見た目).Children(
            SceneCatalog.SceneTypes.Select(場面の型 => Button(場面の型.Name).OnClick(() => ウインドウ.SetContent(SceneCatalog.Create(場面の型, 場面の進め方)))))
    );
}
