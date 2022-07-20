using Assets.Scripts.State.StateHandlers;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.State
{
    public class StateMachine<T> : MonoBehaviour where T : Enum
    {
        [SerializeField]
        [Tooltip("States that are allowed to repeat.")]
        private T[] allowedRepeatStates;

        [SerializeField] private bool setStateOnFirstFrameUpdate;
        [SerializeField] private T startingState;

        [SerializeField] private bool enableLogs;

        private int currentState;

        private bool stateSet;

        /// <summary>
        /// An event raised for when a state changes.
        /// <para>Current State</para>
        /// <para>New State</para>
        /// </summary>
        public Action<int, int> OnStateChanged = delegate { };

        private StateHandlerBase currentStateHandler;
        private StateHandlerBase[] stateHandlers;
        private int[] convertedStates;

        private void Awake()
        {
            stateHandlers = GetComponentsInChildren<StateHandlerBase>();

            if (stateHandlers == null || !stateHandlers.Any())
                Debug.LogError("Failed to get statehandlers.");

            convertedStates = (int[])Enum.GetValues(typeof(T));
        }

        private void FixedUpdate()
        {
            if (!stateSet && setStateOnFirstFrameUpdate)
            {
                stateSet = true;
                SetState(Convert.ToInt32(startingState));
            }

            if (currentStateHandler != null)
                currentStateHandler.OnFixedUpdate();
        }

        public int GetCurrentState()
        {
            return currentState;
        }

        public void SetState(int newState)
        {
            //If state is the same state and is not in repeatStates, return.
            if (newState == currentState && !convertedStates.Any(x => x == newState))
            {
                return;
            }

            StateHandlerBase newStateHandler = stateHandlers.FirstOrDefault(x => x.CanHandle(newState));

            if (newStateHandler != null)
            {
                if (enableLogs)
                    Debug.Log($"New State: {newState}. Current: {currentState}");

                OnStateChanged?.Invoke(currentState, newState);
                currentState = newState;

                //Should only be null on initialization
                if (currentStateHandler != null)
                    currentStateHandler.OnExit();

                currentStateHandler = newStateHandler;
                currentStateHandler.OnEnter(currentState);
            }
        }
    }
}