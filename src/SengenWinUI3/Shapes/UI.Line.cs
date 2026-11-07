namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>部品の内側の始点と終点を結ぶ Line を作る。</summary>
    public static Line Line(Point start, Point end, Style? style = null) => StyleIfGiven(new Line(start, end), style);
}
