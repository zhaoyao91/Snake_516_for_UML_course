using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class FirstGame : FoodGame
    {
        public FirstGame()
        {
            world = new World(20, 20);

            player = world.CreatSnake(2, 0, 3, Direction.Right, "player");
            computer = world.CreatSnake(17, 19, 3, Direction.Left, "computer");
            AI = new SnakeAI(computer, world);

            world.CreateWall(9, 6);
            world.CreateWall(9, 7);
            world.CreateWall(9, 8);
            world.CreateWall(10, 11);
            world.CreateWall(10, 12);
            world.CreateWall(10, 13);

            world.CreateFood(4, 4);
            world.CreateFood(4, 15);
            world.CreateFood(15, 4);
            world.CreateFood(15, 15);
        }
    }
}
