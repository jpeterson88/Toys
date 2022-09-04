using Assets.Scripts.Audio;
using Assets.Scripts.Utility;
using System;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    class Shroom : MonoBehaviour
    {
        [SerializeField] private RandomPitchPlayer pitchPlayer;
        [SerializeField] private LaunchOnEnter launchOnEnter;
        [SerializeField] private GameObject shroomTop;
        [SerializeField] private Vector2 shroomScaleTo;
        [SerializeField] private float shroomScalTime;
        [SerializeField] private LeanTweenType easeType;

        private void Awake()
        {
            launchOnEnter.OnTrigger += HandleEnter;
        }

        private void HandleEnter()
        {
            pitchPlayer.Play();
            LeanTween.scale(shroomTop, shroomScaleTo, shroomScalTime)
                .setLoopPingPong(2)
                .setEase(easeType);
        }
    }
}