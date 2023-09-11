using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public List<Weapon> weaponPrefabs = new List<Weapon>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public RecipeData[] weaponRecipes;

    // �ٸ� ��ũ��Ʈ���� Ư�� ���� �����Ǹ� �������� �޼���
    public RecipeData GetWeaponRecipe(WeaponType weaponType)
    {
        foreach (var recipe in weaponRecipes)
        {
            // ���� �������� �̸��� WeaponType�� ���Ͽ� �ش� ���� �����Ǹ� ã���ϴ�.
            if (recipe.recipeName == weaponType.ToString())
            {
                return recipe;
            }
        }
        return null; // �ش� ���� �����Ǹ� ã�� ���� ���
    }
}