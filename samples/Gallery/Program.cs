using Microsoft.UI.Dispatching;
using WinUI = Microsoft.UI.Xaml;

namespace Gallery;

// 見本のアプリの入口。XAML のファイルを持たないので、WinUI が生成する Main の代わりにここで Application を始める。
internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        GalleryOptions 起動の設定 = GalleryOptions.FromArgs(args);
        WinRT.ComWrappersSupport.InitializeComWrappers();
        WinUI.Application.Start(始めの知らせ =>
        {
            SynchronizationContext.SetSynchronizationContext(new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread()));
            _ = new GalleryApp(起動の設定); // 注意: Application は作るだけで WinUI に登録され、OnLaunched が呼ばれる
        });
    }
}
