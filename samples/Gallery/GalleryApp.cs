using SengenWinUI3;
using WinUI = Microsoft.UI.Xaml;

namespace Gallery;

// 見本のアプリの Application。XAML のファイルを持たないので、標準の部品の見た目(XamlControlsResources)をコードで読み込み、その型の情報を渡す。
internal sealed partial class GalleryApp : WinUI.Application, WinUI.Markup.IXamlMetadataProvider
{
    private readonly WinUI.XamlTypeInfo.XamlControlsXamlMetaDataProvider 標準の部品の型の情報 = new();
    private readonly GalleryOptions 起動の設定;

    internal GalleryApp(GalleryOptions 起動の設定)
    {
        this.起動の設定 = 起動の設定;
        UnhandledException += (_, 出来事) => 起動の設定.結果を書く($"捕まえていない例外: {出来事.Exception}");
    }

    public WinUI.Markup.IXamlType GetXamlType(Type type) => 標準の部品の型の情報.GetXamlType(type);

    public WinUI.Markup.IXamlType GetXamlType(string fullName) => 標準の部品の型の情報.GetXamlType(fullName);

    public WinUI.Markup.XmlnsDefinition[] GetXmlnsDefinitions() => 標準の部品の型の情報.GetXmlnsDefinitions();

    protected override void OnLaunched(WinUI.LaunchActivatedEventArgs args)
    {
        Resources.MergedDictionaries.Add(new WinUI.Controls.XamlControlsResources());
        WinUI.Window ウインドウ = new() { Title = "SengenWinUI3 Gallery" };
        SceneRun 場面の進め方 = new(ウインドウ, 起動の設定.結果を書く, 起動の設定.自動で進めるか);
        Node 最初の画面 = 起動の設定.始める画面 switch
        {
            SceneMenuScreen => new SceneMenu(ウインドウ, 場面の進め方),
            NamedSceneScreen 選んだ場面 => SceneCatalog.Create(選んだ場面.場面の型, 場面の進め方),
        };
        ウインドウ.SetContent(最初の画面);
        ウインドウ.Activate();
        起動の設定.結果を書く("ウインドウを開いた");
    }
}
