using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace go_to_four
{
   
    public static class ReplaceAtExtensions
    {
        public static string ReplaceAtChars(this string source, int index, char replacement)
        {
            var temp = source.ToCharArray();
            temp[index] = replacement;
            return new String(temp);
        }
    }

    class Player
    {
        public char Marker { get; set; }
        public int Position { get; set; }
        public Player(char playerMarker, int playerPosition)
        {
            Marker = playerMarker;
            Position = playerPosition;

        }
    }

    class FourInRow
    {
        /// <summary>
        /// This Class runs the four in a row clasic game via .
        /// </summary>
        private static string I00 = "-1234567-";
        private static string I01 = "|       |";
        private static string I02 = "|-------|";
        private static string I03 = "|       |";
        private static string I04 = "|       |";
        private static string I05 = "|       |";
        private static string I06 = "|       |";
        private static string I07 = "|       |";
        private static string I08 = "|       |";
        private static string I09 = "|-------|";

        private string[] frame = { I00, I01, I02, I03, I04, I05, I06, I07, I08, I09, I00 };
        private Player p1 = new Player('x', 4);
        private Player p2 = new Player('o', 4);
        private int turn = 1;

        /// <summary>
        /// Draws current frame 
        /// </summary>
        public void DrawFrame()
        {

            Console.Clear();
            foreach (string I in frame)
                Console.WriteLine(I);
            Console.WriteLine("player: {0}", turn);
            System.Threading.Thread.Sleep(200);

        }

        public void InsertChip()
        {
            int position;
            char marker;

            if (turn == 1)
            {
                position = p1.Position;
                marker = p1.Marker;
            }
            else
            {
                position = p2.Position;
                marker = p2.Marker;
            }

            for (int i = 3; i < 9; i++)
            {
                if (frame[i][position] == ' ')
                {
                    frame[i] = frame[i].ReplaceAtChars(position, marker);
                    if (frame[i - 1][position] == marker)
                    {
                        frame[i - 1] = frame[i - 1].ReplaceAtChars(position, ' ');
                    }
                    DrawFrame();

                }
                else
                {
                    break;
                }

            }



        }

        public void P1Turn()
        {
            turn = 1;
            frame[1] = I01.ReplaceAtChars(p1.Position, p1.Marker);
            DrawFrame();
            Console.Write("Please enter your position (1-7): ");
            p1.Position = Convert.ToInt32(Console.ReadLine());
            frame[1] = I01.ReplaceAtChars(p1.Position, p1.Marker);
            InsertChip();


        }

        public void P2Turn()
        {
            turn = 2;
            frame[1] = I01.ReplaceAtChars(p2.Position, p2.Marker);
            DrawFrame();
            Console.Write("Please enter your position (1-7): ");
            p2.Position = Convert.ToInt32(Console.ReadLine());
            frame[1] = I01.ReplaceAtChars(p2.Position, p2.Marker);
            InsertChip();


        }

        public string Check4Winner()
        {
            // This method check to see if the current play has won
            // if so it returns a string "winner"
            // else it returns "no winner"
            string gameStatus = "no winner";
            char marker;
            if (turn == 1)
            {
                marker = p1.Marker;
            }
            else
            {
                marker = p2.Marker;
            }

            // check for Vertical Wins
            for (int i = 0; i < 3; i++)
            {
                for (int col = 1; col < 8; col++)
                    if (frame[8 - i][col] == marker && frame[7 - i][col] == marker && frame[6 - i][col] == marker && frame[5 - i][col] == marker)
                    {
                        gameStatus = "winner";
                        return gameStatus;
                    }
            }
            // check for horizontal
            for (int i = 0; i < 4; i++)
            {
                for (int row = 3; row < 9; row++)
                    if (frame[row][7 - i] == marker && frame[row][6 - i] == marker && frame[row][5 - i] == marker && frame[row][4 - i] == marker)
                    {
                        gameStatus = "winner";
                        return gameStatus;
                    }
            }
            // check for diagonals that fit only 1 instance of 4 in a row
            if (frame[6][1] == marker && frame[5][2] == marker && frame[4][3] == marker && frame[3][4] == marker)
            {
                gameStatus = "winner";
                return gameStatus;
            }

            else if (frame[6][7] == marker && frame[5][6] == marker && frame[4][5] == marker && frame[3][4] == marker)
            {
                gameStatus = "winner";
                return gameStatus;
            }


            else if (frame[8][4] == marker && frame[7][5] == marker && frame[6][6] == marker && frame[5][7] == marker)
            {
                gameStatus = "winner";
                return gameStatus;
            }

            else if (frame[8][4] == marker && frame[7][3] == marker && frame[6][2] == marker && frame[5][1] == marker)
            {
                gameStatus = "winner";
                return gameStatus;
            }

            // check for diagonal wins that fit 2 instances of 4 in row
            for (int i = 0; i < 2; i++)
            {
                if (frame[7 - i][1 + i] == marker && frame[6 - i][2 + i] == marker && frame[5 - i][3 + i] == marker && frame[4 - i][4 + i] == marker)
                {
                    gameStatus = "winner";
                    return gameStatus;
                }

                else if (frame[8 - i][3 + i] == marker && frame[7 - i][4 + i] == marker && frame[6 - i][5 + i] == marker && frame[5 - i][6 + i] == marker)
                {
                    gameStatus = "winner";
                    return gameStatus;
                }
                else if (frame[3 + i][3 + i] == marker && frame[4 + i][4 + i] == marker && frame[5 + i][5 + i] == marker && frame[6 + i][6 + i] == marker)
                {
                    gameStatus = "winner";
                    return gameStatus;
                }
                else if (frame[4 + i][1 + i] == marker && frame[5 + i][2 + i] == marker && frame[6 + i][3 + i] == marker && frame[7 + i][4 + i] == marker)
                {
                    gameStatus = "winner";
                    return gameStatus;
                }
            }
            // check for diagonal wins that fit 3 instances of 4 in row
            for (int i = 0; i < 3; i++)
            {
                if (frame[8 - i][1 + i] == marker && frame[7 - i][2 + i] == marker && frame[6 - i][3 + i] == marker && frame[5 - i][4 + i] == marker)
                {
                    gameStatus = "winner";
                    return gameStatus;
                }

                else if (frame[8 - i][2 + i] == marker && frame[7 - i][3 + i] == marker && frame[6 - i][4 + i] == marker && frame[5 - i][5 + i] == marker)
                {
                    gameStatus = "winner";
                    return gameStatus;
                }
                else if (frame[3 + i][1 + i] == marker && frame[4 + i][2 + i] == marker && frame[5 + i][3 + i] == marker && frame[6 + i][4 + i] == marker)
                {
                    gameStatus = "winner";
                    return gameStatus;
                }
                else if (frame[3 + i][2 + i] == marker && frame[4 + i][3 + i] == marker && frame[5 + i][4 + i] == marker && frame[6 + i][5 + i] == marker)
                {
                    gameStatus = "winner";
                    return gameStatus;
                }
            }
            return gameStatus;

        }
        //Constructor to initialize game
        public FourInRow()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            FourInRow Game = new FourInRow();

            for (int i = 0; i < 21; i++)
            {
                Game.P1Turn();
                if (Game.Check4Winner() == "winner")
                {
                    Console.WriteLine("Winner!!!");
                    break;
                }
                Game.P2Turn();
                if (Game.Check4Winner() == "winner")
                {
                    Console.WriteLine("Winner!!!");
                    break;
                }
            }
            Console.Read();

        }

    }
   
}
