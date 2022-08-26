using Assets.Scripts.ActionComponents;
using Assets.Scripts.ActionComponents.TopDown;
using Assets.Scripts.Input;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.State.StateHandlers.TopDown
{
    class TdMoveTowardsHandler : StateHandlerBase
    {
        [SerializeField] private TopDownMovementComponentAbsolute moveComponent;

        [Tooltip("Will not pause if set to 0")]
        [SerializeField] private float idleAfterDuration, idleForDuration;

        [SerializeField] private RaycastCheck playerDetector;
        [SerializeField] private TargetComponent targetComponent;

        private float currentMoveDuration, currentIdleDuration;
        private bool isIdling;

        internal override void OnEnter(int state)
        {
            base.OnEnter(state);
            currentMoveDuration = 0f;
        }

        internal override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (IsInCurrentHandlerState())
            {
                currentMoveDuration += Time.deltaTime;

                playerDetector.Check();
                if (playerDetector.IsHit())
                {
                    SetState(PlayerStates.Attack2);
                }
                else if (idleAfterDuration > 0 && currentMoveDuration >= idleAfterDuration)
                {
                    currentMoveDuration = 0f;
                    isIdling = true;
                }
                else if (isIdling)
                {
                    currentIdleDuration += Time.deltaTime;
                    if (currentIdleDuration >= idleForDuration)
                    {
                        currentIdleDuration = 0f;
                        isIdling = false;
                    }
                }
                else
                {
                    Vector2 direction = targetComponent.GetTarget().position - transform.root.position;
                    moveComponent.ApplyMovement(direction.normalized);
                }
            }
        }

        internal override void OnExit()
        {
            base.OnExit();
            currentMoveDuration = 0f;
            currentIdleDuration = 0f;
            isIdling = false;
            moveComponent.Exit();
        }
    }
}