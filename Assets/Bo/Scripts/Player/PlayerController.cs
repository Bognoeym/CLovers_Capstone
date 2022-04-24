using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject CamRig;
    CharacterController controller;

    Vector3 rig;
    Vector3 Axis;
    Vector3 MoveDirection;
    float H;
    float V;
    [SerializeField] float Gravity = 1f;
    [SerializeField] float JumpForce = 0.3f;
    [SerializeField] float characterSpeed = 2f;

    private Animator anim;

    private bool isDancing;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        CamRig = GameObject.Find("CameraRig");
        MoveDirection = Vector3.zero;
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            Debug.Log("isGrounded");
            MoveDirection.y = 0;

            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("Jump", true);
                MoveDirection.y = JumpForce;
            }
        }
        if (!controller.isGrounded)
        {
            anim.SetBool("Jump", false);

            MoveDirection.y -= Gravity * Time.deltaTime;
        }

        Dance();

        if(isDancing)
        {
            anim.SetBool("Dancing", isDancing);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            isDancing = false;
            anim.SetBool("Dancing", isDancing);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Dance()
    {
        if (isDancing)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetFloat("Dance", 0.1f);
            isDancing = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetFloat("Dance", 0.4f);
            isDancing = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetFloat("Dance", 0.7f);
            isDancing = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetFloat("Dance", 1f);
            isDancing = true;
        }
    }

    private void Move()
    {
        if (MoveDirection != Vector3.zero)
        {
            controller.Move(MoveDirection);
            //Debug.Log(MoveDirection);
        }

        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        rig = CamRig.transform.rotation.eulerAngles;
        //ī�޶�� �����ִ� ����
        rig = new Vector3(0, rig.y, 0);
        //��Ʈ�ѷ��κ��� �Է¹��� ���� ���� (2D����)
        Axis = new Vector3(H, 0, V);
        //Axis�� Rig.y������ŭ ȸ��. ȸ���� Axis�� ������ǥ��.
        Axis = Quaternion.Euler(rig) * Axis;
        Axis = Vector3.ClampMagnitude(Axis, 1.0f);
        MoveDirection.x = characterSpeed * Axis.x;
        MoveDirection.z = characterSpeed * Axis.z;
        anim.SetFloat("Speed", Axis.magnitude);

        if (!(H == 0 && V == 0))
        {
            //Axis�� ĳ������ ��ġ���� �����̵� �� ĳ���Ͱ� �ٶ󺸰� �����.
            transform.LookAt(Axis + transform.position);
        }

        //Follow cam to player
        CamRig.transform.position = Vector3.Lerp(CamRig.transform.position, this.transform.position + Vector3.up * 2.5f,
            Time.deltaTime * 10f);
    }
}
