using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="4Team Project/Create Item", order = 3)]
public class Item : ScriptableObject
{
    public enum ItemType { Equip, Food, Default }

    public int Id => _ID;
    public string Name => _name;
    public string Desc => Description;
    public int Quantity => quantity;

    public int Buy => Price_Buy;
    public int Sell => Price_Sell;

    [Header("공통 아이템 속성")]
    [SerializeField] int _ID;
    [SerializeField] string _name;
    [TextArea(3, 6)]
    [SerializeField] string Description;         // 설명
    [SerializeField, Range(1, 99)] int quantity; // 수량
    public ItemType Type;

    [Space(10)]
    [Header("상점가")]
    [SerializeField] int Price_Buy;              // 매입가
    [SerializeField] int Price_Sell;             // 판매가

    [Space(10)]
    [Header("바인딩")]
    public GameObject Prefab;
    public Sprite Icon;

    public Weapon[] weapondata; //무기 데이터 추가
}

    [System.Serializable]
    public class Weapon
    {
        public Sprite Icon;
        public GameObject weaponPrefab;
        public WeaponType weaponType; // 무기 유형
        public int durability; // 현재 무기 내구도
        protected Transform transform;
    }
