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
        //�̱�� �������� �ְ�, ���� �׳� �ٽ� ���ư�.
        if (score >= 1000)
        {
            // ���� ���� �޼��� ���
        }
        else
        {
            // ���� ���� �޼��� ���
        }

        StartCoroutine(DelayTime());


    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }

}
