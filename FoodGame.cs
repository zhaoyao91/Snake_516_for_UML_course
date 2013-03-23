using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class FoodGame:Game
    {
        public override void Update()
        {
            if (FoodCount <= 0)
                world.CreateFood();
        }

        public virtual int FoodCount
        {
            get
            {
                int count = 0;
                Snapshot[,] snapshots = world.Snapshots;
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (snapshots[i, j] != null && snapshots[i, j].Type == ObjectType.Food)
                            count++;
                    }
                }
                return count;
            }
        }
    }
}
