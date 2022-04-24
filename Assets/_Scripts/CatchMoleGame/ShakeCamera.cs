using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    // 싱글톤 처리를 위한 instance 변수 선언
    private static ShakeCamera instance;
    // 외부에서 Get 접근만 가능하도록 Instance property 선언
    public static ShakeCamera Instance => instance;

    private float shakeTime;
    private float shakeIntensity;

    public ShakeCamera()
    {
        instance = this;
    }

    // 외부에서 카메라 흔들림을 조작할 때 호출하는 메소드
    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)      //1.0초간 0.1세기로 흔들림
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");
    }

    // 카메라를 shakeTime동안 shakeIntensity의 세기로 흔드는 코루틴 함수
    private IEnumerator ShakeByPosition()
    {
        // 흔들리기 직전의 시작 위치 (흔들림 종료 후 돌아올 위치)
        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            // 초기 위치로부터 구 범위(size 1) * shakeIntensity의 범위안에서 카메라 위치 변동
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            // 시간 감소
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        transform.position = startPosition;
    }

}
