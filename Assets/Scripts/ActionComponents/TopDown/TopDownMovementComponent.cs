using System;
using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    class TopDownMovementComponent : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private AudioSource walkSfx;

        internal void ApplyMovement(Vector2 moveVector)
        {
            //Rotate object
            if (moveVector.x != 0)
            {
                Vector2 lookDirection = moveVector.x > 0 ? Vector2.left : Vector2.right;
                transform.root.Rotate(new Vector3(0, 0, lookDirection.x * rotationSpeed * Time.deltaTime));
            }

            //Is walking
            if (moveVector.y != 0)
            {
                if (!walkSfx.isPlaying)
                    walkSfx?.Play();

                Vector2 direction = moveVector.y > 0 ? transform.root.up : -transform.root.up;
                rb2d.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode2D.Force);
            }
            else if (walkSfx.isPlaying)
                walkSfx.Stop();
        }
    }
}