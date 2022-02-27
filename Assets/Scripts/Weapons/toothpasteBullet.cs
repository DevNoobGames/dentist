using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toothpasteBullet : MonoBehaviour
{
    public Rigidbody rb;
    public bool sendPain = true;
    public ParticleSystem particles;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 10);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;

        if (other.CompareTag("lips"))
        {
            other.GetComponent<WalkingLips>().removeHealth(25);
            Destroy(gameObject);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        //particles.Play();

        if (collision.gameObject.CompareTag("lips") && sendPain)
        {
            collision.gameObject.SendMessage("gotHit", 25, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        else
        {
            sendPain = false;
        }
    }
}
