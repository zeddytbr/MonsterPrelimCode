namespace PrelimCode;

public class Trap : Item
{
    private bool _isArmed;

    public Trap(char icon, CellReference position) : base(icon, position)
    {
        _isArmed = true;
    }

    public override char GetGridIcon()
    {
        return ' ';
    }

    public bool CheckForActivation(CellReference playerPosition)
    {
        if (!_isArmed || !Position.Equals(playerPosition)) return false;
        _isArmed = false;
        return true;

    }
}
