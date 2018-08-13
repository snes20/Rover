using System;

namespace TechTest
{
    public class Rover
    {
        public static void Main()
        {
            Console.WriteLine(Constants.WELCOME);
            var rover = RoverPosition.GetInstance();
            PrintRoverPosition(rover.RoverPositionX, rover.RoverPositionY, rover.RoverFacingPosition);

            while (true)
            {
                Console.WriteLine(Constants.COMMAND_PROMPT);
                try
                {
                    var command = Console.ReadLine().Trim();
                    var isValid = ValidateCommand(command);

                    if (!isValid)
                    {
                        Console.WriteLine(Constants.INVALID_COMMAND);
                    }
                    else
                    {
                        ExecuteCommand(command, rover);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was an error and the application will terminate. {ex.Message}");
                    System.Environment.Exit(1);
                }               
            }
        }

        public static bool ValidateCommand(string command)
        {
            if (String.IsNullOrEmpty(command))
                return false;

            if (!command.Equals(Constants.Movements.LEFT, StringComparison.InvariantCultureIgnoreCase) &&
                !command.Equals(Constants.Movements.RIGHT, StringComparison.InvariantCultureIgnoreCase) &&
                !command.Equals(Constants.Movements.FORWARD, StringComparison.InvariantCultureIgnoreCase) &&
                !command.Equals(Constants.Movements.EXIT_ROVER, StringComparison.InvariantCultureIgnoreCase))
                return false;
            else
                return true;
        }

        public static void ExecuteCommand(string command, IRoverPosition position)
        {

            switch (command.ToUpper())
            {
                case Constants.Movements.LEFT:
                    position.RoverFacingPosition = position.RoverFacingPosition.Equals(RoverFacing.North) ? RoverFacing.West : (RoverFacing)((int)position.RoverFacingPosition - 1);
                    break;
                case Constants.Movements.RIGHT:
                    position.RoverFacingPosition = position.RoverFacingPosition.Equals(RoverFacing.West) ? RoverFacing.North : (RoverFacing)((int)position.RoverFacingPosition + 1);
                    break;
                case Constants.Movements.FORWARD:
                    switch (position.RoverFacingPosition)
                    {
                        case RoverFacing.North:
                            if (IsValidMove(position.RoverPositionX, false))
                            {
                                position.RoverPositionX--;
                            }
                            break;
                        case RoverFacing.East:
                            if (IsValidMove(position.RoverPositionY, true))
                            {
                                position.RoverPositionY++;
                            }
                            break;
                        case RoverFacing.South:
                            if (IsValidMove(position.RoverPositionX, true))
                            {
                                position.RoverPositionX++;
                            }
                            break;
                        case RoverFacing.West:
                            if (IsValidMove(position.RoverPositionY, false))
                            {
                                position.RoverPositionY--;
                            }
                            break;
                    }
                    break;
                case Constants.Movements.EXIT_ROVER:
                    Console.WriteLine(Constants.EXIT_ROVER_APP);
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(Constants.INVALID_COMMAND);
                    break;
            }

            PrintRoverPosition(position.RoverPositionX, position.RoverPositionY, position.RoverFacingPosition);
        }

        public static void PrintRoverPosition(int roverPositionX, int roverPositionY, RoverFacing roverFacing)
        {
            Console.WriteLine($"Rover is now at {roverPositionX}, {roverPositionY} - facing {roverFacing}\r\n");
        }

        private static bool IsValidMove(int currentValue, bool isAdding)
        {
            if (isAdding)
            {
                if (currentValue < Constants.Movements.MAX_GRID_VALUE)
                    return true;
                else
                    return false;
            }
            else
            {
                if (currentValue == Constants.Movements.MIN_GRID_VALUE)
                    return false;
                else
                    return true;
            }
        }
    }
}


