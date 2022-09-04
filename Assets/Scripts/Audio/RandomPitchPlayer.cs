using UnityEngine;

namespace Assets.Scripts.Audio
{
    class RandomPitchPlayer : MonoBehaviour
    {
        [SerializeField] private float minPitch, maxPitch;
        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();

            if (source == null)
                Debug.LogError($"Failed to get source from object: {transform.name}");
        }

        public void Play()
        {
            float random = Random.Range(minPitch, maxPitch);
            source.pitch = random;
            source?.Play();
        }

        public void Stop() => source.Stop();

        public bool IsPlaying() => source.isPlaying;
    }
}