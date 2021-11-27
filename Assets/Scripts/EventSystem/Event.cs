using System;

namespace EventSystem
{
    public class Event<T>
    {
        private event Action<T> Action = delegate { };

        public void Invoke(T param)
        {
            Action.Invoke(param);
        }

        public void AddListener(Action<T> listener)
        {
            Action += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            Action -= listener;
        }
    }
}