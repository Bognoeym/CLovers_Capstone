using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransMap : MonoBehaviour
{
    public string transferMapName;  //이동할 맵 이름

    private void OnTriggerStay(Collider point)
    {
        if (point.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                SceneManager.LoadScene(transferMapName);
            }
        }
    }
}
