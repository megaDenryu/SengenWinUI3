namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>未選択の候補欄を作る。子は Children で入れ、初期選択はその後で SetSelectedIndex へ渡す。</summary>
    public static ComboBox ComboBox(Style? style = null) => StyleIfGiven(new ComboBox(), style);

    /// <summary>項目を入れてから初期選択を設定した候補欄を作る。</summary>
    public static ComboBox ComboBox(IEnumerable<Node?> children, SelectionIndex selectedIndex, Style? style = null) =>
        StyleIfGiven(new ComboBox().Children(children).SetSelectedIndex(selectedIndex), style);
}
