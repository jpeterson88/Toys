using Assets.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class IdleStateHandler : StateHandlerBase
    {
        [SerializeField] private TimerComponent timer;
        [SerializeField] private PlayerStates nextState;

        private bool hasStarted;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            timer.Begin();
            hasStarted = true;
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (IsInCurrentHandlerState() && !timer.IsActive() && hasStarted)
                SetState(nextState);
        }

        internal override void OnExit()
        {
            base.OnExit();
            hasStarted = false;
        }
    }
}