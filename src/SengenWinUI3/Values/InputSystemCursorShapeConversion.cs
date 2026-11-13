namespace SengenWinUI3;

internal static class InputSystemCursorShapeConversion
{
    internal static Microsoft.UI.Input.InputSystemCursorShape ToWinUI(this InputSystemCursorShape 形) => 形 switch
    {
        InputSystemCursorShape.Arrow => Microsoft.UI.Input.InputSystemCursorShape.Arrow,
        InputSystemCursorShape.Cross => Microsoft.UI.Input.InputSystemCursorShape.Cross,
        InputSystemCursorShape.Hand => Microsoft.UI.Input.InputSystemCursorShape.Hand,
        InputSystemCursorShape.Help => Microsoft.UI.Input.InputSystemCursorShape.Help,
        InputSystemCursorShape.IBeam => Microsoft.UI.Input.InputSystemCursorShape.IBeam,
        InputSystemCursorShape.SizeAll => Microsoft.UI.Input.InputSystemCursorShape.SizeAll,
        InputSystemCursorShape.SizeNortheastSouthwest => Microsoft.UI.Input.InputSystemCursorShape.SizeNortheastSouthwest,
        InputSystemCursorShape.SizeNorthSouth => Microsoft.UI.Input.InputSystemCursorShape.SizeNorthSouth,
        InputSystemCursorShape.SizeNorthwestSoutheast => Microsoft.UI.Input.InputSystemCursorShape.SizeNorthwestSoutheast,
        InputSystemCursorShape.SizeWestEast => Microsoft.UI.Input.InputSystemCursorShape.SizeWestEast,
        InputSystemCursorShape.UniversalNo => Microsoft.UI.Input.InputSystemCursorShape.UniversalNo,
        InputSystemCursorShape.UpArrow => Microsoft.UI.Input.InputSystemCursorShape.UpArrow,
        InputSystemCursorShape.Wait => Microsoft.UI.Input.InputSystemCursorShape.Wait,
        InputSystemCursorShape.Pin => Microsoft.UI.Input.InputSystemCursorShape.Pin,
        InputSystemCursorShape.Person => Microsoft.UI.Input.InputSystemCursorShape.Person,
        InputSystemCursorShape.AppStarting => Microsoft.UI.Input.InputSystemCursorShape.AppStarting,
        _ => throw new ArgumentOutOfRangeException(nameof(形), 形, "知らないカーソルの形である"),
    };
}
