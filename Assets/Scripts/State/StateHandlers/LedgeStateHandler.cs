using Assets.Scripts.ActionComponents;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class LedgeStateHandler : StateHandlerBase
    {
        [SerializeField] private LedgeComponent ledgeComponent;
        [SerializeField] private float ledgePullupDelay;
        IInput input;
        private float currentDurationOnEnter;

        private void Awake()
        {
            input = transform.root.GetComponent<IInput>();
            if (input == null)
                Debug.LogError("failed to find input for Locomotion handler");
        }

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            ledgeComponent.Initiate();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            currentDurationOnEnter += Time.deltaTime;

            if (IsInCurrentHandlerState() && currentDurationOnEnter >= ledgePullupDelay)
            {
                if (!ledgeComponent.IsPullingUp())
                {
                    if (input.GetMoveVector().y > .5f)
                        ledgeComponent.BeginPullup();
                    else if (input.GetMoveVector().y < 0)
                        SetState(PlayerStates.Airborne);
                }
                else if (ledgeComponent.IsPullingUp() && ledgeComponent.IsFinished())
                    SetState(PlayerStates.Idle);
            }
        }

        internal override void OnExit()
        {
            base.OnExit();
            currentDurationOnEnter = 0f;
            ledgeComponent.Reset();
        }
    }
}