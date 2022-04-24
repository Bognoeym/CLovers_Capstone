using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNPC : MonoBehaviour
{
    [SerializeField] private float walkSpeed;

    private Vector3 direction;  // ����

    // ���� ����
    private bool isAction;  // �ൿ������ �Ǻ�
    private bool isWalking;

    [SerializeField] private float walkTime;  // �ȱ� �ð�
    [SerializeField] private float waitTime;  // ��� �ð�
    [SerializeField] private float danceTime;  // �� ��� �ð�
    private float currentTime;

    // �ʿ��� ������Ʈ
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
        int rand = Random.Range(0, 4);  // ���, �ȱ�, ��, ���̺�?

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
        Debug.Log("���");
    }

    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        Debug.Log("�ȱ�");
    }

    private void Dance()
    {
        isAction = true;
        currentTime = danceTime;
        anim.SetBool("Dance", isAction);
        anim.SetFloat("Dancing", Random.Range(0f, 1f));
        Debug.Log("���߱�");
    }
}
