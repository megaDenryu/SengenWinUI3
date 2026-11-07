using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>棒で進みを表示する部品。確定した進みは Ratio の 0〜1 で指定する。</summary>
public sealed class ProgressBar : Node, IControlNode
{
    private readonly WinUI.Controls.ProgressBar 本物 = new() { Minimum = 0, Maximum = 1 };
    private readonly ElementValue<WinUI.Controls.ProgressBar, ProgressState> 進み;

    internal ProgressBar(ProgressState progress)
    {
        進み = new(本物, static (本物, 値) => 値 switch
        {
            DeterminateProgress 確定 => !本物.IsIndeterminate && 本物.Value == 確定.Value.Value,
            IndeterminateProgress _ => 本物.IsIndeterminate,
        }, static (本物, 値) =>
        {
            (bool 不確定か, double 割合) = 値 switch
            {
                DeterminateProgress 確定 => (false, 確定.Value.Value),
                IndeterminateProgress _ => (true, 本物.Value),
            };
            if (本物.Value != 割合) 本物.Value = 割合;
            if (本物.IsIndeterminate != 不確定か) 本物.IsIndeterminate = 不確定か;
        });
        進み.Set(progress);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>確定した割合か、不確定な進みへ表示を替える。</summary>
    public ProgressBar SetProgress(ProgressState progress)
    {
        ThrowIfDestroyed();
        進み.Set(progress);
        return this;
    }

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);
}
