using UnityEngine;

namespace Assets.Scripts.Utility
{
    class PlayRandomFromArray : MonoBehaviour
    {
        [SerializeField] private AudioSource[] sources;

        public void PlayRandom()
        {
            int index = UnityEngine.Random.Range(0, sources.Length);

            var source = sources[index];

            source.Play();
        }
    }
}