using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class PlanetDestroyer : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Planet")) return;
            
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
