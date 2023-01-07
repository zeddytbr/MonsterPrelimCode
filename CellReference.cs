namespace PrelimCode;

public struct CellReference
{
    public int  CellX,
                CellY;
    
    public bool Equals(CellReference other)
    {
        return CellX == other.CellX && CellY == other.CellY;
    }
}