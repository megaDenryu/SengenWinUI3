namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>中身を1つ持つ Border を作る。</summary>
    public static Border Border(Node content, Style? style = null) => StyleIfGiven(new Border(content), style);
}
