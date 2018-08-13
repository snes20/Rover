using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public class RoverPosition : IRoverPosition
    {
        public int RoverPositionX { get; set; }
        public int RoverPositionY { get; set; }
        public RoverFacing RoverFacingPosition { get; set; }

        static RoverPosition instance;

        private RoverPosition()
        {
            RoverPositionX = 0;
            RoverPositionY = 0;
            RoverFacingPosition = RoverFacing.North;
        }

        public static RoverPosition GetInstance()
        {
            if (instance == null)
                instance = new RoverPosition();

            return instance;
        }
    }
}
