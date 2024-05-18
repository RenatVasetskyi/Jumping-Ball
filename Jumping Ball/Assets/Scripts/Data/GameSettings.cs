using Game.Beam.Data;
using Game.Camera.Data;
using Game.Player.Data;
using Game.UI.CountDown.Data;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Create Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public MaterialConfig[] MaterialConfigs;
        public GameCameraConfig GameCameraConfig;
        public BallConfig BallConfig;
        public GameCountDownConfig GameCountDownConfig;
        public float ShowLoseWindowDelay;
        public float ShowVictoryWindowDelay;
    }
}