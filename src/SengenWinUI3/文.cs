using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>画面の幅で折り返し、利用者が選んで写せる文。</summary>
public static class 文
{
    /// <summary>本文を折り返して見せる文を作る。</summary>
    public static TextBlock From本文(string 本文) => new() { Text = 本文, TextWrapping = TextWrapping.Wrap, IsTextSelectionEnabled = true };
}
