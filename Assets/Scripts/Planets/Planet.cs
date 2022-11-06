using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using Planets.Needs;
using Shooting;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Planets
{
    public class Planet : MonoBehaviour
    {
        [Separator("Component References")]
        [SerializeField] private Rigidbody ownRigidbody;
        [SerializeField] private List<GameObject> planetPrefabs;
        [SerializeField] private NeedManager needManager;
        [Separator("Object References")]
        [SerializeField] private GameObject uiElementPrefab;
        [SerializeField] private GameObject uiContainer;
        [SerializeField] private GameObject visualisation;
        [Separator("SO References")]
        [SerializeField] private Difficulty.Difficulty difficulty;
        [SerializeField] private ScoreHandler scoreHandler;

        [Separator("Planet events")]
        [SerializeField]
        private PlanetHitHandler planetHitHandler;
        [Separator("Values")]
        [SerializeField] private float minScale;
        [SerializeField] private float maxScale;
        [SerializeField] private Vector3 initialVelocity;

        private List<Need> needs;
        private Dictionary<Need, GameObject> spriteDict;

        public IReadOnlyList<Need> Needs => needs;

        private void Awake()
        {
            var obj = Instantiate(planetPrefabs.GetRandom(),transform.position, Quaternion.identity, visualisation.transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            visualisation.transform.localScale *= Random.Range(minScale, maxScale);
        }

        private void Start()
        {
            ownRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
            GenerateNeeds();
        }

        private void GenerateNeeds()
        {
            var count = difficulty.CalcPlanetNeedsCount();
            needs = new List<Need>();
            spriteDict = new Dictionary<Need, GameObject>();
            needManager.GetRandomNeeds(count, ref needs);
            foreach (var need in needs)
            {
                var obj = Instantiate(uiElementPrefab, uiContainer.transform);
                spriteDict.Add(need,obj);
                var image = obj.GetComponent<Image>();
                if (need.sprite != null)
                {
                    image.sprite = need.sprite;
                }
            }
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Projectile")) return;
            if (other.gameObject.TryGetComponent<Projectile>(out var projectile))
            {
                planetHitHandler.ProjectileEntered(this, projectile);

                var result = needs.FirstOrDefault(x => x.needKind == projectile.shotKind);
                if (result != null)
                {
                    Destroy(spriteDict[result]);
                    scoreHandler.PlanetHitCorrect();
                    needs.Remove(result);
                }
                else
                {
                    scoreHandler.PlanetHitWrong();
                }
            }

            if (needs.Count <= 0)
            {
                scoreHandler.PlanetFinished();
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
