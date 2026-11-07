namespace SengenWinUI3;

// ProgressBar と ProgressRing が、ProgressState と本物の IsIndeterminate・Value の対応を同じ判断で使うための置き場。
internal static class ProgressDisplay
{
    // 確定なら不確定の表示でなく割合も等しい。不確定なら不確定の表示である(割合は見ない)。
    internal static bool IsShown(ProgressState 状態, bool 本物の不確定か, double 本物の割合) => 状態 switch
    {
        DeterminateProgress 確定 => !本物の不確定か && 本物の割合 == 確定.Value.Value,
        IndeterminateProgress _ => 本物の不確定か,
    };

    // 不確定のときは、動かさないために今の割合を保つ。
    internal static (bool 不確定か, double 割合) Target(ProgressState 状態, double 本物の割合) => 状態 switch
    {
        DeterminateProgress 確定 => (false, 確定.Value.Value),
        IndeterminateProgress _ => (true, 本物の割合),
    };
}
