using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class World : GridMap
    {
        public World(int width, int height)
            :base(width, height)
        {

        }

        public Snake CreatSnake(int x, int y, int length, Direction direction, string name)
        {
            Snake snake = new Snake(this, length, name);
            snake.Put(new Position(x, y), direction);
            return snake;
        }

        public Food CreateFood(int x, int y)
        {
            Food food = new Food(this);
            food.Put(new Position(x, y));
            return food;
        }

        public Food CreateFood()
        {
            
            Position pos = getRandomEmptyPosition();
            if (pos != null)
            {
                return CreateFood(pos.X, pos.Y);
            }
            else
                return null;
        }

        public Wall CreateWall(int x, int y)
        {
            Wall wall= new Wall(this);
            wall.Put(new Position(x, y));
            return wall;
        }
    }
}
