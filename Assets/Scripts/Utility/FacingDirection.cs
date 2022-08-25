using UnityEngine;

namespace Assets.Scripts
{
    class FacingDirection : MonoBehaviour
    {
        private Vector2 currentDirection;

        public Vector2 GetFacingDirection()
        {
            return currentDirection;
        }

        public void SetFacingDirection(Vector2 direction)
        {
            if (direction.x > 0)
                currentDirection = Vector2.right;
            else if (direction.x < 0)
                currentDirection = Vector2.left;
        }
    }
}