using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toothBulletScript : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        if (target)
        {
            float step = 15 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("elephant"))
        {
            other.gameObject.SendMessage("gotHit", 15, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
