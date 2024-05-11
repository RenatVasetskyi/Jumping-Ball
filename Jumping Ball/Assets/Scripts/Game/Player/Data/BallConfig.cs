using System;

namespace Game.Player.Data
{
    [Serializable]
    public class BallConfig
    {
        public float JumpForce;
        public float JumpDuration;
        public float ChangeLineDuration;
        public float RotationEndValue;
        public float RotationDuration;
    }
}