namespace PrelimCode;

public class Game
{
    private Character _player = null!;
    private readonly Grid _cavern = new();
    private Enemy _monster = null!;     
    private Flask _flask = null!;
    private Trap _trap1 = null!;
    private Trap _trap2 = null!;

    public Game(bool isATrainingGame)
    {
        SetUpGame(isATrainingGame);
        Play();
    }

    private void Play()
    {
        _cavern.Display();
        
        while (true)
        {
            bool validMove;
            char moveDirection;
            do
            { 
                DisplayMoveOptions();
                moveDirection = GetMove();
                validMove = CheckValidMove(moveDirection);
            } while (!validMove);
            if (moveDirection == 'M') break;
            
            _player.MakeMove(moveDirection);
            _cavern.Display();
            
            bool flaskFound = _player.CheckIfSameCell(_flask.Position);
            if (flaskFound)
            { 
                DisplayWonGameMessage();
                break;
            } 
            
            bool hasBeenEaten = _monster.CheckIfSameCell(_player.Position);
            // This selection structure checks to see if the player has 
            // triggered one of the traps in the cavern
            if (!_monster.IsAwake 
                && !flaskFound && !hasBeenEaten 
                && (_trap1.CheckForActivation(_player.Position) 
                    || _trap2.CheckForActivation(_player.Position)))
            { 
                _monster.ToggleAwake();
                DisplayTrapMessage();
                _cavern.Display();
            } 
            if (_monster.IsAwake && !hasBeenEaten && !flaskFound)
            {
                int count = 0;
                do
                {
                    var monsterOldPosition = _monster.Position;
                    _monster.MoveTowardsPlayer(_player.Position);
                    if (_monster.CheckIfSameCell(_flask.Position))
                    {
                        _flask.Position = monsterOldPosition;
                    } 
                    hasBeenEaten = _monster.CheckIfSameCell(_player.Position);
                    Console.WriteLine();
                    Console.WriteLine("Press Enter key to continue");
                    Console.ReadLine();
                    _cavern.Display();
                    count++;
                } while (count != 2 && !hasBeenEaten);
            }

            if (!hasBeenEaten) continue;
            DisplayLostGameMessage();
            break;
        }
    }

    private static void DisplayMoveOptions()
    { 
        // TODO: Make this account for invalid moves, implementing border checks
        // based on the players position.
        Console.WriteLine();
        Console.WriteLine("Enter N to move NORTH");
        Console.WriteLine("Enter S to move SOUTH");
        Console.WriteLine("Enter E to move EAST");
        Console.WriteLine("Enter W to move WEST");
        Console.WriteLine("Enter M to return to the Main Menu");
        Console.WriteLine();
    }

    private static char GetMove()
    {
        string? input = Console.ReadLine();
        if (input == null || input.Trim().Length == 0)
        {
            return ' ';
        }

        char move = input.Trim().ToUpper().ToCharArray()[0]; 
        Console.WriteLine();
        return move;
    }

    private static void DisplayWonGameMessage()
    { 
        Console.WriteLine("Well done! you have found the flask containing the Styxian potion.");
        Console.WriteLine("You have won the game of MONSTER!");
        Console.WriteLine();
    }

    private static void DisplayTrapMessage()
    { 
        Console.WriteLine("Oh no! You have set off a trap. Watch out, the monster is now awake!");
        Console.WriteLine();
    }

    private static void DisplayLostGameMessage()
    { 
        Console.WriteLine("ARGHHHHHH! The monster has eaten you. GAME OVER.");
        Console.WriteLine("Maybe you will have better luck next time you play MONSTER!");
        Console.WriteLine();
    }

    private static bool CheckValidMove(char direction)
    {
        // TODO: Some form of border check. Maybe a dynamic valid move list?
        // i.e. check player position, display moves appropriately.
        bool validMove = direction is 'N' or 'S' or 'W' or 'E' or 'M';
        return validMove;
    }

    private void SetUpGame(bool isTraining)
    {
        _cavern.Reset();
        
        _player = new Character(
            '*', 
            isTraining 
                ? new CellReference { CellX = 4, CellY = 2 } 
                : new CellReference { CellX = 0, CellY = 0 }
        );
        _cavern.AddItem(_player);
        
        _trap1 = new Trap(
            'T',
            isTraining 
                ? new CellReference { CellX = 6, CellY = 2 }
                : _cavern.GetRandEmptyCell()
        );
        _cavern.AddItem(_trap1);
        
        _trap2 = new Trap(
            'T',
            isTraining 
                ? new CellReference { CellX = 4, CellY = 3 }
                : _cavern.GetRandEmptyCell()
        );
        _cavern.AddItem(_trap2);
        
        _monster = new Enemy(
            'M',
            isTraining 
                ? new CellReference { CellX = 4, CellY = 0 }
                : _cavern.GetRandEmptyCell()
        );
        _cavern.AddItem(_monster);
        
        _flask = new Flask(
            'F',
            isTraining 
                ? new CellReference { CellX = 3, CellY = 1 }
                : _cavern.GetRandEmptyCell()
        );
        _cavern.AddItem(_flask);
    }
}