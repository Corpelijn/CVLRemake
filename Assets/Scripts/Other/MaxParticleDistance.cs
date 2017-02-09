using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Other
{
    [RequireComponent(typeof(ParticleSystem))]
    public class MaxParticleDistance : MonoBehaviour
    {
        public float DistanceToDestroy;
        private ParticleSystem cachedSystem;
        private Vector3 staticPosition;
        void Start()
        {
            cachedSystem = this.GetComponent<ParticleSystem>();
            staticPosition = this.transform.position;
        }
        void Update()
        {
            ParticleSystem.Particle[] ps = new ParticleSystem.Particle[cachedSystem.particleCount];
            cachedSystem.GetParticles(ps);
            // keep only particles that are within DistanceToDestroy
            var distanceParticles = ps.Where(p => Vector3.Distance(staticPosition, p.position) < DistanceToDestroy).ToArray();
            cachedSystem.SetParticles(distanceParticles, distanceParticles.Length);
        }
    }
}
