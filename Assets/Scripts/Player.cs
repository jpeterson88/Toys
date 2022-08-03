using Assets.Scripts.State;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    class Player : MonoBehaviour
    {
        [SerializeField] private PlayerStateMachine stateMachine;
        private GroundedDetector groundedDetector;

        private void Awake()
        {
            groundedDetector = GetComponentInChildren<GroundedDetector>();
        }

        private void Start()
        {
            var startingState = groundedDetector.IsGrounded() ? PlayerStates.Airborne : PlayerStates.Idle;

            stateMachine.SetState((int)startingState);
        }
    }
}