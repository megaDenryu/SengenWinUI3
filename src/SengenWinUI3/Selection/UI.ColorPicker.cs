namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>指定した色で ColorPicker を作る。表示する入力欄は WinUI の既定に従う。</summary>
    public static ColorPicker ColorPicker(Color color, Style? style = null) => StyleIfGiven(new ColorPicker(color), style);
}
