namespace SengenWinUI3;

/// <summary>0 以上 1 以下の割合。不透明度に使う。</summary>
public readonly record struct Ratio
{
    /// <summary>0 以上 1 以下でない値を渡すと例外を投げる。</summary>
    public Ratio(double value)
    {
        if (!(value is >= 0.0 and <= 1.0))
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "割合は 0 以上 1 以下である");
        }
        Value = value;
    }

    /// <summary>割合の値(0〜1)。</summary>
    public double Value { get; }
}
