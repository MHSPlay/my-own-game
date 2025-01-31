using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    None,

    // Material
    Wood,
    Coal,
    Iron,
    Gold,

    // Food
    Vegetables,
    Meat,
    Fish,
}

[System.Serializable]
public class Resource : MonoBehaviour
{
    [Header("ResourceType")]
    public ResourceType type;

    [Header("Resource Quantity")]
    [Min(1)]
    public int amount;

    [Header("Resource cost per unit")]
    [Min(0)]
    public int cost;

    [Header("Resource total cost")]
    public int totalCost;

    // it's for the inspector at Unity.
    private void OnValidate()
    {
        CalcTotalCost();
    }

    public Resource(ResourceType resource_type)
    {
        type = resource_type;
        amount = 1;

        CalcTotalCost();
    }

    public Resource(ResourceType resource_type, int resource_amount)
    {
        type = resource_type;
        amount = resource_amount;

        CalcTotalCost();
    }

    public string GetCategory()
    {
        switch (type)
        {
            case ResourceType.Wood:
            case ResourceType.Coal:
            case ResourceType.Iron:
            case ResourceType.Gold:
                return "Material";

            case ResourceType.Vegetables:
            case ResourceType.Meat:
            case ResourceType.Fish:
                return "Food";

            default:
                return "Unknown";
        }
    }

    public void CalcTotalCost() => totalCost = amount * cost;

}