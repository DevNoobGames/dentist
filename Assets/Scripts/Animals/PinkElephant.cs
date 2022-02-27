using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PinkElephant : MonoBehaviour
{
    public Vector3 midPoint;
    public float walkRadius = 20;
    public NavMeshAgent agent;

    public float health = 100;

    public GameObject targetObj;
    public AudioSource elephantDedAudio;

    void Start()
    {
        float randomSpeed = Random.Range(2f, 4f);
        agent.speed = randomSpeed;

        midPoint = transform.position;
        agent = GetComponent<NavMeshAgent>();
        float renew = Random.Range(10, 20);
        //InvokeRepeating("newTarget", 0, renew);

        elephantDedAudio = GameObject.FindGameObjectWithTag("elephantScream").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 3)
        {
            newTarget();
        }
    }

    public void gotHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("generalManager").GetComponent<GeneralManager>().changeElephantAmount(-1);
            //Does not get removed from list yet?!
            elephantDedAudio.Play();
            Destroy(gameObject);
        }
    }

    public void newTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        //agent.SetDestination(randomDirection + midPoint);
        agent.SetDestination(new Vector3(randomDirection.x, 0, randomDirection.z) + midPoint);
    }
}
