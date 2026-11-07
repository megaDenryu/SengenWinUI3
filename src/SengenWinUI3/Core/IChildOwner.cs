namespace SengenWinUI3;

// 部品を子として持てるもの(部品・浮かぶ部品・ウインドウ)。子の本物を自分の本物へ入れることと、子として登録することを1つの操作にする。
internal interface IChildOwner
{
    void AdoptChild(Node child, Action 本物へ入れる); // 本物へ入れてから登録する。入れるのに失敗したら登録しない
    void ThrowIfCannotRelease(Node child) { } // 子が離れる前に、状態を変えずに呼ばれる。外せない子なら例外を投げる

    void ReleaseChild(Node child); // 子が Detach か Destroy で離れるとき、子から呼ばれる。子の本物を外し、子の覚えを捨てる
}
