using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeObject : GridObject
    {
        protected Snake host;
        public SnakeObject(GridMap map, Snake host)
            : base(map)
        {
            this.host = host;
        }

        public override Snapshot Snapshot
        {
            get
            {
                Snapshot snapshot = base.Snapshot;
                snapshot.Name = host.Name;
                snapshot.Type = ObjectType.SnakeObject;
                return snapshot;
            }
        }
    }
}
