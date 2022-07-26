using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class JumpStateHandler : StateHandlerBase
    {
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private JumpComponent jumpComponent;

        internal override void OnEnter(int state)
        {
            Debug.Log("Enter Jump state");
            base.OnEnter(state);
            jumpComponent.InitiateJump();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (!groundedDetector.IsGrounded())
                SetState(PlayerStates.Airborne);
        }
    }
}