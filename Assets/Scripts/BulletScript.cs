using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody BulletRB;
    [SerializeField] float BulletSpeed = 100;

    void Start()
    {
        BulletRB = GetComponent<Rigidbody>();
        BulletRB.velocity = transform.forward * BulletSpeed * Time.deltaTime; 
    }

    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }
}
