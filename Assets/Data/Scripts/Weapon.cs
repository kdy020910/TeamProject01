using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Custom/Weapon")] //무기 정보 저장
public class Weapon : ScriptableObject
{
    public string weaponName; // 무기 이름
    public GameObject weaponPrefab; // 무기 프리펩
    public WeaponType weaponType; // 무기 유형
    public Sprite weaponImage; // 무기 이미지
    public int durability; // 현재 무기 내구도

    protected Transform transform;

}
