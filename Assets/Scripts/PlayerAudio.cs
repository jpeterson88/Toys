using System;
using UnityEngine;

namespace Assets.Scripts
{
    class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource land;

        private void Awake()
        {
            Player.OnLand += HandleLand;
        }

        private void OnDisable()
        {
            Player.OnLand -= HandleLand;
        }

        private void HandleLand()
        {
            land?.Play();
        }
    }
}