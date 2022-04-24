using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadNPC : MonoBehaviour
{
    [SerializeField] private float runSpeed;  // 속도

    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private FieldOfViewAngle fieldOfViewAngle;

    private bool isChasing;  // 추격 중인지

    private float currentTime;
    private float maxTime = 4f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fieldOfViewAngle = GetComponent<FieldOfViewAngle>();
    }

    void Update()
    {
        if (fieldOfViewAngle.View() && !isChasing)
        {
            Chase();
        }

        if(isChasing)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= maxTime)
            {
                isChasing = false;
                anim.SetBool("Chasing", isChasing);
            }
            else
                navMeshAgent.SetDestination(fieldOfViewAngle.GetTargetPos());
        }
    }

    private void Chase()
    {
        isChasing = true;
        navMeshAgent.speed = runSpeed;
        anim.SetBool("Chasing", isChasing);
        currentTime = 0;
    }
}
