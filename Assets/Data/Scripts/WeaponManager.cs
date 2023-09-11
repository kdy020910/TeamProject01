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

    // 다른 스크립트에서 특정 무기 레시피를 가져오는 메서드
    public RecipeData GetWeaponRecipe(WeaponType weaponType)
    {
        foreach (var recipe in weaponRecipes)
        {
            // 무기 레시피의 이름과 WeaponType을 비교하여 해당 무기 레시피를 찾습니다.
            if (recipe.recipeName == weaponType.ToString())
            {
                return recipe;
            }
        }
        return null; // 해당 무기 레시피를 찾지 못한 경우
    }
}