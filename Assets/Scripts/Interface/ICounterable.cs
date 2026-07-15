namespace SSunSoft.RPGUdemy
{
    public interface ICounterable
    {
        public bool CanBeCountered { get; }
        public void HandleCounter();
    }
}