using Assets.Scripts.ActionComponents;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class LungeStateHandler : StateHandlerBase
    {
        [SerializeField] private LungeComponent lungeComponent;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            lungeComponent.InitiateLunge();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (lungeComponent.IsFinished())
            {
                lungeComponent.Reset();
                SetState(PlayerStates.Idle);
            }
        }
    }
}