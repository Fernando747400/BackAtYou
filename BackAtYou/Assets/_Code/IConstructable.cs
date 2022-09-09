using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IConstructable 
{
    GameObject Ghost
    {
        get;
        set;
    }

    GameObject Basic
    {
        get;
        set;
    }

    GameObject UpgradeOne
    {
        get;
        set;
    }

    GameObject UpgardeTwo
    {
        get;
        set;
    }

    float CurrentHealth
    {
        get;
        set;
    }

    float MaxHealth
    {
        get;
        set;
    }

    float UpgradePoints
    {
        get;
        set;
    }

    float UpgardePointsRequired
    {
        get;
        set;
    }

    bool CanUpgrade
    {
        get;
        set;
    }

    UnityEvent BuildEvent
    {
        get;
        set;
    }

    UnityEvent RepairEvent
    {
        get;
        set;
    }

    UnityEvent UpgradeEvent
    {
        get;
        set;
    }

    void Build(GameObject building, Vector3 position, Quaternion rotation)
    {
        GameObject.Instantiate(building, position, rotation);
        BuildEvent?.Invoke();
    }

    void Repair(float repairValue)
    {
        if (CurrentHealth <= MaxHealth) CurrentHealth += repairValue;
        CurrentHealth = Math.Clamp(CurrentHealth, 0f, MaxHealth);
        RepairEvent?.Invoke();
    }

    void Upgrade(GameObject oldBuilding, GameObject newBuilding)
    {
        oldBuilding.SetActive(false);
        Build(newBuilding, oldBuilding.transform.position, oldBuilding.transform.rotation);
        UpgradeEvent?.Invoke();
    }

    void UpgradeMeshOnly(GameObject building, Mesh newMesh)
    {
        building.GetComponent<MeshFilter>().mesh = newMesh;
        UpgradeEvent?.Invoke();
    }
}
