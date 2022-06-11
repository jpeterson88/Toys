using Assets.Scripts.State;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    class Player : MonoBehaviour
    {
        private JumpComponent jumpComponent;
        private MovementComponent movementComponent;
        private GroundedDetector groundedDetector;
        private FacingDirection facingDirection;
        private StateEnum currentState;

        private bool previouslyGrounded;

        public static Action OnLand = delegate { };

        private void Awake()
        {
            jumpComponent = GetComponentInChildren<JumpComponent>();
            movementComponent = GetComponentInChildren<MovementComponent>();
            groundedDetector = GetComponentInChildren<GroundedDetector>();
            facingDirection = GetComponentInChildren<FacingDirection>();
        }

        private void Start()
        {
            currentState = groundedDetector.IsGrounded() ? StateEnum.Airborne : StateEnum.Idle;
        }

        private void Update()
        {
            if (groundedDetector.IsGrounded() && !previouslyGrounded)
            {
                OnLand?.Invoke();
                currentState = StateEnum.Idle;
            }
            else if (!groundedDetector.IsGrounded() && previouslyGrounded)
            {
                currentState = StateEnum.Airborne;
            }

            previouslyGrounded = groundedDetector.IsGrounded();
        }

        private void FixedUpdate()
        {
            if (groundedDetector.IsGrounded() && currentState != StateEnum.Jump)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    currentState = StateEnum.Jump;
                    jumpComponent.InitiateJump();
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    currentState = StateEnum.Walk;
                    facingDirection.SetFacingDirection(Vector2.right);
                    movementComponent.Move(Vector2.right);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    currentState = StateEnum.Walk;
                    facingDirection.SetFacingDirection(Vector2.left);
                    movementComponent.Move(Vector2.left);
                }
                else
                {
                    currentState = StateEnum.Idle;
                }
            }
        }

        public void SetState(StateEnum state) => currentState = state;

        public StateEnum GetState() => currentState;
    }
}