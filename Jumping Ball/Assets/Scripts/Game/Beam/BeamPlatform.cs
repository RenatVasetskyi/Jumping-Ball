using Game.Beam.Data;
using Game.Beam.Enums;
using UnityEngine;

namespace Game.Beam
{
    public class BeamPlatform : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private MaterialConfig _config;
        public MaterialType MaterialType { get; private set; }

        public void SetConfig(MaterialConfig config)
        {
            _config = config;
            _meshRenderer.material = _config.Material;
            MaterialType = config.Type;
        }
    }
}