namespace PrelimCode;

/// <summary>
/// Representation of an in-game trap that is triggered once after the player
/// initially "steps" on it. This is used as a signal to awaken the monster
/// during gameplay.
/// </summary>
public class Trap : Item
{
    /// <summary>
    /// An internal flag used to check if the player has already stepped on the
    /// trap before, preventing reactivation.
    /// </summary>
    private bool _isArmed;

    public Trap(char icon, CellReference position) : base(icon, position)
    {
        _isArmed = true;
    }

    public override char GetGridIcon()
    {
        // Traps should be invisible to the player until after they have been
        // triggered.
        return (_isArmed && !GameSettings.AreTrapsAlwaysVisible) ? ' ' : _icon;
    }

    /// <summary>
    /// Checks if the player has "stepped" on the trap whilst it is also armed.
    /// </summary>
    /// <param name="playerPosition">
    /// CellReference denoting the player's current location.
    /// </param>
    /// <returns>boolean value of if the trap has just been activated.</returns>
    public bool CheckForActivation(CellReference playerPosition)
    {
        if (!_isArmed || !Position.Equals(playerPosition)) return false;
        
        // This ensures that the above guard clause will always cause the method
        // to terminate from here on out, preventing the player from reactivating
        // the trap after it has already been triggered.
        _isArmed = false;
        
        return true;
    }
}
