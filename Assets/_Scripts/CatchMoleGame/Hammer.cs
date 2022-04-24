using System.Collections;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private float maxY;                     // ��ġ�� �ִ� y ��ġ
    [SerializeField]
    private float minY;                     // ��ġ�� �ּ� y ��ġ
    [SerializeField]
    private GameObject moleHitEffectPrefab; // �δ��� Ÿ�� ȿ�� prefab
    [SerializeField]
    private AudioClip audioClips;         // �δ����� Ÿ������ �� ����Ǵ� ����
    [SerializeField]
    private GameController gameController;  // ���� ������ ���� GameController
    [SerializeField]
    private objectDetector objectDetector;  // ���콺 Ŭ������ ������Ʈ ������ ���� objectDetector
    private MoleMovement MoleMovement;      // ��ġ ������Ʈ �̵��� ���� Movement
    private AudioSource audioSource;        // �δ����� Ÿ������ �� �Ҹ��� ����ϴ� AudioSource

    private void Awake()
    {
        MoleMovement = GetComponent<MoleMovement>();
        audioSource = GetComponent<AudioSource>();

        objectDetector.raycastEvent.AddListener(OnHit);
        audioSource.clip = audioClips;
    }

    private void OnHit(Transform target)
    {
        if (target.CompareTag("Mole"))
        {
            MoleFSM mole = target.GetComponent<MoleFSM>();

            // �δ����� Ȧ �ȿ� ���� ���� ���� �Ұ�
            if (mole.MoleState == MoleState.UnderGround) return;

            // ��ġ�� ��ġ ����
            transform.position = new Vector3(target.position.x, minY, target.position.z);

            // ��ġ�� �¾ұ� ������ �δ����� ���¸� �ٷ� "UnderGround"�� ����
            mole.ChangeState(MoleState.UnderGround);

            // ī�޶� ����
            ShakeCamera.Instance.OnShakeCamera(0.1f, 0.1f);

            // �δ��� Ÿ�� ȿ�� ���� (particle�� ������ �δ��� ����� �����ϰ� ����)
            GameObject clone = Instantiate(moleHitEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem.MainModule main = clone.GetComponent<ParticleSystem>().main;
            main.startColor = mole.GetComponent<MeshRenderer>().material.color;

            audioSource.Play();

            // ���� ���� (+50)
            gameController.Score += 50;

            // ��ġ�� ���� �̵���Ű�� �ڷ�ƾ ���
            StartCoroutine("MoveUp");
        }
    }

    private IEnumerator MoveUp()
    {
        // �̵����� : (0, 1, 0) [��]
        MoleMovement.MoveTo(Vector3.up);

        while (true)
        {
            if (transform.position.y >= maxY)
            {
                MoleMovement.MoveTo(Vector3.zero);

                break;
            }

            yield return null;
        }
    }

}
