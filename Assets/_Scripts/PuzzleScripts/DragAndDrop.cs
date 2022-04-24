using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    void OnMouseDrag()
    {
        //���콺 ��ǥ�� �޾ƿ´�.
        Vector3 mousePosition
            = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 280);
        //���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

}
