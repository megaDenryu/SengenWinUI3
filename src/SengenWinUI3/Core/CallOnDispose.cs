namespace SengenWinUI3;

// 出来事の購読を解く処理や、一緒に破棄する部品の Destroy を、後片付けの対象List へ入れるための包み。
internal sealed class CallOnDispose(Action 後片付けの処理) : IDisposable
{
    public void Dispose() => 後片付けの処理();
}
