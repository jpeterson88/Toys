using Assets.Scripts.ActionComponents.TopDown;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers.TopDown
{
    class TopDownMoveHandler : StateHandlerBase
    {
        [SerializeField] private TopDownMovementComponent movement;
        [SerializeField] private TopDownHopComponent hopComponent;
        private IInput input;

        private void Awake()
        {
            input = transform.root.GetComponent<IInput>();
            if (input == null)
                Debug.LogError("failed to find input for Locomotion handler");

            //input.JumpPressed += HandleJumpPressed;
            input.Attack2Pressed += HandleAttack2Pressed;
        }

        private void HandleAttack2Pressed()
        {
            if (IsInCurrentHandlerState())
            {
                hopComponent.SetLaunchDir(input.GetMoveVector());
                SetState(PlayerStates.Attack2);
            }
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState())
            {
                Vector2 moveVector = input.GetMoveVector();
                movement.ApplyMovement(moveVector);
            }
        }
    }
}