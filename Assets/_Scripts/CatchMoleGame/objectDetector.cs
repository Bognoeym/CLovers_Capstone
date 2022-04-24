using UnityEngine;
using UnityEngine.Events;

public class objectDetector : MonoBehaviour
{
    [System.Serializable]
    public class RaycastEvent : UnityEvent<Transform> { }   // 이벤트 클래스 정의

    [HideInInspector]
    public RaycastEvent raycastEvent = new RaycastEvent();      // 이벤트 클래스 인스턴스 생성 및 메모리 할당

    private Camera mainCamera;                                  // 광선을 생성하기 위한 Camera
    private Ray ray;                                            // 생성된 광선 정보 저장을 위한 Ray
    private RaycastHit hit;                                     // 광선에 부딪힌 오브젝트 정보 저장을 위한 Raycasthit

    private void Awake()
    {
        // "MainCamera" 태그를 가지고 있는 오브젝트 탐색 후 Camera 컴포넌트 정보 전달
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼을 눌렀을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 카메라 위치에서 화면의 마우스 위치를 관통하는 광선 생성
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 광선에 부딪히는 오브젝트를 검출해서 hit에 저장
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 부딪힌 오브젝트의 Transform 정보를 매개변수로 이벤트 호출
                raycastEvent.Invoke(hit.transform);
            }
        }
    }
}
