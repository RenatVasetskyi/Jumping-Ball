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
        [SerializeField] private Transform _up;
        [SerializeField] private List<BeamPlatform> _platforms;
        
        private GameSettings _gameSettings;
        
        public Transform Up => _up;
        public List<BeamPlatform> Platforms => _platforms;

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
            List<ColorConfig> platformConfigs = _gameSettings.ColorConfigs.ToList();
            
            foreach (BeamPlatform platform in _platforms)
            {
                ColorConfig randomConfig = GetRandomPlatformConfig(platformConfigs);
                platform.SetConfig(randomConfig);
                platformConfigs.Remove(randomConfig);
            }
        }

        private ColorConfig GetRandomPlatformConfig(List<ColorConfig> platformConfigs)
        {
            return platformConfigs[Random.Range(0, platformConfigs.Count)];
        }
    }
}