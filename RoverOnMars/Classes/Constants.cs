using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public static class Constants
    {
        public static class Movements
        {
            public const String LEFT = "L";
            public const String RIGHT = "R";
            public const String FORWARD = "F";
            public const String EXIT_ROVER = "E";

            public const int MIN_GRID_VALUE = 0;
            public const int MAX_GRID_VALUE = 4;
        }

        public const string WELCOME = "Welcome to the Rover on Mars application";
        public const string INVALID_COMMAND = "Invalid Command!!!!, Please Enter a valid command (R,L OR F) or E to quit\r\n";
        public const string COMMAND_PROMPT = "Please enter a command for the Rover(R, L OR F) or E to quit";
        public const string EXIT_ROVER_APP = "Closing connection to the Rover on Mars. Good Bye";
    }

    public enum RoverFacing
    {
        North,
        East,
        South,
        West
    }
}
