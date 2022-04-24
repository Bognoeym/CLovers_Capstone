using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CountDown countDown;
    [SerializeField]
    private MoleSpawner moleSpawner;
    private int score;

    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    [field:SerializeField]
    public float MaxTime { private set; get; }
    public float CurrentTime { private set; get; }

    private void Start()
    {
        countDown.StartCountDown(GameStart);
    }

    private void GameStart()
    {
        moleSpawner.Setup();
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
        moleSpawner.isPause = false;
        //이기면 퍼즐조각 주고, 지면 그냥 다시 돌아감.
        if (score >= 1000)
        {
            // 게임 성공 메세지 출력
        }
        else
        {
            // 게임 실패 메세지 출력
        }

        StartCoroutine(DelayTime());


    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }

}
