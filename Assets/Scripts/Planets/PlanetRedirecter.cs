using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRedirecter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Planet")) return;

        var planetGameObject = other.gameObject.transform.parent.gameObject;

        var planetRigidbody = planetGameObject.GetComponent<Rigidbody>();

        var curVelocity = planetRigidbody.velocity;

        var moveVector = curVelocity + new Vector3((curVelocity.x > 0 ? 1 : -1) * Random.Range(1, 30), 0,
            (curVelocity.z > 0 ? 1 : -1) * Random.Range(1, 30));
        planetRigidbody.AddForce(moveVector, ForceMode.Impulse);
    }
}
