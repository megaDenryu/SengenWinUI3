namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>数を選ぶスライダー。Value・Minimum・Maximum・StepFrequency は長さや割合に限定しない単位の無い数である。 見た目は Style で指定する。</summary>
    public static Slider Slider(double value, double minimum = 0, double maximum = 100, double stepFrequency = 1, Style? style = null) =>
        StyleIfGiven(new Slider(value, minimum, maximum, stepFrequency), style);
}
