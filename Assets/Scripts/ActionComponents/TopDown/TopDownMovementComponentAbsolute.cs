using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    class TopDownMovementComponentAbsolute : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private AudioSource walkSfx;
        [SerializeField] private LockOnComponent lockOnComponent;

        internal void ApplyMovement(Vector2 moveVector)
        {
            //Is walking
            Vector2 direction = new Vector2(moveVector.x, moveVector.y);

            if (moveVector.magnitude != 0)
            {
                if (!walkSfx.isPlaying)
                    walkSfx?.Play();

                rb2d.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode2D.Force);
            }

            direction.Normalize();

            RotateMovement(direction);
        }

        private void RotateMovement(Vector2 direction)
        {
            //Manually steer player rotation
            if ((lockOnComponent == null || lockOnComponent.GetLockedTarget() == null) && direction != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.root.rotation = Quaternion.RotateTowards(transform.root.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        public void Exit()
        {
            walkSfx.Stop();
        }
    }
}