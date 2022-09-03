using UnityEngine;

namespace Assets.Scripts.ActionComponents
{
    class AirborneComponent : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool useForce;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float maxSpeed;

        public void Move(Vector2 inputDirection)
        {
            if (inputDirection.x > 0)
            {
                if (useForce)
                    rb2d.AddForce(Vector2.right * moveSpeed * Time.deltaTime, ForceMode2D.Force);
                else
                {
                    if (rb2d.velocity.magnitude < maxSpeed)
                        rb2d.velocity = new Vector2(rb2d.velocity.x + (moveSpeed * Time.deltaTime), rb2d.velocity.y);
                }
            }
            else if (inputDirection.x < 0)
            {
                if (useForce)
                    rb2d.AddForce(Vector2.left * moveSpeed * Time.deltaTime, ForceMode2D.Force);
                else
                {
                    if (rb2d.velocity.magnitude < maxSpeed)
                        rb2d.velocity = new Vector2(rb2d.velocity.x - (moveSpeed * Time.deltaTime), rb2d.velocity.y);
                }
            }
        }
    }
}