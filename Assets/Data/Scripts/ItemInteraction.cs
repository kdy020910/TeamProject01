using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : SystemProPerty
{
    public GameObject pickupUI; // 줍기 ui 오브젝트 

    private GameObject itemInRange;

    private void Start()
    {
        HidePickupUI();
    }

    private void Update()
    {
        if (itemInRange != null && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("줍기가 호출됨");
            myAnim.SetTrigger("Picking");
            PickupItem();

            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                WeaponObject weaponObject = itemInRange.GetComponent<WeaponObject>();
                if (weaponObject != null)
                {
                    playerController.TryPickupWeapon(weaponObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood") || other.CompareTag("HardWood") || other.CompareTag("Stone") 
            || other.CompareTag("Weapon") || other.CompareTag("Branch") || other.CompareTag("Seed"))             
        {
            itemInRange = other.gameObject;
            pickupUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wood") || other.CompareTag("HardWood") || other.CompareTag("Stone")
            || other.CompareTag("Weapon")|| other.CompareTag("Branch") || other.CompareTag("Seed")) 
        {
            itemInRange = null;
            HidePickupUI();
        }
    }

    private void HidePickupUI()
    {
        pickupUI.SetActive(false);
    }

    public void PickupItem()
    {
        if (itemInRange != null)
        {
            /*
            if(itemInRange.CompareTag("Weapon"))
            {
                Debug.Log("무기를 주웠다");
                Destroy(itemInRange);
            }*/
            if (itemInRange.CompareTag("Wood"))
            {
                Debug.Log("목재를 주웠다");
                Destroy(itemInRange);
            }
            if (itemInRange.CompareTag("HardWood"))
            {
                Debug.Log("단단한 목재를 주웠다");
                Destroy(itemInRange);
            }
            if (itemInRange.CompareTag("Branch"))
            {
                Debug.Log("나뭇가지를 주웠다.");
                Destroy(itemInRange);
            }
            if(itemInRange.CompareTag("Seed"))
            {
                Debug.Log("씨앗을 주웠다.");
                Destroy(itemInRange);
            }
            else if (itemInRange.CompareTag("Stone"))
            {
                Debug.Log("돌을 주웠다");
                Destroy(itemInRange);
            }

            HidePickupUI();
        }
    }
}

