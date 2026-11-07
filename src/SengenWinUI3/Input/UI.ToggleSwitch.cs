namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>オンとオフを切り替える部品。 見た目は Style で指定する。</summary>
    public static ToggleSwitch ToggleSwitch(bool isOn, string header, Style? style = null) =>
        StyleIfGiven(new ToggleSwitch(isOn, header), style);
}
