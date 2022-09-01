using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    class GroundSlamComponent : MonoBehaviour
    {
        [SerializeField] private float slamForce;
        [SerializeField] private Rigidbody2D rb2d;

        public void Slam()
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.down * slamForce);
        }
    }
}