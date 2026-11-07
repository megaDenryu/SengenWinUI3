namespace SengenWinUI3;

// ContentSlot の利用者は、中身を外す処理も必ず実装する。
internal interface IContentOwner : IChildOwner
{
    void RemoveChildElement(Node child);
}
