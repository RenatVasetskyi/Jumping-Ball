using System;
using UnityEngine;

namespace Game.Camera.Data
{
    [Serializable]
    public class GameCameraConfig
    {
        public Vector3 FollowTargetOffset;
        public Vector3 StartRotation;
    }
}