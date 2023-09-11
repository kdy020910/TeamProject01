using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType
{
    None, WateringCan, Rake, Seed,// 내구도 없는 무기들
    FragileAxe, Axe, GoldAxe, // 도끼 
    FragileFishingPole, FishingPole, // 낚싯대
    FragileShovel, Shovel, GoldShovel // 삽     
}
public class PlayerController : AnimationSystem
{
    public Dictionary<int, Weapon> equippedWeaponsMap = new Dictionary<int, Weapon>();

    public Transform rightHand; // 무기를 장착할 손 위치
    public Image[] weaponSlotImages; //슬롯 이미지 바인딩

    public GameObject equippedWeapon;

    public bool IsNoneEquipped = true;
    public bool isShovelEquipped = false;
    public bool isAxeEquipped = false;
    public bool isFishingPoleEquipped = false;
    public bool isRakeEquipped = false;
    public bool isWateringCanEquipped = false;

    public GameObject[] slotBackgrounds; // 각 슬롯 번호에 해당하는 bg 이미지들을 배열로 저장
    private int selectedSlotNumber = -1; // 현재 선택된 슬롯 번호 초기값

    public Weapon currentEquippedWeapon; //현재 장착된 무기
    void Start()
    {
        // 모든 슬롯의 bg 이미지 비활성화
        foreach (var bg in slotBackgrounds)
        {
            bg.SetActive(false);
        }
    }

    void Update()
    {
        InputGetKeyDown();

        // 무기 해제 키
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

            // 현재 선택된 슬롯의 bg 이미지 활성화, 나머지 비활성화
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
            UnequipWeapon(); // 빈 슬롯일 경우에도 무기 해제
        }

        EquipWeaponFromSlot(selectedSlotNumber);
    }

    public void EquipWeapon(Weapon weaponData)
    {
        UnequipWeapon(); // 기존 무기 해제

        // 현재 장착된 무기를 저장합니다.
        currentEquippedWeapon = weaponData;

        // 무기 생성 및 위치 설정
        equippedWeapon = Instantiate(weaponData.weaponPrefab, rightHand); // weaponPrefab 대신 weaponData로 변경
        equippedWeapon.transform.localPosition = Vector3.zero;

        // 무기 애니메이션 설정
        SetWeaponAnimationParameter(weaponData.weaponType);

        // 특정 무기를 든 경우 is()Equipped를 true로 설정
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
                // 다른 무기 유형에 대한 설정도 추가
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

            // 무기 해제 시 해당 무기 유형의 변수를 false로 설정
            IsNoneEquipped = true;

            isWateringCanEquipped = false;
            isAxeEquipped = false;
            isShovelEquipped = false;
            isFishingPoleEquipped = false;
            isRakeEquipped = false;
            // 다른 무기 유형에 대한 설정도 추가

            // 애니메이션 파라미터를 초기화합니다.
            AnimationParameterFalse();
        }
    }

    // 무기 주울때 
    public void TryPickupWeapon(WeaponObject weaponObject)
    {
        if (equippedWeapon == null)
        {
            int slotNumber = GetNextSlotNumber();
            PickupWeapon(weaponObject.weapon);
            equippedWeaponsMap[slotNumber] = weaponObject.weapon;
            Destroy(weaponObject.gameObject);
            Debug.Log("무기를 슬롯에 장착하였습니다.");
        }
        else
        {
            Debug.Log("손에 이미 무기가 장착되어 있습니다.");
        }
    }

    //.. 슬롯 관련 코드
  
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
            return -1; // 선택된 슬롯이 없는 경우 -1 반환
        }
    }

    //무기를 주웠을 때 슬롯에 저장
    public void PickupWeapon(Weapon weapon)
    {
        int slotNumber = GetNextSlotNumber();

        if (equippedWeaponsMap.ContainsKey(slotNumber))
        {
            // 이미 같은 슬롯에 무기가 있다면 제거
            Destroy(equippedWeaponsMap[slotNumber]);
        }

        // 새로운 무기를 슬롯에 저장
        equippedWeaponsMap[slotNumber] = weapon;

        // 해당 슬롯의 무기 이미지를 가져와서 할당
        UpdateSlotImage(slotNumber, weapon.weaponImage);
    }
    public void PickupSeed(Seed seed)
    {
        int slotNumber = GetNextSlotNumber();

        if (equippedWeaponsMap.ContainsKey(slotNumber))
        {
            // 이미 같은 슬롯에 무기가 있다면 제거
            Destroy(equippedWeaponsMap[slotNumber]);
        }

        // 새로운 씨앗을 슬롯에 저장
        equippedWeaponsMap[slotNumber] = new Weapon
        {
            weaponType = WeaponType.Seed,
            weaponPrefab = seed.seedPrefab,
            weaponImage = seed.seedSprite // 씨앗 스프라이트를 슬롯 이미지로 설정
        };

        // 해당 슬롯의 무기 이미지를 가져와서 할당
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

            // UI를 강제로 업데이트합니다. ( 씨앗 땜에 나중에 수정해도 됌)
            Canvas.ForceUpdateCanvases();

            Debug.Log("슬롯 " + slotNumber + "에 무기 이미지가 할당되었습니다.");
        }
        else
        {
            Debug.LogWarning("유효하지 않은 슬롯 번호: " + slotNumber);
        }
    }
    private int GetNextSlotNumber()
    {
        return equippedWeaponsMap.Count + 1;
    }
}