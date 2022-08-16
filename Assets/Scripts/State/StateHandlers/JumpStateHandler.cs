using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class JumpStateHandler : StateHandlerBase
    {
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private JumpComponent jumpComponent;
        [SerializeField] private SlopeCheck slopeCheck;
        private float launchFrames;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            jumpComponent.InitiateJump();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (jumpComponent.HasLaunched())
                launchFrames++;

            if (!groundedDetector.IsGrounded())
                SetState(PlayerStates.Airborne);
            //If we jump on slope and we don't take off
            else if (jumpComponent.HasLaunched() && launchFrames > 15 && slopeCheck.IsOnSlope())
                SetState(PlayerStates.Idle);
        }
    }
}