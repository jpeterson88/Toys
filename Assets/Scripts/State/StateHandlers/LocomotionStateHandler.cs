using Assets.Scripts.ActionComponents;
using Assets.Scripts.Input;
using Assets.Scripts.Utility;
using System;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class LocomotionStateHandler : StateHandlerBase
    {
        [SerializeField] private MovementComponent movementComponent;
        [SerializeField] private MovementComponentv2 movementComponentv2;
        [SerializeField] private SlopeCheck slopeCheck;
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private ClimbDetection climbDetector;
        [SerializeField] private FacingDirection facingDirection;

        private IInput input;
        private bool sprintPressed;

        private void Awake()
        {
            input = transform.root.GetComponent<IInput>();
            if (input == null)
                Debug.LogError("failed to find input for Locomotion handler");

            input.JumpPressed += HandleJumpPressed;
            input.Attack2Pressed += HandleAttack2Pressed;
            input.SprintPressed += HandleSprintPressed;
        }

        private void HandleSprintPressed() => sprintPressed = true;

        private void HandleAttack2Pressed()
        {
            if (groundedDetector.IsGrounded() && IsInCurrentHandlerState())
                SetState(PlayerStates.Attack2);
        }

        private void HandleJumpPressed()
        {
            if (groundedDetector.IsGrounded() && IsInCurrentHandlerState())
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
            if (!groundedDetector.IsGrounded() && IsInCurrentHandlerState())
            {
                SetState(PlayerStates.Airborne);
            }
            else if (climbDetector.CanClimb() && Mathf.Abs(input.GetMoveVector().y) > .5f && IsInCurrentHandlerState())
            {
                SetState(PlayerStates.Climb);
            }
            else
            {
                Vector2 moveVector = input.GetMoveVector();

                slopeCheck.Detect(moveVector);

                if (moveVector != Vector2.zero)
                {
                    SetState(PlayerStates.Walk);
                    facingDirection.SetFacingDirection(moveVector);
                    movementComponentv2.ApplyMovement(moveVector, sprintPressed);
                    //movementComponent.Move(moveVector);
                }
                else
                {
                    SetState(PlayerStates.Idle);
                    sprintPressed = false;
                }
                //otherwise we're idle
            }
        }

        internal override void OnExit()
        {
            base.OnExit();
            sprintPressed = false;
        }
    }
}