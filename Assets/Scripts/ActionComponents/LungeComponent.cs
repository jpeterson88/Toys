using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ActionComponents
{
    class LungeComponent : MonoBehaviour
    {
        [SerializeField] private float lungeForce;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float delayTime;
        [SerializeField] private float stopMagnitude;
        [SerializeField] private FacingDirection facingDirection;
        [SerializeField] private float linearDragIncrease;
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private AudioSource audio;

        private bool hasStarted;
        private float startingDrag;
        private bool isFinished;

        private void Awake() => startingDrag = rb2d.drag;

        public void InitiateLunge()
        {
            rb2d.velocity = Vector2.zero;
            isFinished = false;
            rb2d.drag = linearDragIncrease;
            StartCoroutine(LungeDelay());
        }

        private IEnumerator LungeDelay()
        {
            yield return new WaitForSeconds(delayTime);

            Vector2 direction = facingDirection.GetFacingDirection();
            float lungeForceDirection = direction.x > 0 ? lungeForce : -lungeForce;
            float psDirection = direction.x > 0 ? 180 : 0;
            ps.transform.eulerAngles = new Vector3(0, psDirection, 0);
            rb2d.AddForce(new Vector2(lungeForceDirection, 0), ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (!hasStarted && !isFinished && rb2d.velocity.magnitude > stopMagnitude)
            {
                hasStarted = true;
                ps.Play();
                audio.Play();
            }
            else if (hasStarted && !isFinished && rb2d.velocity.magnitude < stopMagnitude)
            {
                isFinished = true;
                ps.Stop();
            }
        }

        public bool IsFinished() => isFinished;

        public void Reset()
        {
            hasStarted = false;
            isFinished = true;
            rb2d.drag = startingDrag;
        }
    }
}