namespace SengenWinUI3;

// トーストに付けるボタン。無いか、文字と押したときの処理があるかのどちらか。
internal union ToastButton(NoToastButton, ToastButtonContent);

internal sealed record NoToastButton;

internal sealed record ToastButtonContent(string Text, Action OnClick);
