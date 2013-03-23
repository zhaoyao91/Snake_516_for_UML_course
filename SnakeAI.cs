using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeAI
    {
        Snake snake;
        World world;
        public SnakeAI(Snake snake, World world)
        {
            this.snake = snake;
            this.world = world;
        }

        public void Move()
        {
            //not implemented
            //snake.Move(snake.Direction);

            // this AI make the snake find the position of the closest food
            // and move towards it
            // if there is not food in the map
            // just random move to a empty position

            //a map to record the positions found and their pre-position
            Position[,] posMap = new Position[world.Width, world.Height]; // use to record the positions found and their pre-position
            posMap[snake.HeadPosition.X, snake.HeadPosition.Y] = snake.HeadPosition;

            //a queue to record the positions need to be searched
            LinkedList<Position> posQueue = new LinkedList<Position>(); // use to record the positions need to be searched
            posQueue.AddFirst(snake.HeadPosition);

            //deep first to find the position of the closest food
            Position foodPos = bfSearchFood(posMap, posQueue);

            //determine if go towards the food or a random position
            if (foodPos != null)
            {
                Direction dir = findDirectionTowardsFood(posMap, foodPos);
                snake.Move(dir);
            }
            else
            {
                Direction dir = findDirectionTowardsRandomEmptyPosition();
                snake.Move(dir);
            }
        }

        private Direction findDirectionTowardsFood(Position[,] posMap, Position foodPos)
        {
            Position nextPos = foodPos;
            // search the next position towards the food
            while (posMap[nextPos.X, nextPos.Y] != snake.HeadPosition)
                nextPos = posMap[nextPos.X, nextPos.Y];
            //determine the direction
            return findDirection(snake.HeadPosition, nextPos);
        }

        private Direction findDirection(Position startPos, Position endPos)
        {
            int xDiff = endPos.X - startPos.X;
            int yDiff = endPos.Y - startPos.Y;

            if (xDiff == 1)
                return Direction.Right;
            else if (xDiff == -1)
                return Direction.Left;
            else if (yDiff == 1)
                return Direction.Down;
            else if (yDiff == -1)
                return Direction.Up;
            else
                return Direction.Center; // precisely speaking, this is a wrong direction
        }

        private Direction findDirectionTowardsRandomEmptyPosition()
        {
            Position nextPos = findNextRandomEmptyPosition();
            if (nextPos != null)
                return findDirection(snake.HeadPosition, nextPos);
            else
                return snake.Direction;
        }

        static Random random = new Random();
        private Position findNextRandomEmptyPosition()
        {
            List<Position> nextPoss = new List<Position>();
            Position headPos = snake.HeadPosition;
            addEmptyPosition(nextPoss, new Position(headPos.X + 1, headPos.Y));
            addEmptyPosition(nextPoss, new Position(headPos.X - 1, headPos.Y));
            addEmptyPosition(nextPoss, new Position(headPos.X, headPos.Y+1));
            addEmptyPosition(nextPoss, new Position(headPos.X, headPos.Y-1));
            if (nextPoss.Count == 0)
                return null;
            else
           {
             int choice = random.Next(nextPoss.Count - 1);
             return nextPoss[choice];
            }
        }

        private void addEmptyPosition(List<Position> nextPoss, Position position)
        {
            if (world.IsInside(position.X, position.Y) &&
                world.Snapshots[position.X, position.Y] == null)
                nextPoss.Add(position);
        }

        private Position bfSearchFood(Position[,] posMap, LinkedList<Position> posQueue)
        {
            while (posQueue.Count != 0)
            {
                Position curPos = posQueue.First.Value;
                posQueue.RemoveFirst();

                // if find a food, return
                if (world.Snapshots[curPos.X, curPos.Y] != null &&
                    world.Snapshots[curPos.X, curPos.Y].Type == ObjectType.Food)
                    return curPos;
                else
                {
                    // set the around positions and put the positon
                    // which is empty and has not been searched
                    // into the queue
                    searchAroundPositions(posMap, posQueue, curPos, new Position(curPos.X - 1, curPos.Y));
                    searchAroundPositions(posMap, posQueue, curPos, new Position(curPos.X + 1, curPos.Y));
                    searchAroundPositions(posMap, posQueue, curPos, new Position(curPos.X, curPos.Y - 1));
                    searchAroundPositions(posMap, posQueue, curPos, new Position(curPos.X, curPos.Y + 1));
                }
            }
            return null; // not find a food
        }

        private void searchAroundPositions(Position[,] posMap, LinkedList<Position> posQueue, Position curPos, Position position)
        {
            // if the position is inside the map then go on
            if (!world.IsOutside(position.X, position.Y))
            {
                // if the position is empty or is food, then go on
                if (world.Snapshots[position.X, position.Y] == null || world.Snapshots[position.X, position.Y].Type == ObjectType.Food)
                {
                    // if the position has not been searched, then set it and put it into the queue
                    if (posMap[position.X, position.Y] == null)
                    {
                        posMap[position.X, position.Y] = curPos; // the searched position's pre-position is the cur
                        posQueue.AddLast(position);
                    }
                }
            }
        }        
    }
}