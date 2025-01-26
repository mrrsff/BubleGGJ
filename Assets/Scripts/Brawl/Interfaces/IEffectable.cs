namespace GGJ2025.Interfaces
{
    public interface IEffectable<in T> 
    {
        public int AddEffect(T effect);
        public void RemoveEffect(int ID);
    }
}