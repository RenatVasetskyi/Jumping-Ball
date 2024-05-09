using Game.Beam.Data;
using Game.Beam.Enums;
using UnityEngine;

namespace Game.Beam
{
    public class BeamPlatform : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private ColorConfig _config;
        public ColorType ColorType { get; private set; }

        public void SetConfig(ColorConfig config)
        {
            _config = config;
            _meshRenderer.material.color = _config.Color;
            ColorType = config.Type;
        }
    }
}