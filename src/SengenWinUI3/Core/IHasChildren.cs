using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>子を何個でも持てる部品の目印。本物が Panel の部品だけが実装し、SengenWinUI3 の外からは実装できない。</summary>
public interface IHasChildren
{
    internal WinUI.Controls.Panel ChildPanel { get; }
}
