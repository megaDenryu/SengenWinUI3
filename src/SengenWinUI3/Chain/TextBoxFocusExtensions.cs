using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>キーボードのフォーカスが TextBox にあるかを調べるチェーン。</summary>
public static class TextBoxFocusExtensions
{
    /// <summary>この部品が載っている画面で、キーボードのフォーカスが TextBox にあるか。部品が画面に載っていなければ false。</summary>
    public static bool IsTextBoxFocused(this Node node)
    {
        node.ThrowIfDestroyed();
        WinUI.XamlRoot? 根 = node.Element.XamlRoot;
        return 根 is not null && WinUI.Input.FocusManager.GetFocusedElement(根) is WinUI.Controls.TextBox;
    }
}
