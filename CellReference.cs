namespace PrelimCode;

/// <summary>
/// A structure representing a 2D position on the grid.
/// </summary>
public struct CellReference
{
    public int  CellX,
                CellY;
    
    public bool Equals(CellReference other)
    {
        return CellX == other.CellX && CellY == other.CellY;
    }
}