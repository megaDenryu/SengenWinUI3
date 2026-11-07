namespace SengenWinUI3;

/// <summary>数から <see cref="Degrees"/> を作る拡張のプロパティ。<c>90.Degrees</c> と書くためにある。</summary>
public static class DegreesExtensions
{
    extension(int value)
    {
        /// <summary>この数を角度(度)にする。</summary>
        public Degrees Degrees => new(value);
    }

    extension(double value)
    {
        /// <summary>この数を角度(度)にする。</summary>
        public Degrees Degrees => new(value);
    }
}
