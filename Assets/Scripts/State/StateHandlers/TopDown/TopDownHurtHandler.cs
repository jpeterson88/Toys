using Assets.Scripts.ActionComponents.TopDown;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers.TopDown
{
    class TopDownHurtHandler : StateHandlerBase
    {
        [SerializeField] private TopDownHurtComponent hurtComponent;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            hurtComponent.TakeDamage();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState() && !hurtComponent.IsHurt())
                SetState(PlayerStates.Idle);
        }
    }
}