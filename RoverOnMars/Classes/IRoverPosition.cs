namespace TechTest
{
    public interface IRoverPosition
    {
        int RoverPositionX { get; set; }
        int RoverPositionY { get; set; }
        RoverFacing RoverFacingPosition { get; set; }
    }
}