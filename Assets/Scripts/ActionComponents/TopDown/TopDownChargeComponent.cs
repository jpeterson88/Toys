using Assets.Scripts.General;
using Assets.Scripts.Scriptables.Events;
using System;
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
        [SerializeField] private TargetComponent targetComponent;
        [SerializeField] private ChargeMeterEvent chargeReadyEvent, chargeUsedEvent;

        private bool isCharging, delayComplete;
        private Coroutine chargeRoutine;

        public void InitiateCharge()
        {
            isCharging = true;
            delayComplete = false;
            chargeUsedEvent?.Invoke();
            chargeRoutine = StartCoroutine(ChargeCd());
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
            isCharging = false;
        }

        public void RotateDuringDelay()
        {
            if (targetComponent != null && targetComponent.GetTarget() != null)
            {
                if (rotateDuringDelay && !delayComplete)
                {
                    Vector2 direction = targetComponent.GetTarget().position - transform.root.position;
                    Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
                    transform.root.rotation = Quaternion.RotateTowards(transform.root.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }

        public bool CanCharge() => !timer.IsActive() && !isCharging;

        public void Reset()
        {
            particles?.Stop();
            isCharging = false;
            delayComplete = false;
            StopCoroutine(chargeRoutine);

            timer.Begin(() => chargeReadyEvent?.Invoke());
        }
    }
}