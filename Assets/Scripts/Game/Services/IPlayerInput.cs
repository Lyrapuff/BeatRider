namespace Game.Services
{
    public interface IPlayerInput
    {
        bool ReadInputs { get; set; }
        float Direction { get; }
    }
}