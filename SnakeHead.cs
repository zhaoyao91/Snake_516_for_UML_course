using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeHead : SnakeObject
    {
        public SnakeHead(GridMap map, Snake host)
            : base(map, host)
        {
            Direction = Direction.Center;
        }

        public Direction Direction;

        public override Snapshot Snapshot
        {
            get
            {
                Snapshot snapshot = base.Snapshot;
                snapshot.Type = ObjectType.SnakeHead;
                snapshot.Direction = this.Direction;
                return snapshot;
            }
        }
    }
}
