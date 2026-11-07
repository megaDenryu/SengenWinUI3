namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>2つの状態だけを持つ押し込みボタン。 見た目は Style で指定する。</summary>
    public static ToggleButton ToggleButton(bool isChecked, string content, Style? style = null) =>
        StyleIfGiven(new ToggleButton(isChecked, content), style);
}
