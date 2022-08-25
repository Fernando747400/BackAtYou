using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AxeThrow : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _AxePrefab;
    [SerializeField] private GameObject _HandSpawner;

    [Header("Settings")]
    [SerializeField] private float _ThrowForceMultiplier;
    [SerializeField] private float throwCooldown = 5f;

    private StarterAssetsInputs mouseControll;
    private float elapsedTime = 0f;
    private GameObject throwedAxe;

    [HideInInspector] public float ThrowForce;
    [HideInInspector] public bool canShoot;

    private void Awake()
    {
        mouseControll = FindObjectOfType<StarterAssetsInputs>();
        ThrowForce = 50f;
    }

    private void Update()
    {
        Reload();
        ModifyThrowForce();
        ThrowAxe();
    }

    private void ModifyThrowForce()
    {
        if (mouseControll.scroll > 0 && ThrowForce < 100f)
        {
            ThrowForce += 1f;
        }

        if (mouseControll.scroll < 0 && ThrowForce > 0f)
        {
            ThrowForce -= 1f;
        }
        Debug.Log(ThrowForce);
    }

    private void ThrowAxe()
    {
        if (canShoot && mouseControll.shoot)
        {
            Debug.Log("ThrowedAxe");
            elapsedTime = 0f;
            throwedAxe = Instantiate(_AxePrefab,_HandSpawner.transform);
            throwedAxe.transform.parent = null;
            throwedAxe.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0 , _ThrowForceMultiplier * ThrowForce), ForceMode.Impulse);           
        }
        if(mouseControll.shoot) mouseControll.shoot = false;
    }

    private void Reload()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= throwCooldown)
        {
            canShoot = true;
        }
        else canShoot = false;
    }
}
