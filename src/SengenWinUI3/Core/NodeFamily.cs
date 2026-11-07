namespace SengenWinUI3;

// 部品の親と子の台帳。破棄でたどる経路は、この子List だけである。
internal sealed class NodeFamily
{
    private List<Node>? 子List;

    internal IChildOwner? Owner { get; private set; }

    // WinUI の部品は親を1つしか持てない。黙って前の親から外すと、どこで付け替えたかがコードから読めなくなるので例外にする。
    internal void ThrowIfOwned(Node 自分)
    {
        if (Owner is not null)
        {
            throw new InvalidOperationException($"{自分.GetType().Name} はすでに親を持つ。別の親へ入れるときは、先に Detach を呼ぶ");
        }
    }

    internal void SetOwner(IChildOwner 持ち主) => Owner = 持ち主;

    internal void ForgetOwner() => Owner = null;

    internal void LeaveOwner(Node 自分)
    {
        IChildOwner? 持ち主 = Owner;
        Owner = null;
        持ち主?.ReleaseChild(自分);
    }

    internal void AddChild(Node 子) => (子List ??= []).Add(子);

    internal void RemoveChild(Node 子) => _ = 子List?.Remove(子);

    internal List<Node>? TakeChildren()
    {
        List<Node>? 持っていた子List = 子List;
        子List = null;
        return 持っていた子List;
    }
}
