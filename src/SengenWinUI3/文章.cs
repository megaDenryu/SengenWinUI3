using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>画面の幅で折り返し、利用者が選んで写せる文章。</summary>
public static class 文章
{
    /// <summary>文字を折り返して見せる文章を作る。</summary>
    public static TextBlock From文字(string 文字) => new() { Text = 文字, TextWrapping = TextWrapping.Wrap, IsTextSelectionEnabled = true };
}
