using UnityEngine;
using Yarn.Unity;

public class DrinkManager : MonoBehaviour
{
    [Header("Drink Prefabs")]
    public GameObject beerPrefab;
    public GameObject cocktailPrefab;
    public GameObject waterPrefab;

    [YarnCommand("activateDrink")]
    public void ActivateDrink(string drinkName)
    {
        // Turn off all prefabs initially
        if (beerPrefab != null) beerPrefab.SetActive(false);
        if (cocktailPrefab != null) cocktailPrefab.SetActive(false);
        if (waterPrefab != null) waterPrefab.SetActive(false);

        // Activate the selected drink prefab
        switch (drinkName.ToLower())
        {
            case "beer":
                if (beerPrefab != null)
                {
                    beerPrefab.SetActive(true);
                    Debug.Log("Activated Beer prefab.");
                }
                else
                {
                    Debug.LogWarning("No Beer prefab assigned.");
                }
                break;
            case "cocktail":
                if (cocktailPrefab != null)
                {
                    cocktailPrefab.SetActive(true);
                    Debug.Log("Activated Cocktail prefab.");
                }
                else
                {
                    Debug.LogWarning("No Cocktail prefab assigned.");
                }
                break;
            case "water":
                if (waterPrefab != null)
                {
                    waterPrefab.SetActive(true);
                    Debug.Log("Activated Water prefab.");
                }
                else
                {
                    Debug.LogWarning("No Water prefab assigned.");
                }
                break;
            default:
                Debug.LogWarning($"Drink '{drinkName}' not recognized.");
                break;
        }
    }
}
