namespace SengenWinUI3;

internal static class DataPackageOperationConversion
{
    internal static Windows.ApplicationModel.DataTransfer.DataPackageOperation ToWinUI(this DataPackageOperation 操作) => 操作 switch
    {
        DataPackageOperation.None => Windows.ApplicationModel.DataTransfer.DataPackageOperation.None,
        DataPackageOperation.Copy => Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy,
        DataPackageOperation.Move => Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move,
        DataPackageOperation.Link => Windows.ApplicationModel.DataTransfer.DataPackageOperation.Link,
        _ => throw new ArgumentOutOfRangeException(nameof(操作), 操作, "知らないドラッグ&ドロップの操作である"),
    };
}
