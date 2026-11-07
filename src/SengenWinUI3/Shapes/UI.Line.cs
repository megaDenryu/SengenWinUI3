namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>部品の内側の始点と終点を結ぶ Line を作る。</summary>
    public static Line Line(Px x1, Px y1, Px x2, Px y2, Style? style = null) => StyleIfGiven(new Line(x1, y1, x2, y2), style);
}
