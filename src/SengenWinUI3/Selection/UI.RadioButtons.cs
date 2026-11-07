namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>未選択の選択肢の集まりを作る。子は Children で入れ、初期選択はその後で SetSelectedIndex へ渡す。</summary>
    public static RadioButtons RadioButtons(Style? style = null) => StyleIfGiven(new RadioButtons(), style);

    /// <summary>項目を入れてから初期選択を設定した選択肢の集まりを作る。</summary>
    public static RadioButtons RadioButtons(Node?[] children, SelectionIndex selectedIndex, Style? style = null) =>
        StyleIfGiven(new RadioButtons().Children(children).SetSelectedIndex(selectedIndex), style);
}
