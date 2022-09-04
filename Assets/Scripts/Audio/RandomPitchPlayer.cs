using UnityEngine;

namespace Assets.Scripts.Audio
{
    class RandomPitchPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private float minPitch, maxPitch;

        public void Play()
        {
            float random = Random.Range(minPitch, maxPitch);
            source.pitch = random;
            source?.Play();
        }
    }
}