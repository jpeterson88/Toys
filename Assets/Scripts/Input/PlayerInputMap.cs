using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.Input
{
    public class PlayerInputMap : MonoBehaviour, IInput
    {
        Vector2 currentMoveVector;

        public Action JumpPressed { get; set; }
        public Action Attack2Pressed { get; set; }
        public Action SprintPressed { get; set; }

        public Action TriggerLPressed { get; set; }

        public Action TriggerRPressed { get; set; }

        public Action LockOnPressed { get; set; }

        public void SetMoveVector(CallbackContext callbackContext) => currentMoveVector = callbackContext.ReadValue<Vector2>();

        public Vector2 GetMoveVector() => currentMoveVector;

        public void OnJumpPressed(CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                JumpPressed?.Invoke();
        }

        public void OnAttack2Pressed(CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                Attack2Pressed?.Invoke();
        }

        public void OnSprintPressed(CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                SprintPressed?.Invoke();
        }

        public void OnLockPressed(CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                LockOnPressed?.Invoke();
        }

        public void OnTriggerLPressed() => TriggerLPressed?.Invoke();

        public void OnTriggerRPressed() => TriggerRPressed?.Invoke();
    }
}