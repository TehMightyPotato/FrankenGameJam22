using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planets
{
    public class PlanetRotation : MonoBehaviour
    {
        [SerializeField] private float minRotationForce;

        [SerializeField] private float maxRotationForce;

        private float _actualRotationForce;
        private Vector3 _rotationVector;

        private void Start()
        {
            _actualRotationForce = Random.Range(minRotationForce, maxRotationForce);
            _rotationVector = new Vector3(Random.value, Random.value, Random.value);
        }

        private void Update()
        {
            transform.Rotate(_rotationVector, _actualRotationForce * Time.deltaTime);
        }
    }
}
