namespace PrelimCode;

/// <summary>
/// Wrapper class used to override the item functionality so the flask is not
/// visible on the screen.
/// </summary>
public class Flask : Item
{
    public Flask(char icon, CellReference position) : base(icon, position)
    {
    }

    public override char GetGridIcon()
    {
        return GameSettings.IsFlaskVisible ? _icon : ' ';
    }
}