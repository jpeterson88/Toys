using UnityEngine;

namespace Assets.Scripts.Utility
{
    class ClimbDetection : MonoBehaviour
    {
        bool canClimb;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Enter can climb");
            canClimb = collision.CompareTag("Climbable");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("Exit can climb");
            canClimb = false;
        }

        public bool CanClimb() => canClimb;
    }
}