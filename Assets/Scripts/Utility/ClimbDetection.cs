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
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
                canClimb = false;
        }

        public bool CanClimb() => canClimb;
    }
}