using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class GameObjectContainer
    {
        public RearMirrorState state;
        
        public Image[] objectsToActivate;
        public Image[] objectsToDeactivate;

        public void Activate()
        {
            foreach (var gameObject in objectsToActivate)
            {
                gameObject.enabled = true;
            }
            foreach (var gameObject in objectsToDeactivate)
            {
                gameObject.enabled = false;
            }
        }

        public void Deactivate()
        {
            foreach (var gameObject in objectsToActivate)
            {
                gameObject.enabled = false;
            }
            foreach (var gameObject in objectsToDeactivate)
            {
                gameObject.enabled = true;
            }
        }
    }
}
