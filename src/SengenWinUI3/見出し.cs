using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>画面の一番上に置く、画面の名前を示す見出し。</summary>
public static class 見出し
{
    /// <summary>本文を見出しの大きさで見せる。</summary>
    public static TextBlock From本文(string 本文) => new() { Text = 本文, FontSize = 20 };
}
