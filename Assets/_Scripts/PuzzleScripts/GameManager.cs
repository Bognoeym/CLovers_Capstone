using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CountDown countDown;
    public GameObject block1, block2, block3, block4, block5;
    private bool b1, b2, b3, b4, b5;

    [field: SerializeField]
    public float MaxTime { private set; get; }
    public float CurrentTime { private set; get; }
    // Start is called before the first frame update
    void Start()
    {

        countDown.StartCountDown(GameStart);
    }

    private void GameStart()
    {
        StartCoroutine("OnTimeCount");
    }

    private IEnumerator OnTimeCount()
    {
        CurrentTime = MaxTime;

        while (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;

            yield return null;
        }
        GameSet();
    }
    private void GameSet()
    {
        //이기면 퍼즐조각 주고, 지면 그냥 다시 돌아감.
        if (isClear())
        {
            Debug.Log("성공");
            // 게임 성공 메세지 출력
        }
        else
        {

            Debug.Log("실패");
            // 게임 실패 메세지 출력
        }

        StartCoroutine(DelayTime());
    }

    private bool isClear()
    {
        if ((block1.transform.position.x >= -160 && block1.transform.position.x <= -140) && (block1.transform.position.y >= -60 && block1.transform.position.y <= -40))
        {
            b1 = true;
        }
        if ((block2.transform.position.x >= -10 && block2.transform.position.x <= 10) && (block2.transform.position.y >= 90 && block2.transform.position.y <= 110))
        {
            b2 = true;
        }
        if ((block3.transform.position.x >= -10 && block3.transform.position.x <= 10) && (block3.transform.position.y >= -110 && block3.transform.position.y <= -90))
        {
            b3 = true;
        }
        if ((block4.transform.position.x >= 140 && block4.transform.position.x <= 160) && (block4.transform.position.y >= -110 && block4.transform.position.y <= -90))
        {
            b4 = true;
        }
        if ((block5.transform.position.x >= 140 && block5.transform.position.x <= 160) && (block5.transform.position.y >= 140 && block5.transform.position.y <= 160))
        {
            b5 = true;
        }

        if ((b1 == true) && (b2 == true) && (b3 == true) && (b4 == true) && (b5 == true))
            return true;
        return false;
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }
}
