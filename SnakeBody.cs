using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeBody:SnakeObject
    {
        public SnakeBody(GridMap map, Snake host)
            : base(map, host)
        {

        }

        public override Snapshot Snapshot
        {
            get
            {
                Snapshot snapshot = base.Snapshot;
                snapshot.Type = ObjectType.SnakeBody;
                return snapshot;
            }
        }
    }
}
