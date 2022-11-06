using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD;
using FMOD.Studio;
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
        
        public FMODUnity.EventReference DubstepEvent;
        public FMODUnity.EventReference HitEvent;
        public FMODUnity.EventReference TranceEvent;

        public FMODUnity.EventReference AtzeEvent;
        public FMODUnity.EventReference SecurityEvent;
        public FMODUnity.EventReference DjEvent;

        public FMODUnity.EventReference GlassShatterEvent;

        [SerializeField]
        public Vector3 ListenerPosition;

        private FMOD.Studio.EventInstance dubstepInstance;
        private FMOD.Studio.EventInstance hitInstance;
        private FMOD.Studio.EventInstance tranceInstance;

        private FMOD.Studio.EventInstance atzeInstance;
        private FMOD.Studio.EventInstance securityInstance;
        private FMOD.Studio.EventInstance djInstance;

        private FMOD.Studio.EventInstance glassShatterInstance;

        private List<Need> needs;
        private Dictionary<Need, GameObject> spriteDict;

        public IReadOnlyList<Need> Needs => needs;

        private void Awake()
        {
            var obj = Instantiate(planetPrefabs.GetRandom(),transform.position, Quaternion.identity, visualisation.transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            visualisation.transform.localScale *= Random.Range(minScale, maxScale);
        }

        private void Update()
        {
            dubstepInstance.setParameterByName("Distance", Vector3.Distance(ListenerPosition, transform.position));
            hitInstance.setParameterByName("Distance", Vector3.Distance(ListenerPosition, transform.position));
            tranceInstance.setParameterByName("Distance", Vector3.Distance(ListenerPosition, transform.position));

            atzeInstance.setParameterByName("Distance", Vector3.Distance(ListenerPosition, transform.position));
            securityInstance.setParameterByName("Distance", Vector3.Distance(ListenerPosition, transform.position));
            djInstance.setParameterByName("Distance", Vector3.Distance(ListenerPosition, transform.position));

            glassShatterInstance.setParameterByName("Distance", Vector3.Distance(ListenerPosition, transform.position));
        }

        private void Start()
        {
            dubstepInstance = FMODUnity.RuntimeManager.CreateInstance(DubstepEvent);
            hitInstance = FMODUnity.RuntimeManager.CreateInstance(HitEvent);
            tranceInstance = FMODUnity.RuntimeManager.CreateInstance(TranceEvent);

            atzeInstance = FMODUnity.RuntimeManager.CreateInstance(AtzeEvent);
            securityInstance = FMODUnity.RuntimeManager.CreateInstance(SecurityEvent);
            djInstance = FMODUnity.RuntimeManager.CreateInstance(DjEvent);

            glassShatterInstance = FMODUnity.RuntimeManager.CreateInstance(GlassShatterEvent);

            ownRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
            GenerateNeeds();
        }

        private void OnDestroy()
        {
            dubstepInstance.stop(STOP_MODE.IMMEDIATE);
            hitInstance.stop(STOP_MODE.IMMEDIATE);
            tranceInstance.stop(STOP_MODE.IMMEDIATE);

            atzeInstance.stop(STOP_MODE.IMMEDIATE);
            securityInstance.stop(STOP_MODE.IMMEDIATE);
            djInstance.stop(STOP_MODE.IMMEDIATE);

            glassShatterInstance.stop(STOP_MODE.IMMEDIATE);

            dubstepInstance.clearHandle();
            hitInstance.clearHandle();
            tranceInstance.clearHandle();

            atzeInstance.clearHandle();
            securityInstance.clearHandle();
            djInstance.clearHandle();

            glassShatterInstance.clearHandle();
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

                    switch (projectile.shotKind)
                    {
                        case NeedKind.Music1:
                            dubstepInstance.start();
                            dubstepInstance.release();
                            break;
                        case NeedKind.Music2:
                            hitInstance.start();
                            hitInstance.release();
                            break;
                        case NeedKind.Music3:
                            tranceInstance.start();
                            tranceInstance.release();
                            break;
                        case NeedKind.Person1:
                            atzeInstance.start();
                            atzeInstance.release();
                            break;
                        case NeedKind.Person2:
                            djInstance.start();
                            djInstance.release();
                            break;
                        case NeedKind.Person3:
                            securityInstance.start();
                            securityInstance.release();
                            break;
                        case NeedKind.Drink1:
                        case NeedKind.Drink2:
                        case NeedKind.Drink3:
                            glassShatterInstance.start();
                            glassShatterInstance.release();
                            break;
                    }

                    MusicManager.Instance.SetMusicIntensity(0);
                }
                else
                {
                    scoreHandler.PlanetHitWrong();
                }
            }

            if (needs.Count <= 0)
            {
                scoreHandler.PlanetFinished();

                var curVelocity = ownRigidbody.velocity;

                var moveVector = curVelocity + new Vector3((curVelocity.x > 0 ? 1 : -1) * Random.Range(1, 30), 0,
                    (curVelocity.z > 0 ? 1 : -1) * Random.Range(1, 30));
                ownRigidbody.AddForce(moveVector, ForceMode.Impulse);
            }
            Destroy(other.gameObject);
        }
    }
}
