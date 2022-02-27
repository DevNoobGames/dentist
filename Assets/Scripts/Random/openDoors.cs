using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoors : MonoBehaviour
{
    public Animation openDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openDoor["HygieneOpenDoors"].speed = 1;
            openDoor.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openDoor["HygieneOpenDoors"].speed = -1;
            openDoor.Play();
        }
    }
}
