using System;
using UnityEngine;

namespace Assets.Scripts.Scriptables.Events
{
    class ActionScriptable : ScriptableObject
    {
        #region Public Variables

        public event Action Value = delegate { };

        public Action PermanentValue;

        #endregion Public Variables

        public void AddAction(Action callback)
        {
            Value += callback;
        }

        public void RemoveAction(Action callback)
        {
            Value -= callback;
        }

        public void Clear()
        {
            Value = delegate { };
        }

        public void Invoke()
        {
            Value?.Invoke();
        }
    }
}