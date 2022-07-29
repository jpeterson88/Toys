using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    internal class AirborneStateHandler : StateHandlerBase
    {
        [SerializeField] private GroundedDetector groundedDetector;

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (groundedDetector.IsGrounded() && IsInCurrentHandlerState())
                SetState(PlayerStates.Idle);
        }
    }
}