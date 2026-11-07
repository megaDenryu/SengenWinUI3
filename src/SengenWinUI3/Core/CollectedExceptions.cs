namespace SengenWinUI3;

// 破棄の途中で起きた例外を集め、全部の後片付けを済ませてからまとめて投げる。1つの失敗で、残りの購読と子孫の破棄が止まらないようにするためである(状態の層の知らせと同じ形)。
internal sealed class CollectedExceptions
{
    private List<Exception>? 起きた例外List;

    internal void Run(Action 後片付けの処理)
    {
        try
        {
            後片付けの処理();
        }
        catch (Exception 例外)
        {
            (起きた例外List ??= []).Add(例外);
        }
    }

    internal void ThrowIfAny()
    {
        if (起きた例外List is not null)
        {
            throw new AggregateException(起きた例外List);
        }
    }
}
