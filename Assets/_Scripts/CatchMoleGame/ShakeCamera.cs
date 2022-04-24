using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    // �̱��� ó���� ���� instance ���� ����
    private static ShakeCamera instance;
    // �ܺο��� Get ���ٸ� �����ϵ��� Instance property ����
    public static ShakeCamera Instance => instance;

    private float shakeTime;
    private float shakeIntensity;

    public ShakeCamera()
    {
        instance = this;
    }

    // �ܺο��� ī�޶� ��鸲�� ������ �� ȣ���ϴ� �޼ҵ�
    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)      //1.0�ʰ� 0.1����� ��鸲
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");
    }

    // ī�޶� shakeTime���� shakeIntensity�� ����� ���� �ڷ�ƾ �Լ�
    private IEnumerator ShakeByPosition()
    {
        // ��鸮�� ������ ���� ��ġ (��鸲 ���� �� ���ƿ� ��ġ)
        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            // �ʱ� ��ġ�κ��� �� ����(size 1) * shakeIntensity�� �����ȿ��� ī�޶� ��ġ ����
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            // �ð� ����
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        transform.position = startPosition;
    }

}
