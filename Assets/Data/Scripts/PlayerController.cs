using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType
{
    None, WateringCan, Rake, Seed,// ������ ���� �����
    FragileAxe, Axe, GoldAxe, // ���� 
    FragileFishingPole, FishingPole, // ���˴�
    FragileShovel, Shovel, GoldShovel // ��     
}
public class PlayerController : AnimationSystem
{
    public Dictionary<int, Weapon> equippedWeaponsMap = new Dictionary<int, Weapon>();

    public Transform rightHand; // ���⸦ ������ �� ��ġ
    public Image[] weaponSlotImages; //���� �̹��� ���ε�

    public GameObject equippedWeapon;

    public bool IsNoneEquipped = true;
    public bool isShovelEquipped = false;
    public bool isAxeEquipped = false;
    public bool isFishingPoleEquipped = false;
    public bool isRakeEquipped = false;
    public bool isWateringCanEquipped = false;

    public GameObject[] slotBackgrounds; // �� ���� ��ȣ�� �ش��ϴ� bg �̹������� �迭�� ����
    private int selectedSlotNumber = -1; // ���� ���õ� ���� ��ȣ �ʱⰪ

    public Weapon currentEquippedWeapon; //���� ������ ����
    void Start()
    {
        // ��� ������ bg �̹��� ��Ȱ��ȭ
        foreach (var bg in slotBackgrounds)
        {
            bg.SetActive(false);
        }
    }

    void Update()
    {
        InputGetKeyDown();

        // ���� ���� Ű
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnequipWeapon();
        }
    }
    public void InputGetKeyDown()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
        Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedSlotNumber = GetSelectedSlotNumber();
            HandleWeaponEquipInput();

            // ���� ���õ� ������ bg �̹��� Ȱ��ȭ, ������ ��Ȱ��ȭ
            for (int i = 0; i < slotBackgrounds.Length; i++)
            {
                slotBackgrounds[i].SetActive(i + 1 == selectedSlotNumber);
            }
        }
    }
    void HandleWeaponEquipInput()
    {
        int selectedSlotNumber = GetSelectedSlotNumber();

        if (equippedWeaponsMap.TryGetValue(selectedSlotNumber, out Weapon selectedWeapon))
        {
            if (equippedWeapon == null)
            {
                EquipWeapon(selectedWeapon);
            }
        }
        else
        {
            AnimationParameterFalse();
            UnequipWeapon(); // �� ������ ��쿡�� ���� ����
        }

        EquipWeaponFromSlot(selectedSlotNumber);
    }

    public void EquipWeapon(Weapon weaponData)
    {
        UnequipWeapon(); // ���� ���� ����

        // ���� ������ ���⸦ �����մϴ�.
        currentEquippedWeapon = weaponData;

        // ���� ���� �� ��ġ ����
        equippedWeapon = Instantiate(weaponData.weaponPrefab, rightHand); // weaponPrefab ��� weaponData�� ����
        equippedWeapon.transform.localPosition = Vector3.zero;

        // ���� �ִϸ��̼� ����
        SetWeaponAnimationParameter(weaponData.weaponType);

        // Ư�� ���⸦ �� ��� is()Equipped�� true�� ����
        switch (weaponData.weaponType)
        {
            case WeaponType.None:
                IsNoneEquipped = true;
                break;
            case WeaponType.Axe:
            case WeaponType.FragileAxe:
            case WeaponType.GoldAxe:
                isAxeEquipped = true;
                break;
            case WeaponType.Shovel:
            case WeaponType.FragileShovel:
            case WeaponType.GoldShovel:
                isShovelEquipped = true;
                break;
            case WeaponType.FishingPole:
            case WeaponType.FragileFishingPole:
                isFishingPoleEquipped = true;
                break;
            case WeaponType.Rake:
                isRakeEquipped = true;
                break;
            case WeaponType.WateringCan:
                isWateringCanEquipped = true;
                break;
                // �ٸ� ���� ������ ���� ������ �߰�
        }

        UpdateSlotImage(GetSelectedSlotNumber(), weaponData.weaponImage);
    }


    public void UnequipWeapon()
    {
        Debug.Log("UnequipWeapon method called.");
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
            equippedWeapon = null;

            // ���� ���� �� �ش� ���� ������ ������ false�� ����
            IsNoneEquipped = true;

            isWateringCanEquipped = false;
            isAxeEquipped = false;
            isShovelEquipped = false;
            isFishingPoleEquipped = false;
            isRakeEquipped = false;
            // �ٸ� ���� ������ ���� ������ �߰�

            // �ִϸ��̼� �Ķ���͸� �ʱ�ȭ�մϴ�.
            AnimationParameterFalse();
        }
    }

    // ���� �ֿﶧ 
    public void TryPickupWeapon(WeaponObject weaponObject)
    {
        if (equippedWeapon == null)
        {
            int slotNumber = GetNextSlotNumber();
            PickupWeapon(weaponObject.weapon);
            equippedWeaponsMap[slotNumber] = weaponObject.weapon;
            Destroy(weaponObject.gameObject);
            Debug.Log("���⸦ ���Կ� �����Ͽ����ϴ�.");
        }
        else
        {
            Debug.Log("�տ� �̹� ���Ⱑ �����Ǿ� �ֽ��ϴ�.");
        }
    }

    //.. ���� ���� �ڵ�
  
    public int GetSelectedSlotNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        else
        {
            return -1; // ���õ� ������ ���� ��� -1 ��ȯ
        }
    }

    //���⸦ �ֿ��� �� ���Կ� ����
    public void PickupWeapon(Weapon weapon)
    {
        int slotNumber = GetNextSlotNumber();

        if (equippedWeaponsMap.ContainsKey(slotNumber))
        {
            // �̹� ���� ���Կ� ���Ⱑ �ִٸ� ����
            Destroy(equippedWeaponsMap[slotNumber]);
        }

        // ���ο� ���⸦ ���Կ� ����
        equippedWeaponsMap[slotNumber] = weapon;

        // �ش� ������ ���� �̹����� �����ͼ� �Ҵ�
        UpdateSlotImage(slotNumber, weapon.weaponImage);
    }
    public void PickupSeed(Seed seed)
    {
        int slotNumber = GetNextSlotNumber();

        if (equippedWeaponsMap.ContainsKey(slotNumber))
        {
            // �̹� ���� ���Կ� ���Ⱑ �ִٸ� ����
            Destroy(equippedWeaponsMap[slotNumber]);
        }

        // ���ο� ������ ���Կ� ����
        equippedWeaponsMap[slotNumber] = new Weapon
        {
            weaponType = WeaponType.Seed,
            weaponPrefab = seed.seedPrefab,
            weaponImage = seed.seedSprite // ���� ��������Ʈ�� ���� �̹����� ����
        };

        // �ش� ������ ���� �̹����� �����ͼ� �Ҵ�
        UpdateSlotImage(slotNumber, seed.seedSprite);
    }


    void EquipWeaponFromSlot(int slotNumber)
    {
        if (equippedWeaponsMap.TryGetValue(slotNumber, out Weapon equippedWeaponData))
        {
            EquipWeapon(equippedWeaponData);
        }
    }
    public void UpdateSlotImage(int slotNumber, Sprite weaponImage)
    {
        int slotIndex = slotNumber - 1;
        if (slotIndex >= 0 && slotIndex < weaponSlotImages.Length)
        {
            weaponSlotImages[slotIndex].sprite = weaponImage;

            // UI�� ������ ������Ʈ�մϴ�. ( ���� ���� ���߿� �����ص� ��)
            Canvas.ForceUpdateCanvases();

            Debug.Log("���� " + slotNumber + "�� ���� �̹����� �Ҵ�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� ���� ��ȣ: " + slotNumber);
        }
    }
    private int GetNextSlotNumber()
    {
        return equippedWeaponsMap.Count + 1;
    }
}