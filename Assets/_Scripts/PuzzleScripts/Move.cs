using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject mother;
    void OnMouseDrag()
    {
        //���콺 ��ǥ�� �޾ƿ´�.
        Vector3 mousePosition
            = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 280);
        //���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
        mother.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

}
