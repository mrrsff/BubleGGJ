using System.Collections.Generic;

namespace GGJ2025.Interfaces
{
    public interface IComponentEffector<T1, in T2> where T1 : IEffectable<T2>
    {  
        public List<int> effectIds { get; set; }
        public int ApplyEffect(T2 effect);
        public void RemoveEffect(int Id);
    }
}