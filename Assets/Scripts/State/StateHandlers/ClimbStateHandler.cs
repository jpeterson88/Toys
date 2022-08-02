﻿using Assets.Scripts.ActionComponents;
using Assets.Scripts.Input;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers
{
    class ClimbStateHandler : StateHandlerBase
    {
        [SerializeField] private ClimbComponent climbComponent;
        [SerializeField] private ClimbDetection climbDetection;

        private IInput input;

        private void Awake()
        {
            input = transform.root.GetComponent<IInput>();
            if (input == null)
                Debug.LogError("failed to find input for Locomotion handler");
        }

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            climbComponent.Initiate();
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (!climbDetection.CanClimb() && IsInCurrentHandlerState())
                SetState(PlayerStates.Idle);
            else
                climbComponent.Climb(input.GetMoveVector());
        }

        internal override void OnExit()
        {
            base.OnExit();
            climbComponent.Reset();
        }
    }
}