namespace PrelimCode;

/// <summary>
/// Represents a grid or "playing field" upon which items are placed and entities
/// such as the player and monster traverse.
/// </summary>
public class Grid
{
    /// <summary>
    /// List containing all items that are considered "in use" on the playing
    /// field / game grid.
    /// </summary>
    private readonly List<Item> _itemContentList = new();
    
    /// <summary>
    /// Draws the world grid to the screen, querying cells for their contents
    /// before rendering them to the screen. Clears the entire terminal in doing
    /// so.
    /// </summary>
    public void DrawToScreen()
    {
        Console.Clear();
        for (int cellY = 0; cellY <= GameSettings.CavernHeightInCells; cellY++)
        {
            Console.WriteLine(" ------------- ");
            for (int cellX = 0; cellX <= GameSettings.CavernWidthInCells; cellX++)
            {
                var currentCellContents = GetCell(
                    new CellReference { CellX = cellX, CellY = cellY }
                );
                if (currentCellContents != null)
                {
                    Console.Write($"|{currentCellContents.GetGridIcon()}");
                    continue;
                }
                Console.Write("| ");
            }

            Console.WriteLine("|");
        }

        Console.WriteLine(" ------------- ");
        Console.WriteLine();
        
        // Compiler warning that originates with constants.
        #pragma warning disable CS0162
        if (!GameSettings.IsPositionDebugEnabled) return;

        Console.WriteLine("DEBUG: ItemPositions");
        foreach (var item in _itemContentList)
        {
            Console.Write($"{item.Icon}({item.Position.CellX}, {item.Position.CellY}) ");
        }
        Console.WriteLine();
        #pragma warning restore CS0162
    }

    /// <summary>
    /// Adds an item to the contents of the grid at the item's position.
    /// </summary>
    /// <param name="item">The item to be added to the grid's contents.</param>
    public void AddItem(Item item) => _itemContentList.Add(item);
    /// <summary>
    /// Removes an item from the contents of the grid.
    /// </summary>
    /// <param name="item">Item to be removed from the grid's contents.</param>
    public void RemoveItem(Item item) => _itemContentList.Remove(item);
    /// <summary>
    /// Clears all items from the grid's contents, effectively reverting all
    /// cells to a default 'empty' state.
    /// </summary>
    public void Reset() => _itemContentList.Clear();

    /// <summary>
    /// Query's the contents of the grid, returning the first item that exists
    /// at the position specified.
    /// </summary>
    /// <param name="position">CellReference denoting which cell to query for
    /// its contents.</param>
    /// <returns>If an item is found at the position specified, it is returned.
    /// If no such item exists, null is returned</returns>
    private Item? GetCell(CellReference position)
    {
        Item? returnItem = null;

        foreach (var item in _itemContentList.Where(item => 
                     item.Position.Equals(position)))
        {
            return item;
        }
        
        return returnItem;
    }
    
    /// <summary>
    /// Method used internally to retrieve a reference to the position of a cell
    /// within the bounds of the grid. Used by GetRandEmptyCell - CellReferences
    /// returned by this method are not guaranteed to be empty therefore it is
    /// advisable to use the aforementioned method instead.
    /// </summary>
    /// <returns>
    /// CellReference to a random cell within the bounds of the grid.
    /// </returns>
    private static CellReference GetRandomCell()
    { 
        CellReference position;
        var rnd = new Random();
        position.CellY = rnd.Next(0, GameSettings.CavernHeightInCells + 1); 
        position.CellX = rnd.Next(0, GameSettings.CavernWidthInCells + 1);  
        return position;
    }

    /// <summary>
    /// Retrieve a reference to an empty cell on the grid.
    /// </summary>
    /// <returns>CellReference linking to an empty cell at a random location
    /// withing the bounds of the grid.</returns>
    public CellReference GetRandEmptyCell()
    { 
        CellReference position;
        do
        { 
            position = GetRandomCell();
        } while(GetCell(position) != null);
        return position;
    }
}
