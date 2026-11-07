using System.Diagnostics;
using System.Text;

namespace Gallery;

// 起動の引数から読んだ設定。--scene 場面の型の名前(省くと場面の一覧)・--report 結果を書くファイル(省くとデバッグの出力)・--auto(場面の手順を自動で進め、終えたら閉じる)。
internal sealed record GalleryOptions(StartScreen 始める画面, Action<string> 結果を書く, bool 自動で進めるか)
{
    private static readonly UTF8Encoding 印の無いUTF8 = new(encoderShouldEmitUTF8Identifier: false);

    internal static GalleryOptions FromArgs(IReadOnlyList<string> 引数List)
    {
        StartScreen 始める画面 = new SceneMenuScreen();
        Action<string> 結果を書く = static 文 => Debug.WriteLine(文);
        bool 自動で進めるか = false;
        for (int 位置 = 0; 位置 < 引数List.Count; 位置++)
        {
            switch (引数List[位置])
            {
                case "--scene":
                    始める画面 = new NamedSceneScreen(SceneCatalog.FindType(次の値(引数List, ref 位置)));
                    break;
                case "--report":
                    string 書く先 = Path.GetFullPath(次の値(引数List, ref 位置));
                    File.WriteAllText(書く先, "", 印の無いUTF8);
                    結果を書く = 文 => File.AppendAllText(書く先, 文 + "\n", 印の無いUTF8);
                    break;
                case "--auto":
                    自動で進めるか = true;
                    break;
                default:
                    throw new ArgumentException($"知らない引数 {引数List[位置]}。使えるのは --scene 名前・--report ファイル・--auto");
            }
        }
        return new GalleryOptions(始める画面, 結果を書く, 自動で進めるか);
    }

    private static string 次の値(IReadOnlyList<string> 引数List, ref int 位置)
    {
        位置++;
        return 位置 < 引数List.Count ? 引数List[位置] : throw new ArgumentException($"{引数List[位置 - 1]} の後に値が無い");
    }
}
