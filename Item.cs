namespace PrelimCode;

/// <summary>
/// Representation of an in-game item with a position "in-world" on the grid.
/// Currently, all entities and items inherit from this base class.
/// </summary>
public class Item
{
    /// <summary>
    /// A 2D representation of the item's current position on the grid. This can
    /// be updated and changes will be reflected in the grid immediately, however
    /// the changes will only be visible to the player after the grid has been
    /// redrawn.
    /// </summary>
    public CellReference Position;
    /// <summary>
    /// A single character representing the item, this character is then drawn
    /// in the respective grid cell denoted by the Position property provided
    /// the functionality of the virtual GetGridIcon method has not been
    /// overriden to cause a different value to be drawn.
    /// </summary>
    public char Icon => _icon;
    protected readonly char _icon;

    protected Item(char icon, CellReference position)
    {
        _icon = icon;
        Position = position;
    }

    /// <summary>
    /// Wrapper method that compares the item's current position to the position
    /// provided.
    /// </summary>
    /// <param name="otherPosition">The external position to compare with.</param>
    /// <returns>Boolean denoting whether or not the position provided is shared
    /// by this item.</returns>
    public bool CheckIfSameCell(CellReference otherPosition)
    {
        return Position.Equals(otherPosition);
    }

    /// <summary>
    /// A method used by the Grid class to decide how to draw the item in its
    /// cell on the grid to the screen. Can be overriden by sub-classes to
    /// satisfy their own unique functionalities.
    /// </summary>
    /// <returns>Character representation of the item to be drawn to the grid.
    /// </returns>
    public virtual char GetGridIcon()
    {
        return _icon;
    }
}