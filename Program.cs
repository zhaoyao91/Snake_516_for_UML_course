using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void drawMap(Snapshot[,] map, int width, int height)
        {
            for (int i = 0; i < width + 2; i++)
                print("-");
            println();
            for (int i = 0; i < height; i++)
            {
                print("|");
                for (int j = 0; j < width; j++)
                {
                    Snapshot snapshot = map[j, i];
                    if (snapshot == null)
                        print(" ");
                    else if (snapshot.Type == ObjectType.Food)
                        print("$");
                    else if (snapshot.Type == ObjectType.Wall)
                        print("#");
                    else if (snapshot.Type == ObjectType.SnakeBody)
                        print("O");
                    else if (snapshot.Type == ObjectType.SnakeHead)
                    {
                        switch (snapshot.Direction)
                        {
                            case Direction.Up:
                                print("^");
                                break;
                            case Direction.Down:
                                print("v");
                                break;
                            case Direction.Left:
                                print("<");
                                break;
                            case Direction.Right:
                                print(">");
                                break;
                        }
                    }
                }
                print("|");
                println();
            }
            for (int i = 0; i < width + 2; i++)
                print("-");
            println();
        }

        private static void print(string content)
        {
            Console.Write(content);
        }

        static void println()
        {
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Game game = new FirstGame();
            drawMap(game.Snapshots, game.Width, game.Height);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        game.MovePlayer(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        game.MovePlayer(Direction.Right);
                        break;
                    case ConsoleKey.UpArrow:
                        game.MovePlayer(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        game.MovePlayer(Direction.Down);
                        break;
                }
                game.Update();
                game.MoveCompute();
                game.Update();
                drawMap(game.Snapshots, game.Width, game.Height);
            }
        }
    }
}
