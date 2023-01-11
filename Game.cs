namespace PrelimCode;

/// <summary>
/// Class representation of an entire "dungeon dive" or play-through of the game.
/// Terminates once the game has finished (either through victory, loss or player
/// intervention).
/// </summary>
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

    /// <summary>
    /// A method containing the core game-loop functionality. Returns upon game
    /// completion, whether it be victory or loss.
    /// </summary>
    private void Play()
    {
        _cavern.DrawToScreen();
        
        while (true)
        {
            // Query the player for an input and don't continue unless it's valid.
            char chosenMove;
            do
            { 
                DisplayMoveOptions();
                chosenMove = GetMove();
            } while (!CheckValidMove(chosenMove));
            if (chosenMove == 'M') break;   // Return to the menu.
            
            // Move the player and refresh the screen.
            _player.MakeMove(chosenMove);
            _cavern.DrawToScreen();

            // Check to see if the player has found the flask and has won.
            bool flaskFound = _player.CheckIfSameCell(_flask.Position);
            if (flaskFound)
            { 
                DisplayWonGameMessage();
                break;  // Break as we have won
            } 
            
            bool hasBeenEaten = _monster.CheckIfSameCell(_player.Position);
            // This selection structure checks to see if the player has 
            // triggered one of the traps in the cavern.
            if (!_monster.IsAwake 
                && !flaskFound && !hasBeenEaten 
                && (_trap1.CheckForActivation(_player.Position) 
                    || _trap2.CheckForActivation(_player.Position)))
            {
                _monster.ToggleAwake();
                _cavern.DrawToScreen();
                DisplayTrapMessage();
            }
            
            // Monster move handling, moves 2 spaces for each player turn (OP??).
            if (_monster.IsAwake && !hasBeenEaten && !flaskFound)
            {
                Console.WriteLine("\nPress Enter key to continue");
                Console.ReadLine();
                for (int i = 0; i < 2; i++)
                {
                    // The original project used this code to prevent the monster
                    // from overwriting the flask on the grid by swapping their
                    // positions as two items could not have the same position
                    // on the grid. This is no longer an issue in the rewrite.
                    //
                    // var monsterOldPosition = _monster.Position;
                    // if (_monster.CheckIfSameCell(_flask.Position))
                    // {
                    //     _flask.Position = monsterOldPosition;
                    // }

                    _monster.MoveTowardsPlayer(_player.Position);
                    _cavern.DrawToScreen();
                    
                    hasBeenEaten = _monster.CheckIfSameCell(_player.Position);
                    if (hasBeenEaten) break;

                    Console.WriteLine("\nPress Enter key to continue");
                    Console.ReadLine();
                }
            }

            if (!hasBeenEaten) continue;    // Keep playing till game over or win.
            if (!_monster.IsAwake) _monster.ToggleAwake();
            _cavern.DrawToScreen();
            DisplayLostGameMessage();
            break;  // Break as we have lost
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

    /// <summary>
    /// Parses the player's input to determine the desired interaction they wish
    /// to make.
    /// </summary>
    /// <returns>The character representation of the interaction the player has chosen.</returns>
    private static char GetMove()
    {
        string? input = Console.ReadLine();
        if (input == null || input.Trim().Length == 0)
        {
            // The player has not entered a legitimate choice/value.
            return ' ';
        }

        char move = input.Trim().ToUpper().ToCharArray()[0]; // Determine the interaction's unique character code.
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
    
    private static bool CheckValidMove(char move)
    {
        // TODO: Some form of border check. Maybe a dynamic valid move list?
        // i.e. check player position, display moves appropriately.
        bool isValidMove = move is 'N' or 'S' or 'W' or 'E' or 'M';
        return isValidMove;
    }

    /// <summary>
    /// Instantiates the items and entities with their initial positions.
    /// </summary>
    /// <param name="isTraining">When true, predetermined values are used for the items'/entities' positions - rather than random ones.</param>
    private void SetUpGame(bool isTraining)
    {
        _cavern.Reset();

        // This registers all the items and entities on the grid, the order in
        // which they're added is also the rendering priority.
        // FIELD - First In Equals Last Drawn (and therefore appears on top).
        _monster = new Enemy(
            'M',
            isTraining 
                ? new CellReference { CellX = 4, CellY = 0 }
                : _cavern.GetRandEmptyCell()
        );
        _cavern.AddItem(_monster);
        
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
        
        _flask = new Flask(
            'F',
            isTraining 
                ? new CellReference { CellX = 4, CellY = 1 }
                : _cavern.GetRandEmptyCell()
        );
        _cavern.AddItem(_flask);
    }
}