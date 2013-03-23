﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Food : GridObject, IDie
    {
        public Food(GridMap map)
            :base(map)
        {

        }


        public void Die()
        {
            Remove();
        }

        public override Snapshot Snapshot
        {
            get
            {
                Snapshot snapshot =  base.Snapshot;
                snapshot.Type = ObjectType.Food;
                return snapshot;
            }
        }
    }
}
