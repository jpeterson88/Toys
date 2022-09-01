using Assets.Scripts.ActionComponents.TopDown;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class GroundSlamStateHandler : StateHandlerBase
    {
        [SerializeField] private GroundSlamComponent groundSlam;
        [SerializeField] private GroundedDetector groundedDetector;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            groundSlam.Slam();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState() && groundedDetector.IsGrounded())
                SetState(PlayerStates.Idle);
        }
    }
}