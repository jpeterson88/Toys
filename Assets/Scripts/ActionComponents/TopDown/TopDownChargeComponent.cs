using Assets.Scripts.General;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    class TopDownChargeComponent : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private float speed;
        [SerializeField] private float chargeDuration;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private TimerComponent timer;
        [SerializeField] private bool rotateDuringDelay;

        private bool isCharging, delayComplete;

        public void InitiateCharge()
        {
            isCharging = true;
            delayComplete = false;
            StartCoroutine(ChargeCd());
        }

        public bool IsCharging() => isCharging;

        private IEnumerator ChargeCd()
        {
            yield return new WaitForSeconds(delay);
            rb2d.AddForce(transform.root.up * speed, ForceMode2D.Force);
            sfx?.Play();
            particles?.Play();
            delayComplete = true;
            yield return new WaitForSeconds(chargeDuration);
            timer.Begin();
            isCharging = false;
        }

        public void RotateDuringDelay()
        {
            if (rotateDuringDelay && !delayComplete)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.root.rotation = Quaternion.RotateTowards(transform.root.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        public bool CanCharge() => !timer.IsActive() && !isCharging;

        public void Reset()
        {
            particles?.Stop();
            //TODO: Potentially stop Coroutine if forced exit?
        }
    }
}