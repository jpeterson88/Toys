using Assets.Scripts.Input;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    internal class AirborneStateHandler : StateHandlerBase
    {
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private ClimbDetection climbDetector;
        [SerializeField] private LandComponent landComponent;
        [SerializeField] private CircleCollider2D environmentInteractor;

        //TODO: This should likely be in a landed component and have a landed state handler

        private IInput input;

        private void Awake()
        {
            input = transform.root.GetComponent<IInput>();
            if (input == null)
                Debug.LogError("failed to find input for Locomotion handler");
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState())
            {
                if (groundedDetector.IsGrounded())
                {
                    SetState(PlayerStates.Idle);
                    landComponent.HandleLand();
                }
                else if (climbDetector.CanClimb() &&
                    Mathf.Abs(input.GetMoveVector().y) > .5f &&
                    IsInCurrentHandlerState())
                {
                    SetState(PlayerStates.Climb);
                }
            }
        }

        internal override void OnExit()
        {
            base.OnExit();
            environmentInteractor.enabled = false;
        }
    }
}