using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Game
{
    public static class DotWeenExtensions
    {
        public static IEnumerator DoJumpWithoutX(this Transform target, Vector3 endValue,
            float jumpHeight, float duration, Action onComplete = null)
        {
            int completedOperations = 0;
            int operations = 3;

            target.DOMoveZ(endValue.z, duration).SetEase(Ease.Linear).onComplete += () => completedOperations++;;
            
            target.DOMoveY(target.position.y + jumpHeight, duration / 2).SetEase(Ease.OutQuad).onComplete = () =>
            {
                target.DOMoveY(endValue.y, duration / 2).SetEase(Ease.InQuad).onComplete = () => completedOperations++;
                
                completedOperations++;
            }; 

            while (completedOperations < operations)
                yield return null;
            
            onComplete?.Invoke();
        }
    }
}