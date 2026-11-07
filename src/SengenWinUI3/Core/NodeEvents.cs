namespace SengenWinUI3;

// 全部の部品のチェーンに置く出来事。1つの部品で同じ出来事を2回受けたかを調べるために、受けた出来事を覚える。
[Flags]
internal enum NodeEvents
{
    None = 0,
    SizeChanged = 1 << 0,
    KeyDown = 1 << 1,
    PointerEntered = 1 << 2,
    PointerExited = 1 << 3,
    PointerPressed = 1 << 4,
    PointerReleased = 1 << 5,
}
