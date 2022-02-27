using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingLips : MonoBehaviour
{
    public float health = 100;

    public NavMeshAgent agent;
    public Transform target;
    public Animator anim;

    bool attacking = false;

    public GeneralManager genMan;
    public AudioSource kissSound;

    private void Start()
    {
        genMan = GameObject.FindGameObjectWithTag("generalManager").GetComponent<GeneralManager>();
        kissSound = GameObject.FindGameObjectWithTag("kissSound").GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("findEnemy", 0, 5);
    }

    public void gotHit(float healthToRemove)
    {
        health -= healthToRemove;
        if (health <= 0)
        {
            genMan.removeLips();
            genMan.addMoney(35);
            genMan.addScore();
            kissSound.Play();
            Destroy(gameObject);
        }
    }

    public void findEnemy()
    {
        target = FindClosestEnemy().transform.GetComponent<PinkElephant>().targetObj.transform;
    }

    private void Update()
    {
        if (target)
        {
            Vector3 targ = new Vector3(target.position.x, transform.position.y, target.position.z);

            if (!attacking)
            {
                agent.SetDestination(targ);
            }

            if (Vector3.Distance(transform.position, targ) < 10 && !attacking)
            {
                attacking = true;
                StartCoroutine(attackSequence());
            }
        }
    }

    public IEnumerator attackSequence()
    {
        GameObject toothBullet = Instantiate(Resources.Load("toothBullet"), transform.position, Quaternion.identity) as GameObject; //spawn bullet
        toothBullet.GetComponent<toothBulletScript>().target = target.gameObject;

        yield return new WaitForSeconds(2);
        attacking = false;
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("elephant");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
