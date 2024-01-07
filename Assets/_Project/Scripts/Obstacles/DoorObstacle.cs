using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObstacle : Obstacle
{
    [SerializeField] private int[] neededObjectsIndex;
    [SerializeField] private string sceneToLoad;
    private bool[] requirements;

    public static event Action<string> OnDoorOpen;

    private void Start()
    {
        requirements = new bool[neededObjectsIndex.Length];
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Inventory inventory))
        {
            for (int i = 0; i < neededObjectsIndex.Length; i++)
            {
                if (inventory.Items.Count > neededObjectsIndex[i])
                {
                    if (inventory.Items[neededObjectsIndex[i]].Value == neededObjectsIndex[i])
                    {
                        inventory.RemoveItem(inventory.Items[neededObjectsIndex[i]]);
                        requirements[i] = true;
                    }
                }
            }

            if (CheckRequirements())
            {
                OnDoorOpen?.Invoke(sceneToLoad);
            }
        }
    }

    private bool CheckRequirements()
    {
        foreach (var requirement in requirements)
        {
            if (!requirement)
            {
                return false;
            }
        }

        return true;
    }
}
