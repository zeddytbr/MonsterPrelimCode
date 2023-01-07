namespace PrelimCode;

public class Character : Item
{
    public Character(char icon, CellReference position) : base(icon, position)
    {
    }
  
    public void MakeMove(char direction)                      
    {                                                         
        switch (direction)                                      
        {                                                       
            case 'N' : Position.CellY--;       
                break;                                     
            case 'S' : Position.CellY++;       
                break;                                     
            case 'W' : Position.CellX--;         
                break;                                     
            case 'E' : Position.CellX++;         
                break;                                     
        }                                                       
    }                                                         
}