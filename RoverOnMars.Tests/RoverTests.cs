using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;

namespace TechTest
{
    [TestClass]
    public class RoverTests
    {
        [TestMethod]
        public void RotateRoverLeftFromNorthToNorth()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.North);
            Rover.ExecuteCommand("L", mock.Object);
            Rover.ExecuteCommand("L", mock.Object);
            Rover.ExecuteCommand("L", mock.Object);
            Rover.ExecuteCommand("L", mock.Object);
            Assert.AreEqual<int>((int)mock.Object.RoverFacingPosition, (int)RoverFacing.North);
        }

        [TestMethod]
        public void RotateRoverRightFromNorthToNorth()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.North);
            Rover.ExecuteCommand("R", mock.Object);
            Rover.ExecuteCommand("R", mock.Object);
            Rover.ExecuteCommand("R", mock.Object);
            Rover.ExecuteCommand("R", mock.Object);
            Assert.AreEqual<int>((int)mock.Object.RoverFacingPosition, (int)RoverFacing.North);
        }

        [TestMethod]
        public void RotateRoverFromEast_InvalidRotationPoint()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.East);
            Rover.ExecuteCommand("L", mock.Object);
            Rover.ExecuteCommand("L", mock.Object);
            Rover.ExecuteCommand("L", mock.Object);
            Rover.ExecuteCommand("L", mock.Object);
            Assert.AreNotEqual<int>((int)mock.Object.RoverFacingPosition, (int)RoverFacing.North);
        }

        [TestMethod]
        public void RotateRoverFromWest_InvalidRotationPoint()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.West);
            Rover.ExecuteCommand("R", mock.Object);
            Rover.ExecuteCommand("R", mock.Object);
            Rover.ExecuteCommand("R", mock.Object);
            Rover.ExecuteCommand("R", mock.Object);
            Assert.AreNotEqual<int>((int)mock.Object.RoverFacingPosition, (int)RoverFacing.North);
        }


        [TestMethod]
        public void MoveRoverForward()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.North);
            mock.SetupProperty(m => m.RoverPositionX, 1);
            mock.SetupProperty(m => m.RoverPositionY, 1);
            Rover.ExecuteCommand("F", mock.Object);
            Assert.AreEqual<int>(mock.Object.RoverPositionX, 0);
            Assert.AreEqual<int>(mock.Object.RoverPositionY, 1);
        }

        [TestMethod]
        public void MoveRoverOutOfGtrid_West()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.West);
            mock.SetupProperty(m => m.RoverPositionX, 0);
            mock.SetupProperty(m => m.RoverPositionY, 0);
            Rover.ExecuteCommand("F", mock.Object);
            Assert.AreEqual<int>(mock.Object.RoverPositionX, 0);
            Assert.AreEqual<int>(mock.Object.RoverPositionY, 0);
        }

        [TestMethod]
        public void MoveRoverOutOfGtrid_North()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.North);
            mock.SetupProperty(m => m.RoverPositionX, 0);
            mock.SetupProperty(m => m.RoverPositionY, 0);
            Rover.ExecuteCommand("F", mock.Object);
            Assert.AreEqual<int>(mock.Object.RoverPositionX, 0);
            Assert.AreEqual<int>(mock.Object.RoverPositionY, 0);
        }

        [TestMethod]
        public void MoveRoverOutOfGtrid_East()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.East);
            mock.SetupProperty(m => m.RoverPositionX, 4);
            mock.SetupProperty(m => m.RoverPositionY, 4);
            Rover.ExecuteCommand("F", mock.Object);
            Assert.AreEqual<int>(mock.Object.RoverPositionX, 4);
            Assert.AreEqual<int>(mock.Object.RoverPositionY, 4);
        }

        [TestMethod]
        public void MoveRoverOutOfGtrid_South()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.South);
            mock.SetupProperty(m => m.RoverPositionX, 4);
            mock.SetupProperty(m => m.RoverPositionY, 4);
            Rover.ExecuteCommand("F", mock.Object);
            Assert.AreEqual<int>(mock.Object.RoverPositionX, 4);
            Assert.AreEqual<int>(mock.Object.RoverPositionY, 4);
        }


        [TestMethod]
        public void SendInvalidCommand()
        {
            Assert.IsFalse(Rover.ValidateCommand("K"));

        }

        [TestMethod]
        public void SendValidCommand()
        {
            Assert.IsTrue(Rover.ValidateCommand("L"));
        }

        [TestMethod]
        public void SendValidMoveEastWest()
        {
            Assert.IsTrue(Rover.IsValidMove(2, true));
        }

        [TestMethod]
        public void SendInvalidMoveEastWest()
        {
            Assert.IsFalse(Rover.IsValidMove(5, true));
        }

        [TestMethod]
        public void SendValidMoveNorthSouth()
        {
            Assert.IsTrue(Rover.IsValidMove(2, false));
        }

        [TestMethod]
        public void SendInvalidMoveNorthSouth()
        {
            Assert.IsFalse(Rover.IsValidMove(-1, false));
        }

        [TestMethod]
        public void PrintRoverPosition()
        {
            var mock = new Moq.Mock<IRoverPosition>();
            mock.SetupProperty(m => m.RoverFacingPosition, RoverFacing.North);
            mock.SetupProperty(m => m.RoverPositionX, 1);
            mock.SetupProperty(m => m.RoverPositionY, 1);
            string expectedText = $"Rover is now at {mock.Object.RoverPositionX}, {mock.Object.RoverPositionY} - facing {mock.Object.RoverFacingPosition}";

            try
            {
                using (var writer = new StreamWriter("ConsoleOut.txt", false, UTF8Encoding.UTF8))
                {
                    var currOut = Console.Out;
                    Console.SetOut(writer);
                    Rover.PrintRoverPosition(mock.Object.RoverPositionX, mock.Object.RoverPositionY, mock.Object.RoverFacingPosition);
                }

                using (var reader = new StreamReader("ConsoleOut.txt", UTF8Encoding.UTF8))
                {
                    var textFromFile = reader.ReadToEnd().Trim();
                    Assert.IsTrue(expectedText.Equals(textFromFile));
                }
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
            
        }
    }
}
