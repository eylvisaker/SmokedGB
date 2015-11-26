namespace SmokedGB
{
    public interface IGameboy
    {
        bool IsGbc { get; }

        void CheckJoysticks();
        void UpdateTimerFrequency();
    }
}