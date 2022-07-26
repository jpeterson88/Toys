using Assets.Scripts.State;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    class Player : MonoBehaviour
    {
        [SerializeField] private PlayerStateMachine stateMachine;
        private GroundedDetector groundedDetector;

        private bool previouslyGrounded;

        public static Action OnLand = delegate { };

        private void Awake()
        {
            groundedDetector = GetComponentInChildren<GroundedDetector>();
        }

        private void Start()
        {
            var startingState = groundedDetector.IsGrounded() ? PlayerStates.Airborne : PlayerStates.Idle;

            stateMachine.SetState((int)startingState);
        }

        private void Update()
        {
            if (groundedDetector.IsGrounded() && !previouslyGrounded)
            {
                OnLand?.Invoke();
            }

            previouslyGrounded = groundedDetector.IsGrounded();
        }
    }
}