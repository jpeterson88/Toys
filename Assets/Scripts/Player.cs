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
        private PlayerStates currentState;

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
            currentState = groundedDetector.IsGrounded() ? PlayerStates.Airborne : PlayerStates.Idle;
        }

        private void Update()
        {
            if (groundedDetector.IsGrounded() && !previouslyGrounded)
            {
                OnLand?.Invoke();
                currentState = PlayerStates.Idle;
            }
            else if (!groundedDetector.IsGrounded() && previouslyGrounded)
            {
                currentState = PlayerStates.Airborne;
            }

            previouslyGrounded = groundedDetector.IsGrounded();
        }

        private void FixedUpdate()
        {
            if (groundedDetector.IsGrounded() && currentState != PlayerStates.Jump)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    currentState = PlayerStates.Jump;
                    jumpComponent.InitiateJump();
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    currentState = PlayerStates.Walk;
                    facingDirection.SetFacingDirection(Vector2.right);
                    movementComponent.Move(Vector2.right);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    currentState = PlayerStates.Walk;
                    facingDirection.SetFacingDirection(Vector2.left);
                    movementComponent.Move(Vector2.left);
                }
                else
                {
                    currentState = PlayerStates.Idle;
                }
            }
        }

        public void SetState(PlayerStates state) => currentState = state;

        public PlayerStates GetState() => currentState;
    }
}