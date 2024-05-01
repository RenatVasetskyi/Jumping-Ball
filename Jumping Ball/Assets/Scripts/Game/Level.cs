using System.Collections.Generic;
using Game.Beam;
using UnityEngine;

namespace Game
{
    public class Level : MonoBehaviour
    { 
        [SerializeField] private List<BeamLine> _beamLines;
        [SerializeField] private Transform _ballStartPoint;

        public List<BeamLine> BeamLines => _beamLines;
        public Transform BallStartPoint => _ballStartPoint;

        public void SendLose()
        {
            
        }

        public void SendVictory()
        {
            
        }
    }
}