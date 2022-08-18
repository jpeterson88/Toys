using Assets.Scripts.Scriptables.Events;
using UnityEngine;

namespace Assets.Scripts
{
    class LandComponent : MonoBehaviour
    {
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] private Vector2 scaleTo;
        [SerializeField] private float scaleDuration;
        [SerializeField] private AudioSource normalLand, hardLand;
        [SerializeField] private float heavyVelocityThreshold, minimumLandThreshold, normalShakeForce, heavyShakeForce;
        [SerializeField] private CamShakeEvent camShakeEvent;

        private ParticleSystem ps;

        private void Awake() => ps = GetComponent<ParticleSystem>();

        public void HandleLand(float landVelocity)
        {
            float absVelocity = Mathf.Abs(landVelocity);
            if (absVelocity >= minimumLandThreshold)
            {
                Debug.Log(absVelocity);
                PlayAudioByMagnitude(absVelocity);
                PlayAnimation();
                HandleShake(absVelocity);
                ps.Play();
            }
        }

        private void PlayAnimation()
        {
            LeanTween.cancel(spriteGameObject);
            LeanTween.scale(spriteGameObject, scaleTo, scaleDuration)
                .setEaseOutCubic()
                .setLoopPingPong(1);
        }

        private void PlayAudioByMagnitude(float landVelocity)
        {
            AudioSource source = landVelocity >= heavyVelocityThreshold ? hardLand : normalLand;
            source.Play();
        }

        private void HandleShake(float landVelocity)
        {
            if (landVelocity >= heavyVelocityThreshold)
            {
                //TODO: 13f is just a random velocity. should make it changeable in editor
                float force = landVelocity > 10f ? heavyShakeForce : normalShakeForce;
                camShakeEvent.Invoke(force);
            }
        }
    }
}