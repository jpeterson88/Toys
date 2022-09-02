using Assets.Scripts.ActionComponents;
using Assets.Scripts.Input;
using Assets.Scripts.Utility;
using System;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    internal class AirborneStateHandler : StateHandlerBase
    {
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private ClimbDetection climbDetector;
        [SerializeField] private LandComponent landComponent;
        [SerializeField] private CircleCollider2D environmentInteractor;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private BoxCaster ledgeDetector;
        [SerializeField] private AirborneComponent airborneComponent;
        [SerializeField] private GroundSlamComponent slamComponent;

        //TODO: This should likely be in a landed component and have a landed state handler
        private IInput input;

        private float currentFallVelocity;

        private void Awake()
        {
            input = transform.root.GetComponent<IInput>();
            if (input == null)
                Debug.LogError("failed to find input for Locomotion handler");

            input.Attack2Pressed += HandleGroundSlam;
        }

        private void HandleGroundSlam()
        {
            if (IsInCurrentHandlerState() && slamComponent.CanSlam())
                SetState(PlayerStates.GroundSlam);
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState())
            {
                if (groundedDetector.IsGrounded())
                {
                    landComponent.HandleLand(currentFallVelocity);
                    SetState(PlayerStates.Idle);
                }
                else if (climbDetector.CanClimb() &&
                    Mathf.Abs(input.GetMoveVector().y) > .5f)
                {
                    SetState(PlayerStates.Climb);
                }
                else if (ledgeDetector.IsHit() && input.GetMoveVector().y > .5f)
                {
                    SetState(PlayerStates.Ledge);
                }
                else if (Mathf.Abs(rb2d.velocity.y) > 0)
                {
                    currentFallVelocity = rb2d.velocity.y;
                    airborneComponent.Move(input.GetMoveVector());
                }
            }
        }

        internal override void OnExit()
        {
            base.OnExit();
            currentFallVelocity = 0f;
            environmentInteractor.enabled = false;
        }
    }
}