using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.Input
{
    public class PlayerInputMap : MonoBehaviour, IInput
    {
        Vector2 currentMoveVector;

        public Action JumpPressed { get; set; }

        public void SetMoveVector(CallbackContext callbackContext) => currentMoveVector = callbackContext.ReadValue<Vector2>();

        public Vector2 GetMoveVector() => currentMoveVector;

        public void OnJumpPressed() => JumpPressed?.Invoke();
    }
}