using System;

namespace EventSystem
{
    public class Event<T>
    {
        private event Action<T> _action = delegate { };

        public void Invoke(T param)
        {
            _action.Invoke(param);
        }

        public void AddListener(Action<T> listener)
        {
            _action += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            _action -= listener;
        }
    }
}