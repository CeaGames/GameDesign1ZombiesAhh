using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    //make the list that holds items
    public List<string> items = new List<string>();

    //range of pickup
    public float range;

    //a layermask so you can only pickup items and not walls or zombies
    public LayerMask itemLayerMask;
    public LayerMask medkitLayerMask;

    //a layermask to know when you are lookin at a doorframe that can be barricaded
    public LayerMask doorFrameLayerMask;

    //a layermask for bonking the naughty zombies
    public LayerMask zombieLayerMask;

    public LayerMask drawerLayerMask;

    //the first person camera needed to center the ray
    public Camera gunCamera;

    // UI displaying the amount of planks
    public Text _numberOfPlanksText;

    //healing
    public PlayerHealth _playerHealth;

    // UI stuff (turial if you will)
    [SerializeField] private TMP_Text _actionText;
    [SerializeField] private Image _leftClickImage;
    [SerializeField] private Image _rightClickImage;

    [SerializeField] int itemHoldingLimit = 3;

    [SerializeField] private AudioClip HitZ;
    [SerializeField] private AudioClip OpenChest;
    [SerializeField] private AudioClip Backpack;
    [SerializeField] private AudioClip Barricade;
    [SerializeField] private AudioClip Pickup;
    private AudioSource audioSource;
    //these add the audio clips

    private void Start()
    {
        UpdatePlankUI();
        _leftClickImage.gameObject.SetActive(false);
        _rightClickImage.gameObject.SetActive(false);
        _actionText.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        UpdateTutorialUI();

        if (Input.GetButtonDown("Fire2"))
        {
            PickingUpOrPlacingPlanks();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Bonking();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Opening();
        }
    }

    private void UpdateTutorialUI()
    {
        //zombie kill ui
        RaycastHit bonk;
        RaycastHit pickup;
        RaycastHit barricade;
        RaycastHit open;
        RaycastHit medkit;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out bonk, range, zombieLayerMask) && items.Count > 0)
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
        else if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out barricade, range, doorFrameLayerMask) && items.Count > 0)
        {
            _leftClickImage.gameObject.SetActive(false);
            _rightClickImage.gameObject.SetActive(true);
            _actionText.gameObject.SetActive(true);
            _actionText.text = "Place plank";
        }
        else if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out open, range, drawerLayerMask))
        {
            _leftClickImage.gameObject.SetActive(false);
            _rightClickImage.gameObject.SetActive(false);
            _actionText.gameObject.SetActive(true);
            _actionText.text = "E to open";
        }
        else if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out medkit, range, medkitLayerMask))
        {
            _leftClickImage.gameObject.SetActive(false);
            _rightClickImage.gameObject.SetActive(false);
            _actionText.gameObject.SetActive(true);
            _actionText.text = "E to HEAL";
        }
        else
        {
            _leftClickImage.gameObject.SetActive(false);
            _rightClickImage.gameObject.SetActive(false);
            _actionText.gameObject.SetActive(false);
            _actionText.text = "";
        }
    }

    private void Opening()
    {
        RaycastHit open;
        RaycastHit medkit;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out open, range, drawerLayerMask))
        {
            Debug.Log("opening");
            // Try to get the Drawer component from the 'open' object
            Drawer drawer = open.collider.GetComponent<Drawer>();
            audioSource.clip = OpenChest;
            audioSource.Play();
            Debug.Log("opening");
            if (drawer != null)
            {
                // Toggle the drawerOpen field
                drawer.drawerOpen = !drawer.drawerOpen;
            }
        }
        //Consume Healing
        else if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out medkit, range, medkitLayerMask))
        {
            HasItemScript hasItemScript = medkit.transform.parent.GetComponent<HasItemScript>();
            hasItemScript.HasItem = false;

            audioSource.clip = Backpack;
            audioSource.Play();
            _playerHealth.currentHealth = _playerHealth.currentHealth + 30;
            _playerHealth.currentHealth = Mathf.Clamp(_playerHealth.currentHealth, 0, _playerHealth.maxHealth);

            Debug.Log(_playerHealth.currentHealth);
            _playerHealth.UpdateVisuals();
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
                audioSource.clip = HitZ;
                audioSource.Play();
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
    private void PickingUpOrPlacingPlanks()
    {
        RaycastHit pickup;
        if ((Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out pickup, range, itemLayerMask)) && (items.Count < itemHoldingLimit))
        {
            audioSource.clip = Pickup;
            audioSource.Play();
            Debug.Log(pickup.transform.name);
            items.Add(pickup.transform.gameObject.name);
            pickup.collider.gameObject.SetActive(false);
            UpdatePlankUI();
            return;
        }

        RaycastHit barricade;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out barricade, range, doorFrameLayerMask))
        {
            if (barricade.transform != null && items.Count > 0 && barricade.transform.GetComponentInChildren<zombieDetect>().zombieCount == 0 && !barricade.transform.GetComponentInChildren<zombieDetect>().playerInDoor)
            {
                audioSource.clip = Barricade;
                audioSource.Play();
                items.Remove(items[0]);
                barricade.collider.gameObject.GetComponent<DoorBarricade>().hp += barricade.collider.gameObject.GetComponent<DoorBarricade>().maxHp / 3;
                UpdatePlankUI();
            }
        }
    }
    private void UpdatePlankUI()
    {
        _numberOfPlanksText.text = "Planks:\n" + items.Count + " / " + itemHoldingLimit;
    }

}
