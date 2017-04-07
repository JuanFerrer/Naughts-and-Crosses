using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaughtsAndCrosses
{
    public enum UserSelection { None, Start, Options, Exit }

    static class Menu
    {
        static private string[] menuOptions = { "Start", "Options", "Exit" };
        static private string cellVerticalLine = "│";
        static private string cellHorizontalLine = "─";
        static private string cellTJoint = "┼";

        static private string _defaultCell  = "  │   │  \n──┼───┼──\n  │   │  \n──┼───┼──\n  │   │  ";
        static string currentGame           = "  │   │  \n──┼───┼──\n  │   │  \n──┼───┼──\n  │   │  ";
        static private string _key          = "7 │ 8 │ 9\n──┼───┼──\n4 │ 5 │ 6\n──┼───┼──\n1 │ 2 │ 3"; /* Explanation:
            Character position in string
            7 = char 0
            8 = char 4
            9 = char 8
            4 = char 20
            5 = char 24
            6 = char 28
            1 = char 40
            2 = char 44
            3 = char 48
            */


        /// <summary>
        /// Show starting screen and prepare menu
        /// </summary>
        static public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Naughts and Crosses\nNumber of players: {0}\n", GameManager.IsSinglePlayer ? "1" : "2");
            
            for (int i = 0; i < menuOptions.Length; i++)
                Console.WriteLine("{0}. {1}", i + 1, menuOptions[i]);
        }

        /// <summary>
        /// Prompts user to select option and verifies it's a valid option
        /// </summary>
        /// <returns>The menu selection of the user</returns>
        static public UserSelection MakeUserSelect()
        {
            int selection = -1;
            bool isNumber = int.TryParse(Console.ReadLine(), out selection);
            while (!isNumber || selection < 0 || selection > menuOptions.Length)
            {
                Console.WriteLine("That is not a valid option. Please, try again");
                isNumber = int.TryParse(Console.ReadLine(), out selection);
            }
            return (UserSelection)selection;
        }

        /// <summary>
        /// Prompts user to play and verifies it's a valid play
        /// </summary>
        /// <returns></returns>
        static public Vector2 GetUserPlay()
        {
            int[] coords = new int[2];
            int userInput;
            for (int i = 0; i < coords.Length; ++i)
            {
                Console.Write(i == 0? "x: " : "y: ");
                bool inputIsNumber = int.TryParse(Console.ReadLine(), out userInput);
                bool inputIsValid = 0 <= userInput && userInput < GameManager.board.GetSize();
                while (!inputIsNumber || !inputIsValid)
                {
                    Console.WriteLine("Please, select a valid movement");
                    inputIsNumber = int.TryParse(Console.ReadLine(), out userInput);
                    inputIsValid = 0 <= userInput && userInput < GameManager.board.GetSize();
                }
                coords[i] = userInput;               
            }
            // Reversing coordinates to have horizontal x
            return new Vector2(coords[1], coords[0]);
        }

        /// <summary>
        /// Show options menu
        /// </summary>
        static public void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine("You are currently in {0} mode.\nDo you want to change to {1}? (y/n)", GameManager.IsSinglePlayer ? "single player" : "multiplayer", GameManager.IsSinglePlayer ? "multiplayer" : "singleplayer");
        }

        /// <summary>
        /// Show exit prompt
        /// </summary>
        static public void ShowExit()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit? (y/n)");
        }

        /// <summary>
        /// Show progress of current game on screen
        /// </summary>
        static public void ShowProgress()
        {
            //Show progress on screen
            Console.Clear();
            Console.WriteLine("Enter the position where you want to put your next token:\n\nIntructions:\nEnter 0-based x and then y\nFor example 0 0 will be top left corner");
            Console.WriteLine("\nYour game:\n" + GameManager.board + "\n");
        }

        static public void ShowResults()
        {
            Console.Clear();
            Console.WriteLine("Your game:\n" + GameManager.board.ToString() + "\n");
            if (GameManager.Winner != -1)
                Console.WriteLine("Player " + (GameManager.Winner + 1) + " wins");
            else
            {
                Console.WriteLine("It's a tie!");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Read user input as text
        /// </summary>
        /// <returns></returns>
        static public bool ReadYesNo()
        {
            string userInput = Console.ReadLine().ToLower();
            while (userInput != "y" && userInput != "n")
            {
                Console.WriteLine("Please, only enter 'y' or 'n\'");
                userInput = Console.ReadLine().ToLower();
            }
            return userInput == "y";
        }
    }
}
