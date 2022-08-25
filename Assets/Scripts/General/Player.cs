using Assets.Scripts.State;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.General
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