using Assets.Scripts.Audio;
using Assets.Scripts.Scriptables.Events;
using UnityEngine;

namespace Assets.Scripts
{
    class LandComponent : MonoBehaviour
    {
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] private Vector2 scaleTo;
        [SerializeField] private float scaleDuration;
        [SerializeField] private float heavyVelocityThreshold, minimumLandThreshold, normalShakeForce, heavyShakeForce;
        [SerializeField] private CamShakeEvent camShakeEvent;
        [SerializeField] private RandomPitchPlayer hardLandPlayer, normalLandPlayer;

        private ParticleSystem ps;

        private void Awake() => ps = GetComponent<ParticleSystem>();

        public void HandleLand(float landVelocity)
        {
            float absVelocity = Mathf.Abs(landVelocity);
            if (absVelocity >= minimumLandThreshold)
            {
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
            RandomPitchPlayer source = landVelocity >= heavyVelocityThreshold ? hardLandPlayer : normalLandPlayer;
            source.Play();
        }

        private void HandleShake(float landVelocity)
        {
            if (landVelocity >= heavyVelocityThreshold)
            {
                //TODO: 10f is just a random velocity. should make it changeable in editor
                float force = landVelocity > 10f ? heavyShakeForce : normalShakeForce;
                camShakeEvent.Invoke(force);
            }
        }
    }
}