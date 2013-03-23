using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake : IDie, IMove
    {
        GridMap map;
        SnakeHead head;
        LinkedList<SnakeBody> bodyList;
        public Position HeadPosition
        {
            get
            {
                return head.Position;
            }
        }
        public string Name
        {
            protected set;
            get;
        }
        public bool IsAlive
        {
            protected set;
            get;
        }
        public Snake(GridMap map, int size, string name)
        {
            this.map = map;
            head = new SnakeHead(map, this);
            bodyList = new LinkedList<SnakeBody>();
            for (int i = 1; i < size; i++)
                bodyList.AddLast(new SnakeBody(map, this));
            IsAlive = false;
            Name = name;
        }

        public Direction Direction
        {
            protected set
            {
                head.Direction = value;
            }

            get
            {
                return head.Direction;
            }
        }

        public void Put(Position pos, Direction direction)
        {
            //get position list
            LinkedList<Position> positions = findPositions(pos, direction);
            
            //check if there is enough space
            if (positions.Count < this.Length)
            {
                throw new Exception("can't put the snake: there is not enough room to put it");
            }

            //set the head direction
            // as every snapshot is a new instance, and the map takes snapshot when every grid object is put
            // set up all the properties before put it into map is important
            Direction = direction;

            //put head
            pos = positions.First.Value;
            head.Put(pos);
            positions.RemoveFirst();

            //put bodies
            foreach(SnakeBody body in bodyList)
            {
                pos = positions.First.Value;
                body.Put(pos);
                positions.RemoveFirst();
            }
            IsAlive = true;
        }

        public void Remove()
        {
            head.Remove();
            foreach (SnakeBody body in bodyList)
            {
                body.Remove();
            }
            IsAlive = false;
        }

        public bool Move()
        {
            return Move(Direction);
        }

        public bool Move(Direction direction)
        {
            if (IsAlive)
            {
                // if it's not the opposite direction, set the direction
                if (!(
                    (Direction == Direction.Up && direction == Direction.Down) ||
                    (Direction == Direction.Down && direction == Direction.Up) ||
                    (Direction == Direction.Left && direction == Direction.Right) ||
                    (Direction == Direction.Right && direction == Direction.Left)))
                {
                    Direction = direction;

                    if (nextPositionIsOutsideTheMap())
                    {
                        Die();
                    }
                    else if (nextPositionIsEmpty())
                    {
                        moveAhead();
                    }
                    else
                    {
                        GridObject obj = getNextObject();
                        if (obj is Food)
                        {
                            ((Food)obj).Die();
                            grow();
                        }
                        else // is wall, snake head or snake body
                        {
                            Die();
                        }
                    }

                    return true;
                }
            }
            
            return false;
        }

        void grow()
        {
            //move head
            Position headPos = putHeadAhead();

            //add a body
            SnakeBody newBody = new SnakeBody(map, this);
            newBody.Put(headPos);
            bodyList.AddFirst(newBody);
        }

        Position putHeadAhead()
        {
            Position pos = getNextPosition();
            Position headPos = head.Position;
            head.Remove();
            head.Put(pos);
            return headPos;
        }

        Position getNextPosition()
        {
            Position pos = new Position(head.Position);
            switch (head.Direction)
            {
                case Direction.Up:
                    --pos.Y;
                    break;
                case Direction.Down:
                    ++pos.Y;
                    break;
                case Direction.Left:
                    --pos.X;
                    break;
                case Direction.Right:
                    ++pos.X;
                    break;
            }
            return pos;
        }

        void moveAhead()
        {
            //put head ahead
            Position headPos = putHeadAhead();
            
            //move the last body to the pre head position
            SnakeBody lastBody = bodyList.Last.Value;
            bodyList.RemoveLast();
            bodyList.AddFirst(lastBody);
            lastBody.Remove();
            lastBody.Put(headPos);
        }

        bool nextPositionIsEmpty()
        {
            Position pos = getNextPosition();
            return map.IsEmpty(pos.X, pos.Y);
        }

        bool nextPositionIsOutsideTheMap()
        {
            Position pos = getNextPosition();
            return map.IsOutside(pos.X, pos.Y);
        }

        GridObject getNextObject()
        {
            Position pos = getNextPosition();
            return map.Get(pos.X, pos.Y);
        }

        LinkedList<Position> findPositions(Position pos, Direction direction)
        {
            int xStep = 0;
            int yStep = 0;
            switch (direction)
            {
                case Direction.Up:
                    yStep = 1;
                    break;
                case Direction.Down:
                    yStep = -1;
                    break;
                case Direction.Left:
                    xStep = 1;
                    break;
                case Direction.Right:
                    xStep = -1;
                    break;
            }

            LinkedList<Position> positions = new LinkedList<Position>();
            int snakeLength = this.Length;
            for (int i = 0; i < snakeLength; i++)
            {
                Position nextPos;
                nextPos = new Position(pos.X + xStep * i, pos.Y + yStep * i);

                if (map.IsEmpty(nextPos.X, nextPos.Y))
                {
                    positions.AddLast(nextPos);
                }
                else
                {
                    return positions; // not find enough positions
                }
            }
            return positions; // find enough positions
        }

        public int Length 
        {
            get
            {
                return 1 + bodyList.Count;
            }
        }

        public void Die()
        {
            Remove();
        }
    }
}
