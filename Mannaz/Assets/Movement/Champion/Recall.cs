using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Recall : MonoBehaviour
{
    public Vector3 recallPos;

    public GameObject recallVFXPrefab;

    public GameObject endRecallVFXPrefab;

    public GameObject idleVFXPrefab;

    Animator animator;

    public int recallCounter = 0;

    public NavMeshAgent doubleAgent;

    void Start()
    {
        GameObject theChamp = GameObject.Find("Champion");
        ChampionController champController = theChamp.GetComponent<ChampionController>();
        doubleAgent = champController.agent;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B) && recallCounter == 0)
        {
            recallCounter++;
            StartCoroutine(recallCoroutine());
        }

    }

    IEnumerator recallCoroutine()
    {
        const float waitTime = 8f;
        float counter = 0f;

        GameObject recallEffect = Instantiate(recallVFXPrefab, transform.position, transform.rotation);
        animator.SetTrigger("Recall");
        

        while (counter < waitTime)
        {

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(recallEffect.gameObject);
                recallCounter--;
                yield break;
            }

            counter += Time.deltaTime;
            yield return null; //Don't freeze Unity
        }
        doubleAgent.Warp(recallPos);
        animator.SetTrigger("EndRecall");
        Instantiate(endRecallVFXPrefab, transform.position, transform.rotation);
        recallCounter--;

    }


}
