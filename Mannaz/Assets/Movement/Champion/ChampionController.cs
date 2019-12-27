using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChampionController : MonoBehaviour
{
    public Camera mainCamera;
    public NavMeshAgent agent;
    private Vector3 recallPosition;
    public Transform track;
    private Transform cachedTransform;
    private Vector3 cachedPosition;
    public int recallCounter = 0;

    void Start()
    {
        cachedTransform = GetComponent<Transform>();
        if (track)
        {
            cachedPosition = track.position;
        }
        recallPosition = cachedPosition;
    }

    // Update is called once per frame
    void Update()
    {   

        if (track && cachedPosition != track.position)
        {
            cachedPosition = track.position;
            transform.position = cachedPosition;
        }

        if (Input.GetKeyUp(KeyCode.B) && recallCounter == 0)
        {
            agent.SetDestination(cachedPosition);
            recallCounter++;
            StartCoroutine(homeCoroutine());
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

    }

    IEnumerator homeCoroutine()
    {

        const float waitTime = 8f;
        float counter = 0f;

        while (counter < waitTime)
        {

            if (Input.GetMouseButtonDown(1))
            {
                recallCounter--;
                yield break;
            }

            counter += Time.deltaTime;
            yield return null; //Don't freeze Unity
        }
        recallCounter--;
        agent.ResetPath();
    }

    public void TakeDamage(int amount)
    {
        //health -= amount;
    }

}
