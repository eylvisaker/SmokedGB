namespace SmokedGB
{
    public interface IMemoryController
    {
        byte this[int index] { get; set; }

        int Length { get; }

        void CopyTo(byte[] voluntaryData, int v1, int v2);
        void CopyMemoryToSaveRam();
        void MemWrite(int v1, byte v2);
    }
}