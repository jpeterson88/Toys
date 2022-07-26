using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    interface IInput
    {
        Vector2 GetMoveVector();

        Action JumpPressed { get; set; }
    }
}