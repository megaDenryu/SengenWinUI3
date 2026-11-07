namespace SengenWinUI3;

// 部品を子として持てるもの(部品・浮かぶ部品・ウインドウに置いた根)。子が Detach か Destroy で離れるとき、子から呼ばれる。
internal interface IChildOwner
{
    void ReleaseChild(Node child); // 子の本物を自分の本物から外し、子の覚えを捨てる
}
