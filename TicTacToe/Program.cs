using System;
using System.Threading;

class Program
{
    static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static int currentPlayer = 1;
    static bool isGameOver = false;
    static int player1Score = 0;
    static int player2Score = 0;
    static int moveTimeout = 10; // Timeout in seconds for each move

    static void Main()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine($"Player 1 (X) Score: {player1Score}, Player 2 (O) Score: {player2Score}\n");
            DisplayBoard();

            if (currentPlayer == 1)
            {
                int choice;
                bool validInput;
                bool timeExpired = false;
                Thread moveTimer = new Thread(() =>
                {
                    Thread.Sleep(moveTimeout * 1000);
                    timeExpired = true;
                });

                do
                {
                    Console.Write("Player 1, choose your move (1-9): ");
                    validInput = int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 9 && board[choice - 1] != 'X' && board[choice - 1] != 'O';

                    if (!validInput && !timeExpired)
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                } while (!validInput && !timeExpired);

                if (timeExpired)
                {
                    Console.WriteLine("Player 1 (X) ran out of time. Player 2 (O) wins!");
                    player2Score++;
                    isGameOver = true;
                }
                else
                {
                    MakeMove(choice, 'X');

                    if (CheckWin('X'))
                    {
                        Console.Clear();
                        DisplayBoard();
                        Console.WriteLine("Player 1 (X) wins!");
                        player1Score++;
                        isGameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        Console.Clear();
                        DisplayBoard();
                        Console.WriteLine("It's a draw!");
                        isGameOver = true;
                    }
                    else
                    {
                        currentPlayer = -1;
                    }
                }
            }
            else
            {
                // Computer's turn (AI) with a timeout
                int computerChoice = -1;
                bool timeExpired = false;
                Thread moveTimer = new Thread(() =>
                {
                    Thread.Sleep(moveTimeout * 1000);
                    timeExpired = true;
                });

                moveTimer.Start();
                computerChoice = ComputerMove();
                moveTimer.Join(); // Wait for the timer thread to finish

                if (timeExpired)
                {
                    Console.WriteLine("Player 2 (O) ran out of time. Player 1 (X) wins!");
                    player1Score++;
                    isGameOver = true;
                }
                else
                {
                    MakeMove(computerChoice, 'O');

                    if (CheckWin('O'))
                    {
                        Console.Clear();
                        DisplayBoard();
                        Console.WriteLine("Player 2 (O) wins!");
                        player2Score++;
                        isGameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        Console.Clear();
                        DisplayBoard();
                        Console.WriteLine("It's a draw!");
                        isGameOver = true;
                    }
                    else
                    {
                        currentPlayer = 1;
                    }
                }
            }

            if (isGameOver)
            {
                Console.Write("Play again? (yes/no): ");
                string playAgain = Console.ReadLine().ToLower();
                if (playAgain == "no")
                {
                    break; // Exit the program
                }
                else if (playAgain == "yes")
                {
                    ResetGame();
                }
            }

        } while (true); // Continue playing indefinitely
    }

    static void DisplayBoard()
    {
        Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
    }

    static void MakeMove(int choice, char playerSymbol)
    {
        board[choice - 1] = playerSymbol;
    }

    static bool CheckWin(char playerSymbol)
    {
        return
            (board[0] == playerSymbol && board[1] == playerSymbol && board[2] == playerSymbol) ||
            (board[3] == playerSymbol && board[4] == playerSymbol && board[5] == playerSymbol) ||
            (board[6] == playerSymbol && board[7] == playerSymbol && board[8] == playerSymbol) ||
            (board[0] == playerSymbol && board[3] == playerSymbol && board[6] == playerSymbol) ||
            (board[1] == playerSymbol && board[4] == playerSymbol && board[7] == playerSymbol) ||
            (board[2] == playerSymbol && board[5] == playerSymbol && board[8] == playerSymbol) ||
            (board[0] == playerSymbol && board[4] == playerSymbol && board[8] == playerSymbol) ||
            (board[2] == playerSymbol && board[4] == playerSymbol && board[6] == playerSymbol);
    }

    static bool IsBoardFull()
    {
        foreach (char position in board)
        {
            if (position != 'X' && position != 'O')
                return false;
        }
        return true;
    }

    static void ResetGame()
    {
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = (i + 1).ToString()[0];
        }
        currentPlayer = 1;
        isGameOver = false;
    }

    static int ComputerMove()
    {
        // AI logic here (as described in the previous response)
        // ...
    }
}
