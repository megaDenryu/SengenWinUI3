namespace SengenWinUI3;

internal static class VirtualKeyModifiersConversion
{
    internal static Windows.System.VirtualKeyModifiers ToWinUI(this VirtualKeyModifiers 修飾キー) =>
        (修飾キー.HasFlag(VirtualKeyModifiers.Control) ? Windows.System.VirtualKeyModifiers.Control : Windows.System.VirtualKeyModifiers.None)
        | (修飾キー.HasFlag(VirtualKeyModifiers.Menu) ? Windows.System.VirtualKeyModifiers.Menu : Windows.System.VirtualKeyModifiers.None)
        | (修飾キー.HasFlag(VirtualKeyModifiers.Shift) ? Windows.System.VirtualKeyModifiers.Shift : Windows.System.VirtualKeyModifiers.None)
        | (修飾キー.HasFlag(VirtualKeyModifiers.Windows) ? Windows.System.VirtualKeyModifiers.Windows : Windows.System.VirtualKeyModifiers.None);
}
