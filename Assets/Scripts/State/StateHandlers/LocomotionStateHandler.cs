using Assets.Scripts.Input;
using System.Linq;
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
            input.Attack2Pressed += HandleAttack2Pressed;
        }

        private void HandleAttack2Pressed()
        {
            if (groundedDetector.IsGrounded())
                SetState(PlayerStates.Attack2);
        }

        private void HandleJumpPressed()
        {
            if (groundedDetector.IsGrounded())
                SetState(PlayerStates.Jump);
        }

        private void OnDisable()
        {
            input.JumpPressed -= HandleJumpPressed;
            input.Attack2Pressed -= HandleAttack2Pressed;
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            //We're in grounded state but not touching ground
            if (!groundedDetector.IsGrounded() && canHandleStates.Contains(GetCurrentState()))
            {
                SetState(PlayerStates.Airborne);
            }
            else
            {
                Vector2 moveVector = input.GetMoveVector();

                if (moveVector != Vector2.zero)
                {
                    SetState(PlayerStates.Walk);
                    facingDirection.SetFacingDirection(moveVector);
                    movementComponent.Move(moveVector);
                }
                else
                {
                    SetState(PlayerStates.Idle);
                }
                //otherwise we're idle
            }
        }
    }
}