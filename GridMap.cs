using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //this class can add, remove, get GridObject
    class GridMap
    {
        public int Width
        {
            protected set;
            get;
        }
        public int Height
        {
            protected set;
            get;
        }
        GridObject[,] map;
        public GridMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            map = new GridObject[width, height];
            snapshots = new Snapshot[width, height];
        }

        public bool IsEmpty(int x, int y)
        {
            if (map[x, y] == null)
                return true;
            else
                return false;
        }

        public void Add(GridObject obj, int x, int y)
        {
            if (IsEmpty(x, y))
            {
                map[x, y] = obj;
                snapshots[x, y] = obj.Snapshot;
                ++Count;
            }
            else
                throw new Exception("can't add object: the position is not empty"); 
        }

        public void Remove(GridObject obj)
        {
            if (IsEmpty(obj.Position.X, obj.Position.Y))
                throw new Exception("can't remove object: the position is empty");
            else
            {
                map[obj.Position.X, obj.Position.Y] = null;
                snapshots[obj.Position.X, obj.Position.Y] = null;
                --Count;
            }
        }

        public bool IsOutside(int x, int y)
        {
            if (x < 0 || y < 0 ||
                x > Width - 1 || y > Height - 1)
                return true;
            else
                return false;
        }

        public bool IsInside(int x, int y)
        {
            return !IsOutside(x, y);
        }

        public GridObject Get(int x, int y)
        {
            return map[x, y];
        }

        Snapshot[,] snapshots;
        public Snapshot[,] Snapshots
        {
            get
            {
                return snapshots;
            }
        }

        static Random random = new Random();
        protected Position getRandomEmptyPosition()
        {
            int countEmpty = Width * Height - Count;
            int posCount = random.Next(countEmpty-1);
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (posCount == 0 && map[i, j] == null)
                        return new Position(i, j);
                    else if (map[i, j] == null)
                        posCount--;
                }
            }
            return null;
        }

        public int Count
        {
            protected set;
            get;
        }
    }
}
