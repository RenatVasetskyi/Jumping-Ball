using System.Collections.Generic;
using System.Linq;
using Data;
using Game.Beam.Data;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Beam
{
    public class BeamLine : MonoBehaviour
    {
        [SerializeField] private List<BeamPlatform> _platforms;
        
        private GameSettings _gameSettings;

        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            SetRandomPlatformConfigs();
        }

        private void SetRandomPlatformConfigs()
        {
            List<BeamPlatformConfig> platformConfigs = _gameSettings.BeamPlatformConfigs.ToList();
            
            foreach (BeamPlatform platform in _platforms)
            {
                BeamPlatformConfig randomConfig = GetRandomPlatformConfig(platformConfigs);
                platform.SetConfig(randomConfig);
                platformConfigs.Remove(randomConfig);
            }
        }

        private BeamPlatformConfig GetRandomPlatformConfig(List<BeamPlatformConfig> platformConfigs)
        {
            return platformConfigs[Random.Range(0, platformConfigs.Count)];
        }
    }
}