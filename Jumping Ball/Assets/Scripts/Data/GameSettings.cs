using Game.Beam.Data;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Create Settings Holder/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public BeamPlatformConfig[] BeamPlatformConfigs;
    }
}