using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>画面の中の1つのまとまりの頭に置く見出し。見出しより1段小さい。</summary>
public static class 小見出し
{
    /// <summary>本文を小見出しの大きさで見せる。</summary>
    public static TextBlock From本文(string 本文) => new() { Text = 本文, FontSize = 16 };
}
