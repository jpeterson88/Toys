using Assets.Scripts.General;
using Assets.Scripts.Utility;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ActionComponents
{
    class GroundSlamComponent : MonoBehaviour
    {
        [SerializeField] private float slamForce;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private BoxCaster boxCaster;
        [SerializeField] private AudioSource whoosh;
        [SerializeField] private float slamPause;
        [SerializeField] private TimerComponent timer;

        private float startingDrag;

        private void Awake()
        {
            startingDrag = rb2d.drag;
        }

        public void Slam()
        {
            rb2d.drag = 0f;
            rb2d.velocity = Vector2.zero;
            StartCoroutine(SlamDelay());
        }

        private IEnumerator SlamDelay()
        {
            yield return new WaitForSeconds(slamPause);
            whoosh?.Play();
            rb2d.AddForce(Vector2.down * slamForce);
            timer.Begin();
        }

        public void Reset() => rb2d.drag = startingDrag;

        public bool CanSlam()
        {
            return !boxCaster.IsHit() && !timer.IsActive();
        }
    }
}