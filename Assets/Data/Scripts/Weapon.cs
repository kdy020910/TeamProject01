using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Custom/Weapon")] //���� ���� ����
public class Weapon : ScriptableObject
{
    public string weaponName; // ���� �̸�
    public GameObject weaponPrefab; // ���� ������
    public WeaponType weaponType; // ���� ����
    public Sprite weaponImage; // ���� �̹���
    public int durability; // ���� ���� ������

    protected Transform transform;

}
