using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    class TopDownMovementComponentAbsolute : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private AudioSource walkSfx;

        internal void ApplyMovement(Vector2 moveVector)
        {
            //Is walking
            if (moveVector.magnitude != 0)
            {
                if (!walkSfx.isPlaying)
                    walkSfx?.Play();

                Vector2 direction = new Vector2(moveVector.x, moveVector.y);
                rb2d.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode2D.Force);

                direction.Normalize();

                //Rotate player
                if (direction != Vector2.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
                    transform.root.rotation = Quaternion.RotateTowards(transform.root.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }

        public void Exit()
        {
            walkSfx.Stop();
        }
    }
}