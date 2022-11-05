using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planets
{ 
    public class PlanetSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject planetPrefab;
        private Coroutine _spawnPlanetsRoutine;
        
        [SerializeField] private BoxCollider spawnArea;

        [SerializeField] private Difficulty.Difficulty difficulty;

        [SerializeField] private float initialDelay;
        
        #if UNITY_EDITOR
        public bool gizmos;
        #endif
        
        private void Start()
        {
            _spawnPlanetsRoutine = StartCoroutine(SpawnRoutine());
        }

        private void SpawnPlanet()
        { 
            Instantiate(planetPrefab, CalcPlanetPosition(), quaternion.identity);
        }

        private Vector3 CalcPlanetPosition()
        {
            return spawnArea.GetRandomPointInsideCollider();
        }
        
        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(initialDelay);
            while (true)
            {
                SpawnPlanet();
                yield return new WaitForSeconds(difficulty.CalcPlanetSpawnInterval());
            }
        }

        
    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(!gizmos) return;
            Gizmos.DrawCube(transform.position, spawnArea.bounds.size);
        }
    #endif
    }
}
