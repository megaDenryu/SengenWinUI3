namespace SengenWinUI3;

/// <summary>トーストの消え方。時間が過ぎると消えるか、閉じられるまで残るかのどちらか。</summary>
public union ToastLifetime(TimedLifetime, UntilClosedLifetime);
