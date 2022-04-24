using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    [SerializeField] private float viewAngle;  // �þ߰�(120��)
    [SerializeField] private float viewDistance;  // �þ� �Ÿ�(10����)
    [SerializeField] private LayerMask targetMask;  // Ÿ�� ����ũ(�÷��̾�)

    private PlayerController thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        
    }

    public Vector3 GetTargetPos()
    {
        print(thePlayer.name);
        return thePlayer.transform.position;
    }

    public bool View()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if(_targetTf.name == "Player")
            {

                Vector3 direction = (_targetTf.position - transform.position).normalized;
                float angle = Vector3.Angle(direction, transform.forward);  // NPC Ray�� �÷��̾� ���� ����

                if (angle < viewAngle * 0.5f)  // �þ� ���� ����
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up, direction, out hit, viewDistance))
                    {
                        if (hit.transform.tag == "Player")
                        {
                            Debug.Log("�÷��̾ �þ߿� ����");
                            Debug.DrawRay(transform.position + transform.up, direction, Color.blue);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
