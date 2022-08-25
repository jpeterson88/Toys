using UnityEngine;

namespace Assets.Scripts.Utility
{
    class PlayRandomFromArray : MonoBehaviour
    {
        private AudioSource[] sources;

        private void Awake()
        {
            sources = GetComponentsInChildren<AudioSource>();

            if (sources == null)
                Debug.LogError($"Failed to get sources for PlayRandomFromArray On Object {transform.root.name}");
        }

        public void PlayRandom()
        {
            int index = Random.Range(0, sources.Length);

            var source = sources[index];

            source.Play();
        }
    }
}