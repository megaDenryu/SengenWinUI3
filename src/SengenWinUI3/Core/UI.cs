namespace SengenWinUI3;

/// <summary>
/// 葉の部品を作る関数。関数の名前は部品の型の名前と同じであり、<c>using static SengenWinUI3.UI;</c> で取り込んで <c>TextBlock("…")</c> と書く。
/// 部品の種類ごとのファイルに partial で置く。どの関数も最後の引数で省ける <see cref="Style"/> を受け、作った直後に <c>.Style(style)</c> で当てる。
/// </summary>
public static partial class UI
{
    // 作る関数で渡した Style と後から .Style で当てた Style の結果が食い違わないよう、同じ拡張メソッドを通す。
    private static T StyleIfGiven<T>(T node, Style? style) where T : Node => style is null ? node : node.Style(style);
}
