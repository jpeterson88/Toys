using UnityEngine;

namespace Assets.Scripts.Utility
{
    class ClimbDetection : MonoBehaviour
    {
        bool canClimb;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
                canClimb = true;

            Debug.Log($"Enter: {canClimb}");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
                canClimb = false;

            Debug.Log($"Exit: {canClimb}");
        }

        public bool CanClimb() => canClimb;
    }
}