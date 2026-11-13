namespace SengenWinUI3;

/// <summary>ドラッグされているデータの1つの項目。ファイルの場所(パス)を持つファイルか、それ以外(フォルダ・場所を持たない項目)かを区別する。</summary>
public union DroppedItem(DroppedFile, DroppedOtherItem);
