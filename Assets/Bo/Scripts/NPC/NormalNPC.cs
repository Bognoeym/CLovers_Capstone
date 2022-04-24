using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNPC : MonoBehaviour
{
    [SerializeField] private float walkSpeed;

    private Vector3 direction;  // 방향

    // 상태 변수
    private bool isAction;  // 행동중인지 판별
    private bool isWalking;

    [SerializeField] private float walkTime;  // 걷기 시간
    [SerializeField] private float waitTime;  // 대기 시간
    [SerializeField] private float danceTime;  // 춤 대기 시간
    private float currentTime;

    // 필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;

    void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }

    void Update()
    {
        ElapseTime();
        Move();
        Rotation();
    }

    private void Move()
    {
        if(isWalking)
        {
            rigid.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
        }
    }

    private void Rotation()
    {
        if(isWalking)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigid.MoveRotation(Quaternion.Euler(rotation));
        }
    }

    private void ElapseTime()
    {
        if(isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                AnimReset();
        }
    }

    private void AnimReset()
    {
        isWalking = false;
        isAction = true;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Dance", false);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {
        int rand = Random.Range(0, 4);  // 대기, 걷기, 춤, 웨이브?

        switch(rand)
        {
            case 0:
                Wait();
                break;
            case 1:
                TryWalk();
                break;
            case 2:
                Dance();
                break;
        }
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }

    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        Debug.Log("걷기");
    }

    private void Dance()
    {
        isAction = true;
        currentTime = danceTime;
        anim.SetBool("Dance", isAction);
        anim.SetFloat("Dancing", Random.Range(0f, 1f));
        Debug.Log("춤추기");
    }
}
