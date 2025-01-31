using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum NPCRole
{
    Farmer,
    Lumberjack,
    Miner
}

public class VillageController : MonoBehaviour
{
    public string villageName;

    public List<NPC> NPC;
    public List<Resource> villageResources;
    public List<Resource> resourcesObjects;

    void Start()
    {
        // find all resources
        //Resource[] resourceObjects = Object.FindObjectsByType<Resource>(FindObjectsSortMode.None);
        //resourcesObjects = new List<Resource>(resourceObjects);
    }

    void Update()
    {
        foreach (var npc in NPC)
        {
            npc.PerformTask(this);
        }
    }
        
    public Resource FindResourceByType(ResourceType type)
    {
        foreach (var resource in resourcesObjects)
        {
            if (resource.type == type)
                return resource;
        }

        return null;
    }

    public void AddResourceToVillage(VillageController village, Resource newResource)
    {
        var existingResource = village.resourcesObjects
            .FirstOrDefault(r => r.type == newResource.type);

        if (existingResource != null)
        {
            existingResource.amount = existingResource.amount + newResource.amount;
            Debug.Log($"Обновлен ресурс: {existingResource.type}, новое количество: {existingResource.amount}");
        }
        else
        {
            village.villageResources.Add(newResource);
            Debug.Log($"Добавлен новый ресурс: {newResource.type}, количество: {newResource.amount}");
        }
    }

}
