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
            // Check horizontal, vertical, and diagonal wins
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols - 3; c++)
                    if (board[r, c] == symbol && board[r, c + 1] == symbol && board[r, c + 2] == symbol && board[r, c + 3] == symbol)
                        return true;

            for (int r = 0; r < Rows - 3; r++)
                for (int c = 0; c < Cols; c++)
                    if (board[r, c] == symbol && board[r + 1, c] == symbol && board[r + 2, c] == symbol && board[r + 3, c] == symbol)
                        return true;

            for (int r = 0; r < Rows - 3; r++)
                for (int c = 0; c < Cols - 3; c++)
                    if (board[r, c] == symbol && board[r + 1, c + 1] == symbol && board[r + 2, c + 2] == symbol && board[r + 3, c + 3] == symbol)
                        return true;

            for (int r = 3; r < Rows; r++)
                for (int c = 0; c < Cols - 3; c++)
                    if (board[r, c] == symbol && board[r - 1, c + 1] == symbol && board[r - 2, c + 2] == symbol && board[r - 3, c + 3] == symbol)
                        return true;

            return false; // No winner found
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------

    }

    // gpassarelli: Player class, abstract base for all types of players ----------------------------------------------------------------------------------------------------
    // The Player class is an abstract base class that defines the shared properties and behaviors of any player in the game — whether it's a human or a computer.
    // It serves as a blueprint for other specific player types.
    abstract class Player
    {
        public string Name { get; set; } // Player's name
        public char Symbol { get; set; } // Player's symbol (e.g., 'X' or 'O')

        // Constructor to initialize the player's name and symbol
        protected Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        // Abstract method for getting a player's move
        public abstract int GetMove();
    }

    // gpassarelli: HumanPlayer class, represents a human player
    class HumanPlayer : Player
    {
        public HumanPlayer(string name, char symbol) : base(name, symbol) { }

        // Override the GetMove method to get input from the user
        public override int GetMove()
        {
            Console.WriteLine($"{Name} ({Symbol}), enter column (1-7):");
            int move;
            while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 7)
            {
                Console.WriteLine("Invalid input. Enter a number between 1 and 7:");
            }
            return move - 1; // Return the column index (0-6)
        }
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
            bool gameRunning = true;
            board.DisplayBoard(); // Display the initial empty board - Draw the board on the Screen

            while (gameRunning)
            {
                Player currentPlayer = players[currentPlayerIndex]; // Get the current player from GameController
                int move;
                bool validMove;

                do
                {
                    move = currentPlayer.GetMove(); // Get the move from the current player
                    validMove = board.DropPiece(move, currentPlayer.Symbol); // Drop the piece on the board
                    if (!validMove)
                        Console.WriteLine("Column full! Try again."); // If the column is full, ask to try again
                } while (!validMove);

                board.DisplayBoard(); // Display the updated board
                if (board.CheckWin(currentPlayer.Symbol)) // Check if the current player won
                {
                    Console.WriteLine($"{currentPlayer.Name} ({currentPlayer.Symbol}) wins!"); // Display the winner
                    gameRunning = false; // End the game
                }
                else if (IsBoardFull()) // Check if the board is full (draw)
                {
                    Console.WriteLine("It's a draw!"); // Display a draw message
                    gameRunning = false; // End the game
                }
                else
                {
                    currentPlayerIndex = (currentPlayerIndex + 1) % 2; // Switch to the other player
                }
            }
        }

        // gpassarelli: Method to check if the board is full (no more moves)
        private bool IsBoardFull()
        {
            for (int i = 0; i < 7; i++)
            {
                if (board.DropPiece(i, ' ')) return false; // If there is an empty spot, the board is not full
            }
            return true; // If no empty spots, the board is full
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
