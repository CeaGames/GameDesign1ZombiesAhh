using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManu : MonoBehaviour
{
    public float mouseSensetivity = 0.1f;
    public Slider mouseSensetivitySlider;
    public TextMeshProUGUI mouseSensetivitySliderText;

    public float flooramount = 5;
    public Slider floorAmountSlider;
    public TextMeshProUGUI floorAmountSliderText;

    void Update()
    {
        // Update UI
        floorAmountSliderText.text = floorAmountSlider.value.ToString();
        flooramount = floorAmountSlider.value;
        mouseSensetivitySliderText.text = mouseSensetivitySlider.value.ToString("0.00");
        mouseSensetivity = mouseSensetivitySlider.value * 1000;

        SettingsCarrier.levelAmount = (int)flooramount -1;
        SettingsCarrier.mouseSensetivity = mouseSensetivity;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}