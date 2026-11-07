namespace SengenWinUI3.State;

/// <summary>値が変わったときに知らせるセル。値の型を問わず購読できる。</summary>
public interface ICell
{
    /// <summary>値が変わったときに処理を呼ぶ。返した値を Dispose すると購読を解く。</summary>
    IDisposable Subscribe(Action onChanged);
}
