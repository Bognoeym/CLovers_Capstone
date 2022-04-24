using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    [SerializeField] private float viewAngle;  // 시야각(120도)
    [SerializeField] private float viewDistance;  // 시야 거리(10미터)
    [SerializeField] private LayerMask targetMask;  // 타켓 마스크(플레이어)

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
                float angle = Vector3.Angle(direction, transform.forward);  // NPC Ray와 플레이어 사이 각도

                if (angle < viewAngle * 0.5f)  // 시야 내에 있음
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up, direction, out hit, viewDistance))
                    {
                        if (hit.transform.tag == "Player")
                        {
                            Debug.Log("플레이어가 시야에 있음");
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
