using System;
using UnityEngine;

namespace Assets.Scripts.Scriptables.Events
{
    public class ActionT1Scriptable<T> : ScriptableObject
    {
        #region Public Variables

        public event Action<T> Value = delegate { };

        public Action<T> PermanentValue;

        #endregion Public Variables

        public void AddAction(Action<T> callback)
        {
            Value += callback;
        }

        public void RemoveAction(Action<T> callback)
        {
            Value -= callback;
        }

        public void Clear()
        {
            Value = delegate { };
        }

        public void Invoke(T value)
        {
            Value?.Invoke(value);
        }
    }
}