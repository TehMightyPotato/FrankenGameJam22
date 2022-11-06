using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class GameObjectContainer
    {
        public RearMirrorState state;
        
        public GameObject[] objectsToActivate;
        public GameObject[] objectsToDeactivate;

        public void Activate()
        {
            foreach (var gameObject in objectsToActivate)
            {
                gameObject.SetActive(true);
            }
            foreach (var gameObject in objectsToDeactivate)
            {
                gameObject.SetActive(false);
            }
        }

        public void Deactivate()
        {
            foreach (var gameObject in objectsToActivate)
            {
                gameObject.SetActive(false);
            }
            foreach (var gameObject in objectsToDeactivate)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
