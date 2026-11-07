namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>道の文字列で書いた輪郭を描く Path を作る。Stretch は指定しないので、輪郭は道の座標のままの大きさで描かれる。</summary>
    public static Path Path(PathData data, Style? style = null) => StyleIfGiven(new Path(data), style);
}
