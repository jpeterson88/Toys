using Assets.Scripts.ActionComponents.TopDown;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers.TopDown
{
    class TopDownChargeHandler : StateHandlerBase
    {
        [SerializeField] private TopDownChargeComponent chargeComponent;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            chargeComponent.InitiateCharge();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState() && !chargeComponent.IsCharging())
                SetState(PlayerStates.Idle);
            else
                chargeComponent.RotateDuringDelay();
        }

        internal override void OnExit()
        {
            base.OnExit();
            chargeComponent.Reset();
        }
    }
}