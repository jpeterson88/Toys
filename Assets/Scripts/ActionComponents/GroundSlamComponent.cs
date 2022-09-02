using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.ActionComponents
{
    class GroundSlamComponent : MonoBehaviour
    {
        [SerializeField] private float slamForce;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private BoxCaster boxCaster;
        private float startingDrag;

        private void Awake()
        {
            startingDrag = rb2d.drag;
        }

        public void Slam()
        {
            rb2d.drag = 0f;
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.down * slamForce);
        }

        public void Reset()
        {
            rb2d.drag = startingDrag;
        }

        public bool CanSlam() => !boxCaster.IsHit();
    }
}