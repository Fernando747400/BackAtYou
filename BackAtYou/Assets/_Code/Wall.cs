using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour, IConstructable
{
    [Header("Settings")]
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _upgradePoints;
    [SerializeField] private bool _canUpgrade;

    [Header("Events")]
    public UnityEvent BuildEvent;
    public UnityEvent RepairEvent;
    public UnityEvent UpgradePointsEvent;
    public UnityEvent UpgradeEvent;

    private float _maxHealth;
    private float _upgradePointsRequired;

    #region Get and set
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float UpgradePoints { get => _upgradePoints; set => _upgradePoints = value; }
    public float UpgradePointsRequired { get => _upgradePointsRequired; set => _upgradePointsRequired = value; }
    public bool CanUpgrade { get => _canUpgrade; set => _canUpgrade = value; }

    public UnityEvent IBuildEvent => BuildEvent;
    public UnityEvent IRepairEvent => RepairEvent;
    public UnityEvent IUpgradePointsEvent => UpgradePointsEvent;
    public UnityEvent IUpgradeEvent => UpgradeEvent;

    #endregion
}
