using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

/*
 순서
1.인벤에서 아이템 (무기) 우클릭 하면 슬롯에 순서대로 장착되야함
( 무기 데이터를 불러와야함)
2.우클릭하면 슬롯에 있는 Object 컴포넌트에 해당 아이템(무기)
데이터가 표시되야함
3. 표시된 아이템(무기) 정보로 플레이어 손에 무기 장착시켜야함
4. 장착과 동시에 그 무기에 맞는 애니메이션이 실행되야함
5. 4번까지 완료됐다면 PlayerTrigger에 있는 코드가 잘 실행되도록 수정해야함 ( 현재 장착된 무기가 뭔지 체크하는 코드 수정해야할거임)
6. 다른거까지 다 잘 작동되면 완료
*/

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

    public GameObject equippedWeapon; // 이걸 못찾는중
    public Weapon currentEquippedWeapon; // 현재 장착된 무기

    public GameObject addSlotButton;

    public GameObject[] slotBackgrounds; // 각 슬롯 번호에 해당하는 bg 이미지들을 배열로 저장

    private int selectedSlotNumber = -1; // 현재 선택된 슬롯 번호 초기값

    public bool IsNoneEquipped = true;
    public bool isShovelEquipped = false;
    public bool isAxeEquipped = false;
    public bool isFishingPoleEquipped = false;
    public bool isRakeEquipped = false;
    public bool isWateringCanEquipped = false;

    void Start()
    {
        addSlotButton.SetActive(false);
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
            //UnequipWeapon();
        }

    }
    public void InputGetKeyDown()
    {
        // 현재 선택된 슬롯의 bg 이미지 활성화, 나머지 비활성화
        for (int i = 0; i < slotBackgrounds.Length; i++)
        {
            slotBackgrounds[i].SetActive(i + 1 == selectedSlotNumber);
        }
    }

    public void ShowAddSlotButton()
    {
        addSlotButton.SetActive(true);
    }

    public void OnButtonClick()
    {
        // 버튼 클릭됐을때 실행되야할 코드
        // 비어있는 슬롯에( 순서대로 )추가되야함
        // 데이터도 같이 추가되면 키보드값으로 플레이어가 장착했을때 그 데이터에 있는 정보로 무기를 구분할 수 있음
        // 데이터 추가 안되면 안되는대로 해야함
    }
}