namespace SengenWinUI3;

/// <summary>ドラッグ&ドロップで、ドロップを受ける側が許す操作。名前は WinUI の DataPackageOperation にそろえる。</summary>
public enum DataPackageOperation
{
    None,
    Copy,
    Move,
    Link,
}
