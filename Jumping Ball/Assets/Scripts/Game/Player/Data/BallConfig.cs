using System;

namespace Game.Player.Data
{
    [Serializable]
    public class BallConfig
    {
        public float JumpForce;
        public float JumpDuration;
        public int NumberOfJumps;
        public float ChangeLineDuration;
    }
}