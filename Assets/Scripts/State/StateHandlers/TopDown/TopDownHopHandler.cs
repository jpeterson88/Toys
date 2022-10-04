using Assets.Scripts.ActionComponents.TopDown;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers.TopDown
{
    class TopDownHopHandler : StateHandlerBase
    {
        [SerializeField] private TopDownHopComponent hopComponent;
        [SerializeField] private int stateFrame;
        private int launchFrames;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            launchFrames = 0;
            hopComponent.InitiateJump();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (hopComponent.HasLaunched() && launchFrames < stateFrame)
                launchFrames++;
            else if (hopComponent.HasLaunched() && launchFrames >= stateFrame && IsInCurrentHandlerState())
                SetState(PlayerStates.Idle);
        }
    }
}