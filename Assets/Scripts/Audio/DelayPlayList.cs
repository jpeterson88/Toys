using Assets.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class DelayPlayList : MonoBehaviour
    {
        [SerializeField] private TimerComponent timer;

        private bool isPlaying;
        private RandomPitchPlayer currentPlayer;
        private RandomPitchPlayer[] players;

        private void Awake()
        {
            players = GetComponentsInChildren<RandomPitchPlayer>();

            if (players == null)
                Debug.LogError($"Failed to get players on object: {transform.name}");
        }

        public bool IsPlaying() => isPlaying;

        private void Update()
        {
            if (isPlaying && !timer.IsActive())
                Next();
        }

        public void Play()
        {
            Next();
            isPlaying = true;
        }

        public void Stop(bool forceCurrentAudioStop = true)
        {
            isPlaying = false;

            if (currentPlayer != null && forceCurrentAudioStop)
                currentPlayer.Stop();
        }

        public void Next()
        {
            if (currentPlayer == null || !currentPlayer.IsPlaying())
            {
                timer.Begin();
                int value = Random.Range(0, players.Length);
                currentPlayer = players[value];

                currentPlayer?.Play();
            }
        }
    }
}