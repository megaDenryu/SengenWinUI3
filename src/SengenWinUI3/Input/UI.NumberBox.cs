namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>単位を持たない数の入力欄であるため、Value・Minimum・Maximum は double で受ける。 見た目は Style で指定する。</summary>
    public static NumberBox NumberBox(double value, double minimum = double.MinValue, double maximum = double.MaxValue, Style? style = null) =>
        StyleIfGiven(new NumberBox(value, minimum, maximum), style);
}
