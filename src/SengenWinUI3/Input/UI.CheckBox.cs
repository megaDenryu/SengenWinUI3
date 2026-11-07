namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>2つの状態だけを持つチェック欄。 見た目は Style で指定する。</summary>
    public static CheckBox CheckBox(bool isChecked, string content, Style? style = null) =>
        StyleIfGiven(new CheckBox(isChecked, content), style);
}
