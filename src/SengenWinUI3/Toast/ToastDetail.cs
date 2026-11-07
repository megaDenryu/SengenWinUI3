namespace SengenWinUI3;

// トーストの詳細の文。無いか、文があるかのどちらか。
internal union ToastDetail(NoToastDetail, ToastDetailText);

internal sealed record NoToastDetail;

internal sealed record ToastDetailText(string Text);
