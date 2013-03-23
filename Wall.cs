using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Wall : GridObject
    {
        public Wall(GridMap map)
            : base(map)
        {

        }

        public override Snapshot Snapshot
        {
            get
            {
                Snapshot snapshot =  base.Snapshot;
                snapshot.Type = ObjectType.Wall;
                return snapshot;
            }
        }
    }
}
