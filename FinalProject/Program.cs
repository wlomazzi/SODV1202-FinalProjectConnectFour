/**
* @name.......: Final Project - Connect Four
* @Group name.: Connect4Fun
* @Course Code: SODV1202 - Introduction to Object-Oriented Programming
* @class..: Software Development Diploma program.
* @authors -----------------------------------------------------------------
* @name...: Wesley Lomazzi e Souza
* @id.....: 461407
* @email..: w.lomazziesouza407@mybvc.ca
* @name...: Gabriel Puertas Passarelli
* @id.....: 460625
* @email..: g.puertaspassarel625@mybvc.ca
* -------------------------------------------------------------------------- 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectConnectFour
{


    // GameBoard class, represents the Connect Four game board
    // wlomazzi: Class to draw the Board -------------------------------------------------------------------------------------------------------------------------------------
    class GameBoard
    {
        // Size of the array
        private char[,] board; // 2D array to represent the board
        private const int Rows = 6; // Number of rows in the board
        private const int Cols = 7; // Number of columns in the board

        // Constructor to initialize the board with empty spaces
        // Runs through all cells and fills them with spaces.
        public GameBoard()
        {
            board = new char[Rows, Cols];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    board[i, j] = ' '; // Initialize all cells with empty space
        }

        // wlomazzi: Method to drop a piece into a column
        // Starts from the bottom row up, and places the piece in the first empty spot.
        public bool DropPiece(int column, char symbol)
        {
            if (column < 0 || column >= Cols || board[0, column] != ' ')
                return false; // If the column is out of bounds or full, return false

            for (int row = Rows - 1; row >= 0; row--) // Start from the bottom row
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = symbol; // Drop the piece
                    return true; // Successful move
                }
            }
            return false; // If the column is full, return false
        }

        // wlomazzi: Method to display the current board in the console -> Prints the board to the console in a formatted way.
        public void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("+---------------------------+");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write("| " + board[i, j] + " "); // Print each cell
                Console.WriteLine("|");
            }
            Console.WriteLine("|---|---|---|---|---|---|---|");
            Console.WriteLine("| 1 | 2 | 3 | 4 | 5 | 6 | 7 |");
            Console.WriteLine("+---------------------------+");
        }


        // gpassarelli: Method to check if a player has won --------------------------------------------------------------------------------------------
        public bool CheckWin(char symbol)
        {
        
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
        
    }
 



    // gpassarelli: Player class, abstract base for all types of players ----------------------------------------------------------------------------------------------------
    // The Player class is an abstract base class that defines the shared properties and behaviors of any player in the game — whether it's a human or a computer.
    // It serves as a blueprint for other specific player types.
    abstract class Player
    {

    }

    // gpassarelli: HumanPlayer class, represents a human player
    class HumanPlayer : Player
    {

    }
    
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------





    // GameController class, controls the flow of the game
    // wlomazzi: The GameController class is the central coordinator of the game. It manages the game loop, switches turns between players,
    //           checks for victory or draw conditions, and interacts with the GameBoard and Player classes.
    class GameController
    {
        private GameBoard board; // The game board
        private Player[] players; // Array of players (two players)
        private int currentPlayerIndex; // Index of the current player (0 or 1)

        // Constructor to initialize the game - Create a new GameBoard: Instantiates two players (HumanPlayer in this case).
        /*
            Gets the current player.
            Asks for the player’s move using GetMove().
            Attempts to drop the piece with board.DropPiece().
            Displays the updated board.
            Checks for a win using board.CheckWin().
            Checks for a draw using IsBoardFull().
            If the game continues, it switches to the other player. 
        */
        public GameController()
        {
            board = new GameBoard(); // Create a new game board
            players = new Player[2] {
                new HumanPlayer("Player 1", 'X'), // Create Player 1
                new HumanPlayer("Player 2", 'O')  // Create Player 2
            };
            currentPlayerIndex = 0; // Player 1 starts
        }

        // gpassarellii: Method to start the game and control its flow - This is the main game loop.
        public void StartGame()
        {
        }

        // gpassarelli: Method to check if the board is full (no more moves)
        private bool IsBoardFull()
        {
        }
    }





    // wlomazzi: Program class, entry point of the application
    class Program
    {
        static void Main()
        {
            GameController game = new GameController(); // Create a new game controller
            game.StartGame(); // Start the game
        }
    }


}
