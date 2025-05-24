using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    //make the list that holds items
    public List<string> items = new List<string>();

    //range of pickup
    public float range;

    //a layermask so you can only pickup items and not walls or zombies
    public LayerMask itemLayerMask;

    //a layermask to know when you are lookin at a doorframe that can be barricaded
    public LayerMask doorFrameLayerMask;

    //a layermask for bonking the naughty zombies
    public LayerMask zombieLayerMask;

    //the first person camera needed to center the ray
    public Camera gunCamera;

    // UI displaying the amount of planks
    public Text _numberOfPlanksText;

    // UI stuff (turial if you will)
    [SerializeField] private TMP_Text _actionText;
    [SerializeField] private Image _leftClickImage;
    [SerializeField] private Image _rightClickImage;

    [SerializeField] int itemHoldingLimit = 3;

    private void Start()
    {
        UpdatePlankUI();
        _leftClickImage.gameObject.SetActive(false);
        _rightClickImage.gameObject.SetActive(false);
        _actionText.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateTutorialUI();

        if (Input.GetButtonDown("Fire2"))
        {
            Shooting();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Bonking();
        }
    }

    private void UpdateTutorialUI()
    {
        //zombie kill ui
        RaycastHit bonk;
        RaycastHit pickup;
        RaycastHit barricade;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out bonk, range, zombieLayerMask))
        {
            _leftClickImage.gameObject.SetActive(true);
            _rightClickImage.gameObject.SetActive(false);
            _actionText.gameObject.SetActive(true);
            _actionText.text = "Kill with plank";
        }
        else if ((Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out pickup, range, itemLayerMask)) && (items.Count < itemHoldingLimit))
        {
            _leftClickImage.gameObject.SetActive(false);
            _rightClickImage.gameObject.SetActive(true);
            _actionText.gameObject.SetActive(true);
            _actionText.text = "Pick up";
        }
        else if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out barricade, range, doorFrameLayerMask))
        {
            _leftClickImage.gameObject.SetActive(false);
            _rightClickImage.gameObject.SetActive(true);
            _actionText.gameObject.SetActive(true);
            _actionText.text = "Place plank";
        }
        else
        {
            _leftClickImage.gameObject.SetActive(false);
            _rightClickImage.gameObject.SetActive(false);
            _actionText.gameObject.SetActive(false);
            _actionText.text = "";
        }

        
    }

    // shoot and if hit give the name in the console log, add it to your list and destroy the game object after that
    private void Bonking()
    {
        RaycastHit bonk;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out bonk, range, zombieLayerMask))
        {
            if (bonk.transform != null && items.Count > 0)
            {
                items.Remove(items[0]);
                UpdatePlankUI();

                if (bonk.transform.gameObject.GetComponent<WeepingAngelZombie>() != null)
                {
                    bonk.transform.gameObject.GetComponent<WeepingAngelZombie>().Die();
                }
                else
                {
                    bonk.collider.gameObject.SetActive(false);
                }
            }
        }
    }

    //it also makes you put planks on doorframes if you point at doorframes
    private void Shooting()
    {
        RaycastHit pickup;
        if ((Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out pickup, range, itemLayerMask)) && (items.Count < itemHoldingLimit))
        {
            Debug.Log(pickup.transform.name);
            items.Add(pickup.transform.gameObject.name);
            pickup.collider.gameObject.SetActive(false);
            UpdatePlankUI();
        }

        RaycastHit barricade;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out barricade, range, doorFrameLayerMask))
        {
            if (barricade.transform != null && items.Count > 0 && barricade.transform.GetComponentInChildren<zombieDetect>().zombieCount == 0)
            {
                items.Remove(items[0]);
                barricade.collider.gameObject.GetComponent<DoorBarricade>().hp += barricade.collider.gameObject.GetComponent<DoorBarricade>().maxHp / 3;
                UpdatePlankUI();
            }
        }
    }
    private void UpdatePlankUI()
    {
        _numberOfPlanksText.text = "Planks:\n" + items.Count + " / " +itemHoldingLimit;
    }

}
