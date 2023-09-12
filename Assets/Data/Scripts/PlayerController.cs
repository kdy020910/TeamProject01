using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

/*
 ����
1.�κ����� ������ (����) ��Ŭ�� �ϸ� ���Կ� ������� �����Ǿ���
( ���� �����͸� �ҷ��;���)
2.��Ŭ���ϸ� ���Կ� �ִ� Object ������Ʈ�� �ش� ������(����)
�����Ͱ� ǥ�õǾ���
3. ǥ�õ� ������(����) ������ �÷��̾� �տ� ���� �������Ѿ���
4. ������ ���ÿ� �� ���⿡ �´� �ִϸ��̼��� ����Ǿ���
5. 4������ �Ϸ�ƴٸ� PlayerTrigger�� �ִ� �ڵ尡 �� ����ǵ��� �����ؾ��� ( ���� ������ ���Ⱑ ���� üũ�ϴ� �ڵ� �����ؾ��Ұ���)
6. �ٸ��ű��� �� �� �۵��Ǹ� �Ϸ�
*/

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

    public GameObject equippedWeapon; // �̰� ��ã����
    public Weapon currentEquippedWeapon; // ���� ������ ����

    public GameObject addSlotButton;

    public GameObject[] slotBackgrounds; // �� ���� ��ȣ�� �ش��ϴ� bg �̹������� �迭�� ����

    private int selectedSlotNumber = -1; // ���� ���õ� ���� ��ȣ �ʱⰪ

    public bool IsNoneEquipped = true;
    public bool isShovelEquipped = false;
    public bool isAxeEquipped = false;
    public bool isFishingPoleEquipped = false;
    public bool isRakeEquipped = false;
    public bool isWateringCanEquipped = false;

    void Start()
    {
        addSlotButton.SetActive(false);
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
            //UnequipWeapon();
        }

    }
    public void InputGetKeyDown()
    {
        // ���� ���õ� ������ bg �̹��� Ȱ��ȭ, ������ ��Ȱ��ȭ
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
        // ��ư Ŭ�������� ����Ǿ��� �ڵ�
        // ����ִ� ���Կ�( ������� )�߰��Ǿ���
        // �����͵� ���� �߰��Ǹ� Ű���尪���� �÷��̾ ���������� �� �����Ϳ� �ִ� ������ ���⸦ ������ �� ����
        // ������ �߰� �ȵǸ� �ȵǴ´�� �ؾ���
    }
}