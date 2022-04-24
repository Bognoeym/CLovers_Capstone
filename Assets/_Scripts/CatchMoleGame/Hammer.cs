using System.Collections;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private float maxY;                     // 망치의 최대 y 위치
    [SerializeField]
    private float minY;                     // 망치의 최소 y 위치
    [SerializeField]
    private GameObject moleHitEffectPrefab; // 두더지 타격 효과 prefab
    [SerializeField]
    private AudioClip audioClips;         // 두더지를 타격했을 때 재생되는 사운드
    [SerializeField]
    private GameController gameController;  // 점수 증가를 위한 GameController
    [SerializeField]
    private objectDetector objectDetector;  // 마우스 클릭으로 오브젝트 선택을 위한 objectDetector
    private MoleMovement MoleMovement;      // 망치 오브젝트 이동을 위한 Movement
    private AudioSource audioSource;        // 두더지를 타격했을 때 소리를 재생하는 AudioSource

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

            // 두더지가 홀 안에 있을 때는 공격 불가
            if (mole.MoleState == MoleState.UnderGround) return;

            // 망치의 위치 설정
            transform.position = new Vector3(target.position.x, minY, target.position.z);

            // 망치에 맞았기 때문에 두더지의 상태를 바로 "UnderGround"로 설정
            mole.ChangeState(MoleState.UnderGround);

            // 카메라 흔들기
            ShakeCamera.Instance.OnShakeCamera(0.1f, 0.1f);

            // 두더지 타격 효과 생성 (particle의 색상을 두더지 색상과 동일하게 설정)
            GameObject clone = Instantiate(moleHitEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem.MainModule main = clone.GetComponent<ParticleSystem>().main;
            main.startColor = mole.GetComponent<MeshRenderer>().material.color;

            audioSource.Play();

            // 점수 증가 (+50)
            gameController.Score += 50;

            // 망치를 위로 이동시키는 코루틴 재생
            StartCoroutine("MoveUp");
        }
    }

    private IEnumerator MoveUp()
    {
        // 이동방향 : (0, 1, 0) [위]
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
