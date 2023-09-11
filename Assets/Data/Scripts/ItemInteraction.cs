using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : SystemProPerty
{
    public GameObject pickupUI; // �ݱ� ui ������Ʈ 

    private GameObject itemInRange;

    private void Start()
    {
        HidePickupUI();
    }

    private void Update()
    {
        if (itemInRange != null && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("�ݱⰡ ȣ���");
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
                Debug.Log("���⸦ �ֿ���");
                Destroy(itemInRange);
            }*/
            if (itemInRange.CompareTag("Wood"))
            {
                Debug.Log("���縦 �ֿ���");
                Destroy(itemInRange);
            }
            if (itemInRange.CompareTag("HardWood"))
            {
                Debug.Log("�ܴ��� ���縦 �ֿ���");
                Destroy(itemInRange);
            }
            if (itemInRange.CompareTag("Branch"))
            {
                Debug.Log("���������� �ֿ���.");
                Destroy(itemInRange);
            }
            if(itemInRange.CompareTag("Seed"))
            {
                Debug.Log("������ �ֿ���.");
                Destroy(itemInRange);
            }
            else if (itemInRange.CompareTag("Stone"))
            {
                Debug.Log("���� �ֿ���");
                Destroy(itemInRange);
            }

            HidePickupUI();
        }
    }
}

