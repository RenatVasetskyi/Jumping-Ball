using System;
using UnityEngine;

namespace Game.UI.CountDown.Data
{
    [Serializable]
    public class GameCountDownConfig
    {
        public int TimeInSecondsBeforeGameStart;
        
        public Vector3 MinScale;
        public Vector3 MaxScale;
        
        public float ScaleDuration;
        public float UnscaleDuration;

        public LeanTweenType ScaleEasing;
        public LeanTweenType UnScaleEasing;
    }
}