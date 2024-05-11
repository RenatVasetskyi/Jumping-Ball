using System.Collections;
using UnityEngine;

namespace Game.Particle
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PlayAndDestroyParticle : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        
        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystem.Play();
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(_particleSystem.main.duration);
            
            Destroy(gameObject);
        }
    }
}