namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>画素から作った画像を見せる <see cref="SengenWinUI3.Image"/> を作る。</summary>
    public static Image Image(BgraPixels pixels, Style? style = null) => StyleIfGiven(new Image(pixels), style);

    /// <summary>画像の元をまだ持たない <see cref="SengenWinUI3.Image"/> を作る。元は <see cref="SengenWinUI3.Image.SetSource(BgraPixels)"/> などで後から渡す。</summary>
    public static Image Image(Style? style = null) => StyleIfGiven(new Image(), style);
}
