namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>未選択の一覧を作る。子は Children で入れ、初期選択はその後で SetSelectedIndex へ渡す。</summary>
    public static ListView ListView(Style? style = null) => StyleIfGiven(new ListView(), style);

    /// <summary>項目を入れてから初期選択を設定した一覧を作る。</summary>
    public static ListView ListView(Node?[] children, SelectionIndex selectedIndex, Style? style = null) =>
        StyleIfGiven(new ListView().Children(children).SetSelectedIndex(selectedIndex), style);
}
