using System;
using Planets.Needs;
using UnityEngine;

namespace Shooting
{
    public class Projectile : MonoBehaviour
    {
        public NeedKind shotKind;
        [SerializeField] private float turnSpeed;
        [SerializeField] private GameObject rendererObject;

        private void Update()
        {
            var eulerAngles = rendererObject.transform.eulerAngles;
            eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + turnSpeed * Time.deltaTime);
            rendererObject.transform.eulerAngles = eulerAngles;
        }
    }
}
