using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>Slider の線・値の側の線・つまみの塗り。押せるあいだの3つの状態(ふだん・重ねた・押した)に同じ塗りを置く。null の項目は既定の色のままにする。</summary>
public readonly record struct SliderColors
{
    private static readonly string[] 状態の末尾List = ["", "PointerOver", "Pressed"];

    /// <summary>全体の線の塗り。</summary>
    public Brush? Track { get; init; }
    /// <summary>つまみより値の側の線の塗り。</summary>
    public Brush? TrackValue { get; init; }
    /// <summary>つまみの塗り。</summary>
    public Brush? Thumb { get; init; }

    internal void WriteTo(WinUI.FrameworkElement 本物)
    {
        全ての状態へ書く(本物, "SliderTrackFill", Track);
        全ての状態へ書く(本物, "SliderTrackValueFill", TrackValue);
        全ての状態へ書く(本物, "SliderThumbBackground", Thumb);
    }

    // ブラシは1つ作り、3つの資源名で共有する。
    private static void 全ての状態へ書く(WinUI.FrameworkElement 本物, string 資源名の頭, Brush? 塗り)
    {
        if (塗り is not { } 指定の塗り)
        {
            return;
        }
        WinUI.Media.Brush 共有する塗り = 指定の塗り.ToWinUI();
        foreach (string 状態の末尾 in 状態の末尾List)
        {
            本物.Resources[資源名の頭 + 状態の末尾] = 共有する塗り;
        }
    }
}
