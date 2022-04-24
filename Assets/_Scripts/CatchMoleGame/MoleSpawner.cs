using System.Collections;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [SerializeField]
    private MoleFSM[] moles;        // �ʿ� �����ϴ� �δ�����
    [SerializeField]
    private float spawnTime;        // �δ��� ���� �ֱ�
    public bool isPause;

    private void Start()
    {
        isPause = true;
    }
    public void Setup()
    {
        StartCoroutine("SpawnMole");
    }

    private IEnumerator SpawnMole()
    {
        while (isPause)
        {
            // 0 ~ Moles.Length - 1 �� ������ ���� ����
            int index = Random.Range(0, moles.Length);
            // index��° �δ����� ���¸� "MoveUp"���� ����
            moles[index].ChangeState(MoleState.MoveUp);

            // spawnTime �ð����� ���
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
