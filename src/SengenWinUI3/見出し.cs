using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>画面の一番上に置く、画面の名前を示す見出し。</summary>
public static class 見出し
{
    /// <summary>文字を見出しの大きさで見せる。</summary>
    public static TextBlock From文字(string 文字) => new() { Text = 文字, FontSize = 20 };
}
