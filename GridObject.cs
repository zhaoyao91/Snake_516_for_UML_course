using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class GridObject
    {
        GridMap map;
        public Position Position
        {
            protected set;
            get;
        }

        public GridObject(GridMap map)
        {
            this.map = map;
            IsInMap = false;
        }

        public void Put(Position pos)
        {
            if (IsInMap)
                throw new Exception("can't put the object: the object is already in the map");
            else
            {
                this.Position = pos;
                this.IsInMap = true;
                map.Add(this, Position.X, Position.Y);
            }            
        }

        public void Remove()
        {
            if (IsInMap)
            {
                map.Remove(this);
                this.IsInMap = false;
                this.Position = new Position(0, 0);
            }
        }

        public bool IsInMap
        {
            protected set;
            get;
        }

        public virtual Snapshot Snapshot
        {
            get
            {
                Snapshot snapshot = new Snapshot();
                snapshot.Type = ObjectType.GridObject;
                snapshot.Position = Position;
                return snapshot;
            }
        }
    }
}
