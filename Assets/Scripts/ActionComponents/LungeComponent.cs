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

        private bool hasStarted, isFinished, hasInitiated;
        private float startingDrag;

        private void Awake() => startingDrag = rb2d.drag;

        public void InitiateLunge()
        {
            hasInitiated = true;
            rb2d.velocity = Vector2.zero;
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
            if (!hasStarted && hasInitiated && rb2d.velocity.magnitude > stopMagnitude)
            {
                hasStarted = true;
                ps.Play();
                audio.Play();
            }
            else if (hasStarted && !isFinished && rb2d.velocity.magnitude < stopMagnitude)
            {
                isFinished = true;
            }
        }

        public bool IsFinished() => isFinished;

        public bool HasInitiated() => hasInitiated;

        public void Reset()
        {
            hasStarted = false;
            isFinished = false;
            hasInitiated = false;
            rb2d.drag = startingDrag;
            ps.Stop();
        }
    }
}