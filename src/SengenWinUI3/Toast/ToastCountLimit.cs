namespace SengenWinUI3;

// 同時に出すトーストの数の上限。上限なしか、数を決めるかのどちらか。
internal union ToastCountLimit(UnlimitedToasts, LimitedToasts);

internal sealed record UnlimitedToasts;

internal sealed record LimitedToasts(int Count);
