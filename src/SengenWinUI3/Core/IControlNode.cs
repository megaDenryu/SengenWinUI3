using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>本物が Control の部品の目印。使えるかどうか(IsEnabled)とフォーカスを持つ。SengenWinUI3 の外からは実装できない。</summary>
public interface IControlNode
{
    internal WinUI.Controls.Control Control { get; }
}
