using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Scriptables.Lists
{
    public class ListVariable<T> : ScriptableObject
    {
        #region Public Variables

        public List<T> Value;
        public bool Locked;
        public bool InvokeOnValidate;
        public Action<T> OnItemAdded, OnItemRemoved = delegate { };

        public Action OnListChanged = delegate { };

        #endregion Public Variables

        // editor call when GUI is changed
        private void OnValidate()
        {
            if (InvokeOnValidate)
                InvokeChanged();
        }

        public virtual void InvokeChanged() => OnListChanged?.Invoke();

        /// <summary>
        /// Set value, trigger will invoke the OnValueChaned event
        /// </summary>
        public virtual void Add(T value, bool trigger = true)
        {
            if (Locked)
                return;

            Value.Add(value);

            if (trigger)
                InvokeItemAdded(value);
        }

        /// <summary>
        /// Sets value and triggers changed event
        /// </summary>
        public virtual void Remove(T value, bool trigger = true)
        {
            if (Locked)
                return;

            Value.Remove(value);

            if (trigger)
                InvokeItemAdded(value);
        }

        /// <summary>
        /// Sets value and triggers changed event
        /// </summary>
        public virtual void Remove(int index, bool trigger = true)
        {
            if (Locked)
                return;

            T value = Value[index];
            Value.RemoveAt(index);

            if (trigger)
                InvokeItemAdded(value);
        }

        public virtual void Lock(bool value)
        {
            Locked = value;
        }

        public virtual List<T> Get()
        {
            return Value;
        }

        public virtual void AddItemAddedChangeAction(Action<T> action)
        {
            OnItemAdded += action;
        }

        public virtual void RemoveItemAddedChangeAction(Action<T> action)
        {
            OnItemAdded -= action;
        }

        public virtual void AddItemRemovedChangeAction(Action<T> action)
        {
            OnItemRemoved += action;
        }

        public virtual void RemoveItemRemovedChangeAction(Action<T> action)
        {
            OnItemRemoved -= action;
        }

        public virtual void ClearChangeAction()
        {
            OnItemAdded = delegate { };
            OnItemRemoved = delegate { };
        }

        public virtual void InvokeItemAdded(T item)
        {
            OnItemAdded?.Invoke(item);
        }

        public virtual void InvokeItemRemoved(T item)
        {
            OnItemAdded?.Invoke(item);
        }

        public void ClearList(bool trigger = true)
        {
            Value?.Clear();

            if (trigger)
                OnListChanged?.Invoke();
        }

        public int Count() => Value.Count;
    }
}