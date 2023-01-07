namespace PrelimCode;

public class Flask : Item
{
    public Flask(char icon, CellReference position) : base(icon, position)
    {
    }

    public override char GetGridIcon()
    {
        return ' ';
    }
}