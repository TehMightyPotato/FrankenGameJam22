using System;
using System.Collections.Generic;
using Planets.Needs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planets
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private Rigidbody ownRigidbody;

        [SerializeField] private Renderer ownRenderer;

        [SerializeField] private Vector3 initialVelocity;
        
        [SerializeField] private Difficulty.Difficulty difficulty;
        [SerializeField] private NeedManager needManager;

        [SerializeField] private List<Need> needs;

        private void Awake()
        {
            var newMat = new Material(ownRenderer.material)
            {
                color = Random.ColorHSV()
            };
            ownRenderer.material = newMat;
        }

        private void Start()
        {
            ownRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
            GenerateNeeds();
        }

        private void GenerateNeeds()
        {
            var count = difficulty.CalcPlanetNeedsCount();
            needs = needManager.GetRandomNeeds(count);
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Projectile")) return;
            
        }
    }
}
