namespace SengenWinUI3;

internal static class PointerUpdateKindConversion
{
    internal static PointerUpdateKind ToPointerUpdateKind(this Microsoft.UI.Input.PointerUpdateKind 本物の種類) => 本物の種類 switch
    {
        Microsoft.UI.Input.PointerUpdateKind.LeftButtonPressed => PointerUpdateKind.LeftButtonPressed,
        Microsoft.UI.Input.PointerUpdateKind.LeftButtonReleased => PointerUpdateKind.LeftButtonReleased,
        Microsoft.UI.Input.PointerUpdateKind.RightButtonPressed => PointerUpdateKind.RightButtonPressed,
        Microsoft.UI.Input.PointerUpdateKind.RightButtonReleased => PointerUpdateKind.RightButtonReleased,
        Microsoft.UI.Input.PointerUpdateKind.MiddleButtonPressed => PointerUpdateKind.MiddleButtonPressed,
        Microsoft.UI.Input.PointerUpdateKind.MiddleButtonReleased => PointerUpdateKind.MiddleButtonReleased,
        Microsoft.UI.Input.PointerUpdateKind.XButton1Pressed => PointerUpdateKind.XButton1Pressed,
        Microsoft.UI.Input.PointerUpdateKind.XButton1Released => PointerUpdateKind.XButton1Released,
        Microsoft.UI.Input.PointerUpdateKind.XButton2Pressed => PointerUpdateKind.XButton2Pressed,
        Microsoft.UI.Input.PointerUpdateKind.XButton2Released => PointerUpdateKind.XButton2Released,
        _ => PointerUpdateKind.Other,
    };
}
