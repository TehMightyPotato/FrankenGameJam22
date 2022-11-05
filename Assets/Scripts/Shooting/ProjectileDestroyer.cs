using System;
using UnityEngine;

namespace Shooting
{
    public class ProjectileDestroyer : MonoBehaviour
    {
        [SerializeField] private ScoreHandler scoreHandler;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Projectile")) return;
            scoreHandler.ShotMissed();
            Destroy(other.gameObject);
        }
    }
}
