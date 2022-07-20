using UnityEngine;

namespace Assets.Scripts.State.StateHandlers.Animation
{
    class AnimationStateHandlerBase : MonoBehaviour
    {
        //[SerializeField]
        //internal SpineSkeletonAnimationHandle animationHandle;

        //private void Start()
        //{
        //    animationHandle.skeletonAnimation.AnimationState.Complete += HandleTrackComplete;
        //    animationHandle.skeletonAnimation.AnimationState.Event += HandleAnimationEvent;
        //}

        //private void OnDisable()
        //{
        //    animationHandle.skeletonAnimation.AnimationState.Complete -= HandleTrackComplete;
        //    animationHandle.skeletonAnimation.AnimationState.Event -= HandleAnimationEvent;
        //}

        //protected virtual void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event spineEvent)
        //{
        //}

        //protected virtual void HandleTrackComplete(TrackEntry trackEntry)
        //{
        //}

        //protected bool IsCurrentlyPlayingAnimation(string name)
        //{
        //    return animationHandle.skeletonAnimation.AnimationName == name;
        //}
    }
}