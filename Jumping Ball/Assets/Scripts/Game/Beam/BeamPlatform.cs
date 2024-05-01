using Game.Beam.Data;
using UnityEngine;

namespace Game.Beam
{
    public class BeamPlatform : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private BeamPlatformConfig _config;
        
        public void SetConfig(BeamPlatformConfig config)
        {
            _config = config;
            _meshRenderer.material.color = _config.Color;
        }
    }
}