namespace SengenWinUI3;

/// <summary>メニューに表示する文字と、押したときに呼ぶ処理。</summary>
public sealed record MenuFlyoutAction(string Text, Action Handler);
