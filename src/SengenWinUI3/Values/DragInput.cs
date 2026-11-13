using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>ドラッグ&ドロップの出来事の中身。出来事のたびに割り当てないよう構造体にした。名前は WinUI の DragEventArgs にそろえる。</summary>
public readonly struct DragInput
{
    private readonly WinUI.DragEventArgs 元の出来事;

    internal DragInput(WinUI.DragEventArgs 元の出来事) => this.元の出来事 = 元の出来事;

    /// <summary>ドラッグされているデータに、ファイルかフォルダ(WinUI の StorageItems)が入っているか。</summary>
    public bool ContainsStorageItems => 元の出来事.DataView.Contains(Windows.ApplicationModel.DataTransfer.StandardDataFormats.StorageItems);

    /// <summary>ドロップを受ける操作を決める。<see cref="DataPackageOperation.None"/> のままだと、ドロップを受けられない。</summary>
    public void SetAcceptedOperation(DataPackageOperation operation) => 元の出来事.AcceptedOperation = operation.ToWinUI();

    /// <summary>ドロップされたデータを非同期に読み終えるまで、ドラッグの元に待たせる札を返す。読み終えたら <see cref="DragOperationDeferral.Complete"/> を呼ぶ。</summary>
    public DragOperationDeferral GetDeferral() => new(元の出来事.GetDeferral());

    /// <summary>ドラッグされているデータの項目を、ドロップされた順に、種類つきで返す。</summary>
    public Task<IReadOnlyList<DroppedItem>> GetStorageItemsAsync() => Readドロップされた項目(元の出来事.DataView);

    private static async Task<IReadOnlyList<DroppedItem>> Readドロップされた項目(Windows.ApplicationModel.DataTransfer.DataPackageView データ)
    {
        IReadOnlyList<Windows.Storage.IStorageItem> 項目List = await データ.GetStorageItemsAsync();
        List<DroppedItem> 結果 = [];
        foreach (Windows.Storage.IStorageItem 項目 in 項目List)
        {
            if (項目 is Windows.Storage.StorageFile { Path.Length: > 0 } ファイル) 結果.Add(new DroppedFile(new FileInfo(ファイル.Path)));
            else 結果.Add(new DroppedOtherItem());
        }
        return 結果;
    }
}
