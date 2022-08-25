using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AxeThrow : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _AxePrefab;
    [SerializeField] private GameObject _HandSpawner;
    [SerializeField] private GameObject _Player;

    [Header("Settings")]
    [SerializeField] private float _ThrowForceMultiplier;
    [SerializeField] private float _ThrowCooldown = 5f;
    [SerializeField] private float _TeleportCooldown = 5f;

    private StarterAssetsInputs mouseControll;
    private float elapsedTime = 0f;
    private float elapsedTimeTeleport = 0f;
    private GameObject throwedAxe;
    private Vector3 _TeleportLocation = new Vector3();
    private Camera cameraPlayer;

    [HideInInspector] public float ThrowForce;
    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool canTeleport;

    private void Awake()
    {
        mouseControll = FindObjectOfType<StarterAssetsInputs>();
        ThrowForce = 50f;
        cameraPlayer = Camera.main;
    }

    private void Update()
    {
        Reload();
        ReloadTeleport();
        ModifyThrowForce();
        ThrowAxe();
        TeleportToLocation();
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
        //Debug.Log(ThrowForce);
    }

    private void ThrowAxe()
    {
        if (canShoot && mouseControll.shoot)
        {
            //Debug.Log("ThrowedAxe");
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
        if (elapsedTime >= _ThrowCooldown)
        {
            canShoot = true;
        }
        else canShoot = false;
    }

    private void TeleportToLocation()
    {
        if (canTeleport && mouseControll.teleport)
        {
            elapsedTimeTeleport = 0f;
            RaycastHit hit;
            if (Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out hit, 300f))
            {
                _TeleportLocation = hit.point;
                StartCoroutine("Teleport");
            }
            
        }
        if (mouseControll.teleport) mouseControll.teleport = false;
    }

    private void ReloadTeleport()
    {
        elapsedTimeTeleport += Time.deltaTime;
        if (elapsedTimeTeleport >= _TeleportCooldown)
        {
            canTeleport = true;
        }
        else
        {
            canTeleport = false;
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForEndOfFrame();
        _Player.transform.position = _TeleportLocation;
    }
}
