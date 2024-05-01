using Game.Beam.Data;
using Game.Camera.Data;
using Game.Player.Data;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Create Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public BeamPlatformConfig[] BeamPlatformConfigs;
        public GameCameraConfig GameCameraConfig;
        public BallConfig BallConfig;
    }
}