using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    internal class AirborneStateHandler : StateHandlerBase
    {
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private ClimbDetection climbDetector;

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState())
            {
                if (groundedDetector.IsGrounded())
                    SetState(PlayerStates.Idle);
                else if (climbDetector.CanClimb())
                    SetState(PlayerStates.Climb);
            }
        }
    }
}