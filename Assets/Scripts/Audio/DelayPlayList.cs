using Assets.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class DelayPlayList : MonoBehaviour
    {
        [SerializeField] private RandomPitchPlayer[] players;
        [SerializeField] private TimerComponent timer;

        private bool isPlaying;
        private RandomPitchPlayer currentPlayer;

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

        public void Stop()
        {
            isPlaying = false;

            if (currentPlayer != null)
                currentPlayer.Stop();
        }

        public void Next()
        {
            timer.Begin();
            int value = Random.Range(0, players.Length);
            currentPlayer = players[value];

            currentPlayer?.Play();
        }
    }
}