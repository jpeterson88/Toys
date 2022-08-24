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

        public Action TriggerLPressed { get; set; }

        public Action TriggerRPressed { get; set; }

        public void SetMoveVector(CallbackContext callbackContext) => currentMoveVector = callbackContext.ReadValue<Vector2>();

        public Vector2 GetMoveVector() => currentMoveVector;

        public void OnJumpPressed() => JumpPressed?.Invoke();

        public void OnAttack2Pressed() => Attack2Pressed?.Invoke();

        public void OnTriggerLPressed() => TriggerLPressed?.Invoke();

        public void OnTriggerRPressed() => TriggerRPressed?.Invoke();
    }
}