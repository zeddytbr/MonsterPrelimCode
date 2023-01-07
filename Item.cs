namespace PrelimCode;

public class Item
{
    public CellReference Position;
    private readonly char _icon;

    public Item(char icon, CellReference position)
    {
        _icon = icon;
        Position = position;
    }

    public bool CheckIfSameCell(CellReference otherPosition)
    {
        return Position.Equals(otherPosition);
    }

    public virtual char GetGridIcon()
    {
        return _icon;
    }
}