using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>画面の中の1つのまとまりの頭に置く見出し。見出しより1段小さい。</summary>
public static class 小見出し
{
    /// <summary>文字を小見出しの大きさで見せる。</summary>
    public static TextBlock From文字(string 文字) => new() { Text = 文字, FontSize = 16 };
}
