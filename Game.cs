using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //supply basic control, need to inherit and set up the game
    abstract class Game
    {
        protected World world;
        protected Snake player;
        protected Snake computer;
        protected SnakeAI AI;

        public virtual int Width
        {
            get
            {
                return world.Width;
            }
        }
        public virtual int Height
        {
            get
            {
                return world.Height;
            }
        }

        public virtual void MovePlayer(Direction direction)
        {
            player.Move(direction);
        }

        public virtual void MovePlayer()
        {
            player.Move();
        }

        public virtual void MoveCompute()
        {
            AI.Move();
        }

        public abstract void Update();

        public virtual bool IsPlayerAlive
        {
            get
            {
                return player.IsAlive;
            }
        }

        public virtual bool IsComputerAlive
        {
            get
            {
                return computer.IsAlive;
            }
        }

        public virtual int PlayerLength
        {
            get
            {
                return player.Length;
            }
        }

        public virtual int ComputerLength
        {
            get
            {
                return computer.Length;
            }
        }

        public Snapshot[,] Snapshots
        {
            get
            {
                return world.Snapshots;
            }
        }
    }
}
