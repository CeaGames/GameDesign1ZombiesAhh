using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUIController : MonoBehaviour
{
    public float mouseSensetivity;
    public Slider mouseSensitivitySlider;
    public TextMeshProUGUI mouseSensitivitySliderText;


    private void Start()
    {
        if (!SettingsCarrier.SettingsHaveStarted)
        {
            SettingsCarrier.SettingsHaveStarted = true;
            mouseSensetivity = 0.3f;
        }

        mouseSensitivitySlider.value = SettingsCarrier.mouseSensetivity / 1000;
    }

    void Update()
    {
        // Update UI
        //floorAmountSliderText.text = floorAmountSlider.value.ToString();
        //flooramount = floorAmountSlider.value;
        mouseSensitivitySliderText.text = mouseSensitivitySlider.value.ToString("0.00");

        mouseSensetivity = mouseSensitivitySlider.value * 1000;
        SettingsCarrier.mouseSensetivity = mouseSensetivity;
    }
}
