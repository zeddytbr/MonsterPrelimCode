namespace PrelimCode;

public class Grid
{
    private readonly List<Item> _itemContentList = new();
    
    public void Display()
    {
        for (int cellY = 0; cellY <= GameSettings.CavernHeightInCells; cellY++)
        {
            Console.WriteLine(" ------------- ");
            for (int cellX = 0; cellX <= GameSettings.CavernWidthInCells; cellX++)
            {
                var currentCellContents = GetCell(new CellReference { CellX = cellX, CellY = cellY });
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
    }

    public void AddItem(Item item) => _itemContentList.Add(item);
    public void RemoveItem(Item item) => _itemContentList.Remove(item);
    public void Reset() => _itemContentList.Clear();

    private Item? GetCell(CellReference position)
    {
        Item? returnItem = null;

        foreach (var item in _itemContentList.Where(item => item.Position.Equals(position)))
        {
            return item;
        }
        
        return returnItem;
    }
    
    private static CellReference GetRandomCell()
    { 
        CellReference position;
        var rnd = new Random();
        position.CellY = rnd.Next(0, GameSettings.CavernHeightInCells + 1); 
        position.CellX = rnd.Next(0, GameSettings.CavernWidthInCells + 1);  
        return position;
    }

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
