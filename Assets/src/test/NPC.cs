using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;

    public string Name;
    public NPCRole Role;

    public NPC(string name, NPCRole role)
    {
        Name = name;
        Role = role;
    }

    public float collectionRange = 2f;

    public void PerformTask(VillageController village)
    {
        switch (Role)
        {
            case NPCRole.Farmer:
                HarvestFood(village);
                break;
            case NPCRole.Lumberjack:
                CutWood(village);
                break;
            case NPCRole.Miner:
                MineResources(village);
                break;
        }
    }

    private void HarvestFood(VillageController village)
    {
        Resource food = village.FindResourceByType(ResourceType.Vegetables);
        if (food != null)
            FindClosestResourceByType(village, food);
    }

    private void CutWood(VillageController village)
    {
        Resource wood = village.FindResourceByType(ResourceType.Wood);
        if (wood != null)
            FindClosestResourceByType(village, wood);
    }

    private void MineResources(VillageController village)
    {
        Resource coal = village.FindResourceByType(ResourceType.Coal);
        Resource iron = village.FindResourceByType(ResourceType.Iron);
        Resource gold = village.FindResourceByType(ResourceType.Gold);

        if (coal != null)
            FindClosestResourceByType(village, coal);

        if (iron != null)
            FindClosestResourceByType(village, iron);

        if (gold != null)
            FindClosestResourceByType(village, gold);

    }

    void FindClosestResourceByType(VillageController village, Resource type)
    {
        if (village.resourcesObjects.Count == 0)
            return;

        Resource closestResource = null;
        float closestDistance = float.MaxValue;

        foreach (var resource in village.resourcesObjects)
        {
            // check on valid
            if (resource == null || resource.transform == null)
                continue;

            // check type
            if (resource.type != type.type)
                continue;

            float distance = Vector3.Distance(transform.position, resource.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestResource = resource;
            }
        }

        if (closestResource != null)
        {
            agent.SetDestination(closestResource.transform.position);

            if (Vector3.Distance(transform.position, closestResource.transform.position) < collectionRange)
                CollectResource(village,closestResource.transform.position);
           
        }
    }

    void CollectResource(VillageController village, Vector3 resourcePosition)
    {
        Resource resourceToCollect = null;

        foreach (var resource in village.resourcesObjects)
        {
            if (Vector3.Distance(resource.transform.position, resourcePosition) < collectionRange)
            {
                resourceToCollect = resource;
                break;
            }
        }

        if (resourceToCollect != null)
        {
            Debug.Log($"Собран ресурс: {resourceToCollect.type} ({resourceToCollect.amount})");

            village.resourcesObjects.Remove(resourceToCollect);

            Destroy(resourceToCollect.gameObject);

            village.AddResourceToVillage(village, resourceToCollect);

        }
    }

}
