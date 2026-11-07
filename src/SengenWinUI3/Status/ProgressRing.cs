using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>円で進みを表示する部品。確定した進みは Ratio の 0〜1 で指定する。</summary>
public sealed class ProgressRing : Node, IControlNode
{
    private readonly WinUI.Controls.ProgressRing 本物 = new() { Minimum = 0, Maximum = 1 };
    private readonly ElementValue<WinUI.Controls.ProgressRing, ProgressState> 進み;
    private readonly ElementValue<WinUI.Controls.ProgressRing, bool> 動作中か;

    internal ProgressRing(bool isActive, ProgressState progress)
    {
        進み = new(本物,
            static (本物, 値) => ProgressDisplay.IsShown(値, 本物.IsIndeterminate, 本物.Value),
            static (本物, 値) =>
            {
                (bool 不確定か, double 割合) = ProgressDisplay.Target(値, 本物.Value);
                if (本物.Value != 割合) 本物.Value = 割合;
                if (本物.IsIndeterminate != 不確定か) 本物.IsIndeterminate = 不確定か;
            });
        動作中か = new(本物, static (本物, 値) => 本物.IsActive == 値, static (本物, 値) => 本物.IsActive = 値);
        動作中か.Set(isActive);
        進み.Set(progress);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>確定した割合か、不確定な進みへ表示を替える。</summary>
    public ProgressRing SetProgress(ProgressState progress)
    {
        ThrowIfDestroyed();
        進み.Set(progress);
        return this;
    }

    /// <summary>リングを動作中にするかを替える。</summary>
    public ProgressRing SetIsActive(bool isActive)
    {
        ThrowIfDestroyed();
        動作中か.Set(isActive);
        return this;
    }

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);
}
