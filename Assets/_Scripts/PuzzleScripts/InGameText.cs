using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameText : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TextMeshProUGUI textPlayTime;
    [SerializeField]
    private Slider sliderPlayTime;

    private void Update()
    {
        textPlayTime.text = gameManager.CurrentTime.ToString("F1");
        sliderPlayTime.value = gameManager.CurrentTime / gameManager.MaxTime;
    }
}
