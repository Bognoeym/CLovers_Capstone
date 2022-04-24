using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private GameObject[] HandsPoint;
    [SerializeField] private GameObject itemParent;

    //Test
    [SerializeField] private float radius = 4f;  // 인식 범위
    [SerializeField] private LayerMask layerMask;

    private int left = 0, right = 1;
    private bool[] getHands;
    private GameObject[] getItem;

    void Start()
    {
        getHands = new bool[2];
        getItem = new GameObject[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!getHands[left])
                CheckItem(left);
            else
                PutItem(left);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!getHands[right])
                CheckItem(right);
            else
                PutItem(right);
        }
    }

    private void CheckItem(int hand)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log(hitColliders[i].gameObject.name);

            //
            hitColliders[i].GetComponent<Rigidbody>().isKinematic = true;
            hitColliders[i].GetComponent<BoxCollider>().enabled = false;
            
            getHands[hand] = true;
            getItem[hand] = hitColliders[i].gameObject;
            hitColliders[i].transform.SetParent(HandsPoint[hand].transform, true);
            hitColliders[i].transform.localPosition = Vector3.zero;
            //

            return;
        }
    }

    private void PutItem(int hand)
    {
        getHands[hand] = false;
        getItem[hand].GetComponent<Rigidbody>().isKinematic = false;
        getItem[hand].GetComponent<BoxCollider>().enabled = true;
        getItem[hand].transform.parent = null;
        getItem[hand].transform.SetParent(itemParent.transform, true);

        getItem[hand] = null;
    }
}