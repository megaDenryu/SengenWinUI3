namespace SengenWinUI3.State;

/// <summary>値を読めて、変わったときに新しい値を受け取れるセル。</summary>
public interface IReadOnlyCell<out T> : ICell
{
    /// <summary>今の値。</summary>
    T Value { get; }

    /// <summary>値が変わったときに新しい値で処理を呼ぶ。返した値を Dispose すると購読を解く。</summary>
    IDisposable Subscribe(Action<T> onChanged);
}
