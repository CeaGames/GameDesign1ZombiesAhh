using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManu : MonoBehaviour
{
    public float mouseSensetivity;
    public Slider mouseSensetivitySlider;
    public TextMeshProUGUI mouseSensetivitySliderText;

    public float flooramount = 5;
    //public Slider floorAmountSlider;
    //public TextMeshProUGUI floorAmountSliderText;

    private void Start()
    {
        if (!SettingsCarrier.SettingsHaveStarted)
        {
            SettingsCarrier.SettingsHaveStarted = true;
            mouseSensetivity = 0.3f;
        }

        mouseSensetivitySlider.value = SettingsCarrier.mouseSensetivity /1000;
    }

    void Update()
    {
        // Update UI
        //floorAmountSliderText.text = floorAmountSlider.value.ToString();
        //flooramount = floorAmountSlider.value;
        mouseSensetivitySliderText.text = mouseSensetivitySlider.value.ToString("0.00");

        mouseSensetivity = mouseSensetivitySlider.value * 1000;

        SettingsCarrier.levelAmount = (int)flooramount -1;
        SettingsCarrier.mouseSensetivity = mouseSensetivity;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}