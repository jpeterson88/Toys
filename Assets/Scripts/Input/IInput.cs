using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    interface IInput
    {
        Vector2 GetMoveVector();

        Action JumpPressed { get; set; }

        Action Attack2Pressed { get; set; }

        Action TriggerLPressed { get; set; }
        Action TriggerRPressed { get; set; }
    }
}