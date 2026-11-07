namespace SengenWinUI3;

/// <summary>数から <see cref="Px"/> を作る拡張のプロパティ。<c>12.Px</c> と書くためにある。</summary>
public static class PxExtensions
{
    extension(int value)
    {
        /// <summary>この数を長さ(Px)にする。</summary>
        public Px Px => new(value);
    }

    extension(double value)
    {
        /// <summary>この数を長さ(Px)にする。</summary>
        public Px Px => new(value);
    }
}
