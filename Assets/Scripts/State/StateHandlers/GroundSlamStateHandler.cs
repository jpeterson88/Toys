using Assets.Scripts.ActionComponents;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class GroundSlamStateHandler : StateHandlerBase
    {
        [SerializeField] private GroundSlamComponent groundSlam;
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private LandComponent landComponent;

        private bool detectedGround;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            groundSlam.Slam();
        }

        private void Update()
        {
            if (IsInCurrentHandlerState() && !detectedGround)
                detectedGround = groundedDetector.IsGrounded();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState() && detectedGround)
            {
                //Setting to 100 because ground slam should always be hard
                landComponent.HandleLand(100);
                SetState(PlayerStates.Idle);
            }
        }

        internal override void OnExit()
        {
            base.OnExit();
            groundSlam.Reset();
            detectedGround = false;
        }
    }
}