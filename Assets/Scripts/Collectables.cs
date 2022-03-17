using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject explosedSphere;
    [SerializeField] private Rigidbody rb;

    private float thrust = .025f;
    private bool isPicked;
    #endregion

    private void FixedUpdate()
    {
        CollectablesPushing();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Area"))
        {
            isPicked = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Area"))
        {
            isPicked = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            rb.velocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Side"))
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void CollectablesPushing()
    {
        if (isPicked && PlayerController.Instance.IsStop)
        {
            rb.AddForce(0, 0, thrust, ForceMode.Impulse);
            Invoke("Explosion", 1);
        }
    }

    private void Explosion()
    {
        var explosionPos = transform.position;
        var power = 2f;
        var radius = 4.5f;
        sphere.SetActive(false);
        explosedSphere.SetActive(true);
        rb.AddExplosionForce(power, explosionPos, radius, 3);
        Invoke("ResetActivation", 1.5f);
    }

    private void ResetActivation()
    {
        gameObject.SetActive(false);
    }
}
