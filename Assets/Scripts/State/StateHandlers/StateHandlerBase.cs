using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    internal class StateHandlerBase : MonoBehaviour
    {
        [SerializeField] protected int[] canHandleStates;
        [SerializeField] private int idleState;

        private StateMachine<PlayerStates> stateMachine;

        //TODO: this should be Awake and children statehandlers start
        private void Start()
        {
            stateMachine = GetComponentInParent<StateMachine<PlayerStates>>();

            if (stateMachine == null)
                throw new NullReferenceException(nameof(stateMachine));
        }

        internal bool CanHandle(int desiredState)
        {
            return canHandleStates.Any(x => x == desiredState);
        }

        protected bool IsInCurrentHandlerState()
        {
            return canHandleStates.Any(x => x == GetCurrentState());
        }

        protected void SetState(int desiredState)
        {
            stateMachine.SetState(desiredState);
        }

        internal int GetCurrentState()
        {
            if (stateMachine == null)
                return idleState;

            return stateMachine.GetCurrentState();
        }

        internal virtual void OnEnter(int state)
        {
        }

        internal virtual void OnExit()
        {
        }

        internal virtual void OnFixedUpdate()
        {
        }
    }
}