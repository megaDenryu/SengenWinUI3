namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>位置を指定して子を置く Canvas を作る。子は Children で入れる。</summary>
    public static Canvas Canvas(Style? style = null) => StyleIfGiven(new Canvas(), style);
}
