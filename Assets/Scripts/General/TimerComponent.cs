using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.General
{
    class TimerComponent : MonoBehaviour
    {
        [SerializeField] private float duration;

        private float currentDuration;
        private bool isActive;
        private Action nextCallback;

        public void Begin(Action callback = null)
        {
            nextCallback = callback;
            isActive = true;
        }

        public bool IsActive() => isActive;

        private void Update()
        {
            if (isActive)
            {
                currentDuration += Time.deltaTime;

                if (currentDuration >= duration)
                {
                    nextCallback?.Invoke();
                    End();
                }
            }
        }

        public void End()
        {
            currentDuration = 0f;
            isActive = false;
            nextCallback = null;
        }
    }
}