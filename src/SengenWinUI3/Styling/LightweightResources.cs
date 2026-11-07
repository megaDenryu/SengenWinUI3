using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// WinUI の lightweight styling(部品ごとの Resources に、既定のテンプレートが引く名前でブラシを置いて色を替える仕組み)の書き込み。
// 注意: テンプレートはリソースを読み込むときに1回引くので、読み込まれた後に書いても効かない。書き込みの前に ThrowIfLoaded で拒む。
internal static class LightweightResources
{
    internal static void ThrowIfLoaded(WinUI.FrameworkElement 本物, Node 部品)
    {
        if (本物.IsLoaded)
        {
            throw new InvalidOperationException($"{部品.GetType().Name} は読み込まれた後に状態ごとの色を設定された。木へ入れる前に設定する");
        }
    }

    // 塗りが null の項目は、既定の色のままにする。
    internal static void Write(WinUI.FrameworkElement 本物, string 資源名, Brush? 塗り)
    {
        if (塗り is { } 指定の塗り)
        {
            本物.Resources[資源名] = 指定の塗り.ToWinUI();
        }
    }
}
