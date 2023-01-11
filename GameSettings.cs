// ReSharper disable RedundantLogicalConditionalExpressionOperand - more fun constant errors
namespace PrelimCode;

/// <summary>
/// Fairly self-explanatory name. Contains values used internally by the game,
/// exposing them as constants here ensures no inconsistency between multiple
/// declarations or difficulty modifying gameplay parameters.
/// </summary>
public struct GameSettings
{
    public const int    CavernHeightInCells = 4,
                        CavernWidthInCells  = 6;

    private const bool  IsFullDebugEnabled = false;

    public const bool   IsPositionDebugEnabled = false || IsFullDebugEnabled,
                        IsFlaskVisible = false || IsFullDebugEnabled,
                        AreMonstersAlwaysVisible = false || IsFullDebugEnabled,
                        AreTrapsAlwaysVisible = false || IsFullDebugEnabled;
}