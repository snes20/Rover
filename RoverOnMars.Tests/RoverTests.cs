using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void MoveRoverOutOfGtrid()
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
        public void SendInvalidCommand()
        {
            Assert.IsFalse(Rover.ValidateCommand("K"));

        }

        [TestMethod]
        public void SendValidCommand()
        {
            Assert.IsTrue(Rover.ValidateCommand("L"));
        }
    }
}
