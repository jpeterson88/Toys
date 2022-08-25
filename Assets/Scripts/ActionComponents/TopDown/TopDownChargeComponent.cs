using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    class TopDownChargeComponent : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private float speed;
        [SerializeField] private float cooldown;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private ParticleSystem particles;

        private bool isCharging;

        public void InitiateCharge()
        {
            isCharging = true;
            StartCoroutine(ChargeCd());
        }

        public bool IsCharging() => isCharging;

        private IEnumerator ChargeCd()
        {
            yield return new WaitForSeconds(delay);
            rb2d.AddForce(transform.root.up * speed, ForceMode2D.Force);
            sfx?.Play();
            particles?.Play();
            yield return new WaitForSeconds(cooldown);
            isCharging = false;
        }

        public void Reset()
        {
            particles?.Stop();
            //TODO: Potentially stop Coroutine if forced exit?
        }
    }
}