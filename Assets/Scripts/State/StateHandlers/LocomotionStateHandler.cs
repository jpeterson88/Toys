using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class LocomotionStateHandler : StateHandlerBase
    {
        [SerializeField] private MovementComponent movementComponent;
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private FacingDirection facingDirection;

        private IInput input;

        private void Awake()
        {
            input = transform.root.GetComponent<IInput>();
            if (input == null)
                Debug.LogError("failed to find input for Locomotion handler");

            input.JumpPressed += HandleJumpPressed;
        }

        private void HandleJumpPressed()
        {
            if (groundedDetector.IsGrounded())
                SetState(PlayerStates.Jump);
        }

        private void OnDisable() => input.JumpPressed -= HandleJumpPressed;

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            Vector2 moveVector = input.GetMoveVector();
            facingDirection.SetFacingDirection(moveVector);
            movementComponent.Move(moveVector);
        }
    }
}