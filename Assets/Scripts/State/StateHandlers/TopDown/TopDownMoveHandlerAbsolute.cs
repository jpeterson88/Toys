﻿using Assets.Scripts.ActionComponents.TopDown;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers.TopDown
{
    class TopDownMoveHandlerAbsolute : StateHandlerBase
    {
        [SerializeField] private TopDownMovementComponentAbsolute movement;
        [SerializeField] private TopDownChargeComponent chargeComponent;

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
            if (IsInCurrentHandlerState() && chargeComponent.CanCharge())
                SetState(PlayerStates.Attack2);
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

        internal override void OnExit()
        {
            base.OnExit();
            movement.Exit();
        }
    }
}