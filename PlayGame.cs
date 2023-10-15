using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Battleship
{

    internal class PlayGame
    {
        private readonly int boardWidth = 10;
        private readonly int boardHeight = 10;
        private char[,] Board { get; set; }
        private List<Ship> Ships { get; set; }
        private List<Destroyer> Destroyers { get; set; }
        private int ShipLeft { get; set; }
        private int DestroyerLeft { get; set; }

        /// <summary>
        /// Constructor for Assign and initialize Variables and Methods
        /// </summary>
        public PlayGame()
        {
            Initialize();
            PayGame();
        }

        /// <summary>
        /// Initialize the variables and load the data to console
        /// </summary>
        void Initialize()
        {
            Board = new char[boardWidth, boardHeight];
            Ships = new List<Ship>();
            Destroyers = new List<Destroyer>();
            PopulateBoard();
            PlaceVessels();
            ShipLeft = Ships.Count;
            DestroyerLeft = Destroyers.Count;
            GameHelp();
            PrintBoard();
        }

        /// <summary>
        /// Writing help content into the console
        /// </summary>
        void GameHelp()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to simple Battleship game!");
            Console.WriteLine("This is C# understanding console game!");
            Console.WriteLine("1 Ship placed from D3 to G3 - 5 squares ");
            Console.WriteLine("2 Destroyers placed from H4 to I6 and B1 to E1 - 2X4 squares ");
            Console.WriteLine("Help:-");
            Console.WriteLine("x -- Stop and quit the game");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();
        }

        /// <summary>
        /// Load the data into Board
        /// </summary>
        void PopulateBoard()
        {
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    Board[j, i] = 'O';
                }
            }
        }

        /// <summary>
        /// Print he loaded Board into Console
        /// </summary>
        void PrintBoard()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  A B C D E F G H I J");

            for (int i = 0; i < boardWidth; i++)
            {
                Console.Write(i);
                for (int j = 0; j < boardHeight; j++)
                {
                    Console.Write(" " + Board[j, i]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Place the vessels into the console ground
        /// </summary>
        void PlaceVessels()
        {
            //make 5 big ships (size 5), and 2 big destroyers(4*4)
            //and put it by design (manually by coder) on the board
            Ships.Clear();
            //Ships.Add(new Ship("Big", 5));
            Ships.Add(new Ship("Ship", 5, Alignment.Horizontal));
            //Destroyers.Add(new Destroyers("Big", 5));
            Destroyers.Add(new Destroyer("Destroyer1", 4, Alignment.Vertical));
            Destroyers.Add(new Destroyer("Destroyer2", 4, Alignment.Horizontal));

            //place ships
            Ships[0].Position[0] = new int[] { 3, 3 }; //D3
            Ships[0].Position[1] = new int[] { 4, 3 }; //E3
            Ships[0].Position[2] = new int[] { 5, 3 }; //F3
            Ships[0].Position[3] = new int[] { 6, 3 }; //E3
            Ships[0].Position[4] = new int[] { 7, 3 }; //G3

            //place destroyers
            Destroyers[0].Position[0] = new int[] { 7, 4 }; //H4
            Destroyers[0].Position[1] = new int[] { 7, 5 }; //H5
            Destroyers[0].Position[2] = new int[] { 7, 6 }; //H6
            Destroyers[0].Position[3] = new int[] { 7, 7 }; //I6

            Destroyers[1].Position[0] = new int[] { 1, 1 }; //B1
            Destroyers[1].Position[1] = new int[] { 2, 1 }; //C1
            Destroyers[1].Position[2] = new int[] { 3, 1 }; //D1
            Destroyers[1].Position[3] = new int[] { 4, 1 }; //E1
        }

        /// <summary>
        /// This method to start/quit the game.
        /// </summary>
        void PayGame()
        {
            string consoleCommand;
            bool stop = false;
            while (!stop)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Input command:");
                consoleCommand = Console.ReadLine();
                if (consoleCommand != null)
                {
                    if (consoleCommand.ToLower() == "h")
                    {
                        GameHelp();
                    }
                    else if (consoleCommand.ToLower() == "x")
                    {
                        stop = true;
                        Console.WriteLine("Exiting game!");
                    }
                    else if (consoleCommand.Length > 1 && CheckAttackInput(consoleCommand))
                    {
                        PlayAttack(consoleCommand);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red; 
                        Console.WriteLine("ERROR - Invalid command");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR - Invalid command");
                }
            }
            Console.WriteLine("Nice Play! Good bye!");
        }

        /// <summary>
        /// RegEx validation for Input key. This to be validate 2 chars and it should ne Alphanumeric
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        bool CheckAttackInput(string tile)
        {
            Match result = Regex.Match(tile, @"\b[A-J]{1}\d{1}\b");

            return result.Success;
        }

        /// <summary>
        /// Play the game
        /// </summary>
        /// <param name="val"></param>
        void PlayAttack(string val)
        {
            Console.Clear();
            GameHelp();
            int x = char.ToUpper(val[0]) - 65;
            int y = int.Parse(val[1].ToString());
            bool shipHit = false;
            bool destroyerpHit = false;

            if (Board[x, y] == 'O')
            {
                if (CheckShipHit(x, y, out int sindex))
                {
                    shipHit = true;
                    DestroyShip(sindex);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Attack SUCCESS one ship has been destroyed!!\n");
                }
                if (CheckDestroyerHit(x, y, out int dindex))
                {
                    destroyerpHit = true;
                    RemoveDestroyer(dindex);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Attack SUCCESS one Destroyer has been destroyed!!\n");
                }
                if (!shipHit && !destroyerpHit)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Attack success but No ship/destroyer Hit!!\n");
                }
                PrintBoard();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Failed, position attacked already. Choose another location!");
            }
        }

        /// <summary>
        /// To check the array wheather the ship is hit or not.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="index"> Out param for identify the result to destory the same</param>
        /// <returns></returns>

        bool CheckShipHit(int x, int y, out int index)
        {
            index = 0;
            foreach (Ship ship in Ships)
            {
                foreach (int[] pos in ship.Position)
                {
                    if (pos[0] == x && pos[1] == y)
                    {
                        return true;
                    }
                }
                index++;
            }
            return false;
        }

        /// <summary>
        /// Remove the ship from ship collection
        /// </summary>
        /// <param name="index"></param>
        void DestroyShip(int index)
        {
            //mark ship's tile with X and remove it from list
            Ship ship = Ships[index];
            foreach (int[] pos in ship.Position)
            {
                Board[pos[0], pos[1]] = 'X';
            }
            Ships.RemoveAt(index);
            ShipLeft = Ships.Count;

        }

        /// <summary>
        /// To check the array wheather the destroyer is hit or not.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="index">Out param for identify the result to destory the same</param>
        /// <returns></returns>
        bool CheckDestroyerHit(int x, int y, out int index)
        {
            index = 0;
            foreach (Destroyer destroyer in Destroyers)
            {
                foreach (int[] pos in destroyer.Position)
                {
                    if (pos[0] == x && pos[1] == y)
                    {
                        return true;
                    }
                }
                index++;
            }
            return false;
        }

        /// <summary>
        /// Remove the destroyer from Destroyers collection
        /// </summary>
        /// <param name="index"></param>
        void RemoveDestroyer(int index)
        {
            //mark destroyer's tile with X and remove it from list
            Destroyer destroyer = Destroyers[index];
            foreach (int[] pos in destroyer.Position)
            {
                Board[pos[0], pos[1]] = 'X';
            }
            Destroyers.RemoveAt(index);
            DestroyerLeft = Destroyers.Count;
        }
    }
}