namespace SengenWinUI3;

/// <summary>構築の後に配線を1回だけ受け取る部品。自分を返すので、木の中で置きながら配線できる。</summary>
public interface IWirable<TSelf, TWiring> where TSelf : IWirable<TSelf, TWiring>
{
    /// <summary>配線を受け取り、自分を返す。</summary>
    TSelf Wire(TWiring wiring);
}
