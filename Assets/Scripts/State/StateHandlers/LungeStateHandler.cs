using Assets.Scripts.ActionComponents;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class LungeStateHandler : StateHandlerBase
    {
        [SerializeField] private LungeComponent lungeComponent;
        [SerializeField] private GroundedDetector groundedDetector;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            lungeComponent.InitiateLunge();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            //Check if lunge finished
            if (lungeComponent.HasInitiated() && lungeComponent.IsFinished() && IsInCurrentHandlerState())
            {
                lungeComponent.Reset();
                SetState(PlayerStates.Idle);
            }
            //Check if we're airborne
            else if (!groundedDetector.IsGrounded() && IsInCurrentHandlerState())
            {
                SetState(PlayerStates.Airborne);
            }
        }

        internal override void OnExit()
        {
            base.OnExit();
            lungeComponent.Reset();
        }
    }
}