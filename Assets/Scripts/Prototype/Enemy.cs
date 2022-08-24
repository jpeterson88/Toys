using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Prototype
{
    class Enemy : MonoBehaviour
    {
        [SerializeField] private float takeDamageForce;

        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private GameObject player;
        [SerializeField] private float offset;

        [SerializeField] private float hitDuration;
        [SerializeField] private float moveTowardsCooldown, moveTowardsDuration;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float dealtDamageCooldown;
        [SerializeField] private AudioSource walkAudio;

        private bool isHit, isOnMoveCd, recentlyDidDamage;
        private float moveDuration;

        public void TakeHit()
        {
            if (!isHit)
            {
                rb2d.AddForce(-transform.up * takeDamageForce, ForceMode2D.Impulse);
                isHit = true;
                walkAudio.Stop();
                StartCoroutine(HitState());
            }
        }

        internal void DoDamage()
        {
            recentlyDidDamage = true;
            walkAudio.Stop();
            StartCoroutine(DamageCd());
        }

        private void Update()
        {
            if (!isHit && !recentlyDidDamage)
            {
                Vector3 relative = transform.InverseTransformPoint(player.transform.position);
                float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - offset;
                transform.Rotate(0, 0, angle);

                //Move Towards
                if (!isOnMoveCd)
                {
                    if (!walkAudio.isPlaying)
                        walkAudio.Play();

                    moveDuration += Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                }

                if (moveDuration >= moveTowardsDuration)
                {
                    StartCoroutine(MoveCD());
                    walkAudio.Stop();
                    moveDuration = 0f;
                }
            }
        }

        private IEnumerator HitState()
        {
            yield return new WaitForSeconds(hitDuration);
            isHit = false;
        }

        private IEnumerator MoveCD()
        {
            isOnMoveCd = true;
            yield return new WaitForSeconds(moveTowardsCooldown);
            isOnMoveCd = false;
        }

        private IEnumerator DamageCd()
        {
            recentlyDidDamage = true;
            yield return new WaitForSeconds(dealtDamageCooldown);
            recentlyDidDamage = false;
        }
    }
}