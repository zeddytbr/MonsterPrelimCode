namespace PrelimCode;
 
public class Enemy : Item
{
    public bool IsAwake { get; private set; }

    public Enemy(char icon, CellReference position) : base(icon, position)
    { 
        IsAwake = false; 
    }

    public override char GetGridIcon()
    {
        return IsAwake ? 'M' : ' ';
    }

    public void MoveTowardsPlayer(CellReference playerPosition)      
    {
        // Nasty else-if chain to ensure monster doesn't move more than
        // a single square cardinally each move. 
        if (Position.CellY < playerPosition.CellY)
        { 
            Position.CellY++;
        } 
        else if (Position.CellY > playerPosition.CellY)
        {
            Position.CellY--;
        } 
        // Breaking the chain here would permit diagonal movement.
        // VVVVV
        else if (Position.CellX < playerPosition.CellX)
        {
            Position.CellX++;
        } 
        else if (Position.CellX > playerPosition.CellX)
        {
            Position.CellX--;
        } 
    }

    public void ToggleAwake() => IsAwake = !IsAwake;
}
