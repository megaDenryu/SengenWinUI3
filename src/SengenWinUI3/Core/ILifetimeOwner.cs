namespace SengenWinUI3;

// 寿命を持つもの(部品と浮かぶ部品)。出来事の配線(EventWiring)は、この型を受けて両方で同じ保護を使う。
internal interface ILifetimeOwner
{
    Lifetime Lifetime { get; }
}
