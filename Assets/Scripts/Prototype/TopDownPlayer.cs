using Assets.Scripts.Input;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Prototype
{
    class TopDownPlayer : MonoBehaviour
    {
        [SerializeField] private float moveSpeed, lungeSpeed, sideHopSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float lungeDelay, lungeCd;
        [SerializeField] private float sideHopDelay, sideHopCd;
        [SerializeField] private Transform leftDir, rightDir;
        [SerializeField] private float hurtDuration, hurtCd, hurtKnockbackSpeed;
        [SerializeField] private AudioSource lunge, walk;
        [SerializeField] private ParticleSystem lungePs;
        [SerializeField] private AudioSource[] screeches;

        private IInput input;

        [HideInInspector] public bool isLunging, isSideHop, isHurt, canBeHurt;

        private void Awake()
        {
            input = GetComponent<IInput>();

            input.Attack2Pressed += HandleLunge;
            input.TriggerLPressed += SideHopLeft;
            input.TriggerRPressed += SideHopRight;

            canBeHurt = true;
        }

        private void SideHopLeft()
        {
            if (!isSideHop)
            {
                isSideHop = true;
                StartCoroutine(SideHopCd(leftDir.up));
            }
        }

        private void SideHopRight()
        {
            if (!isSideHop)
            {
                isSideHop = true;
                StartCoroutine(SideHopCd(rightDir.up));
            }
        }

        private void HandleLunge()
        {
            if (!isLunging)
            {
                isLunging = true;
                StartCoroutine(LungeCd());
                lunge?.Play();
                lungePs?.Play();
            }
        }

        private void Update()
        {
            if (!isLunging && !isSideHop && !isHurt)
            {
                if (input.GetMoveVector().x != 0)
                {
                    Vector2 lookDirection = input.GetMoveVector().x > 0 ? Vector2.left : Vector2.right;
                    transform.Rotate(new Vector3(0, 0, lookDirection.x * rotationSpeed * Time.deltaTime));
                }

                //Is walking
                if (input.GetMoveVector().y != 0)
                {
                    if (!walk.isPlaying)
                        walk?.Play();

                    Vector2 direction = input.GetMoveVector().y > 0 ? transform.up : -transform.up;
                    rb2d.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode2D.Force);
                }
                else if (walk.isPlaying)
                    walk.Stop();
            }
        }

        private IEnumerator LungeCd()
        {
            yield return new WaitForSeconds(lungeDelay);
            rb2d.AddForce(transform.up * lungeSpeed, ForceMode2D.Force);
            yield return new WaitForSeconds(lungeCd);
            isLunging = false;
        }

        private IEnumerator SideHopCd(Vector2 direction)
        {
            yield return new WaitForSeconds(sideHopDelay);
            rb2d.AddForce(direction * sideHopSpeed, ForceMode2D.Force);
            yield return new WaitForSeconds(sideHopCd);
            isSideHop = false;
        }

        public void Hurt()
        {
            isHurt = true;
            PlayScreech();
            StartCoroutine(HurtCd());
        }

        public IEnumerator HurtCd()
        {
            canBeHurt = false;
            rb2d.AddForce(-transform.up * hurtKnockbackSpeed, ForceMode2D.Force);
            yield return new WaitForSeconds(hurtDuration);
            isHurt = false;
            yield return new WaitForSeconds(hurtCd);
            canBeHurt = true;
        }

        public void PlayScreech()
        {
            int index = UnityEngine.Random.Range(0, screeches.Length);

            var source = screeches[index];

            source.Play();
        }
    }
}