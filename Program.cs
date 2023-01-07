using PrelimCode;

bool isPlaying = true;
while (isPlaying)
{
    DisplayMenu();

    int choice;
    do
    {
        choice = GetMainMenuChoice();
    } while (choice == -1);

    switch (choice)
    {
        case 1:
        {
            var unused = new Game(false);
            break;
        }

        case 2:
        {
            var unused = new Game(true);
            break;
        }

        case 9:
        {
            isPlaying = false;
            break;
        }

        default:
        {
            Console.WriteLine($"Invalid Choice: {choice}");
            break;
        }
    }
}

static void DisplayMenu()
{
    Console.WriteLine("MAIN MENU");
    Console.WriteLine();
    Console.WriteLine("1. Start new game");
    Console.WriteLine("2. Play training game");
    Console.WriteLine("9. Quit");
    Console.WriteLine();
    Console.Write("Please enter your choice: ");
}

int GetMainMenuChoice()
{
    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        choice = -1;
    }

    Console.WriteLine();
    return choice;
}