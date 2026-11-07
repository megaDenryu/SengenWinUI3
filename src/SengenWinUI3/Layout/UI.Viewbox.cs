namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>中身を1つ持つ Viewbox を作る。中身は縦横比を保って Viewbox の大きさに合わせる。</summary>
    public static Viewbox Viewbox(Node content, Style? style = null) => StyleIfGiven(new Viewbox(content), style);
}
