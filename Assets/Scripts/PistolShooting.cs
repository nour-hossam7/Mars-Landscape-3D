using System.Collections;
using TMPro;
using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera aimCamera;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Shooting Settings")]
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float bulletLifetime = 3f;

    [Header("Ammo Settings")]
    [SerializeField] private int magazineSize = 10;
    [SerializeField] private float reloadTime = 2f;

    private int currentAmmo;
    private bool isReloading;

    private void Start()
    {
        currentAmmo = magazineSize;
        UpdateAmmoUI();
    }

    private void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0)
            {
                Shoot();
                currentAmmo--;
                UpdateAmmoUI();
            }
            else
            {
                Debug.Log("Out of Ammo! Press R to Reload");
            }
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || aimCamera == null)
        {
            Debug.LogWarning(
                "Please assign Bullet Prefab, Fire Point, and Aim Camera."
            );

            return;
        }

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        Vector3 shootDirection = aimCamera.transform.forward;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(shootDirection)
        );

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity =
                shootDirection * bulletSpeed;
        }
        else
        {
            Debug.LogWarning(
                "The Bullet Prefab does not have a Rigidbody."
            );
        }

        Destroy(bullet, bulletLifetime);
    }

    private IEnumerator Reload()
    {
        if (currentAmmo == magazineSize)
        {
            yield break;
        }

        isReloading = true;

        if (ammoText != null)
        {
            ammoText.text = "Reloading...";
        }

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;

        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text =
                "Ammo: " + currentAmmo + " / " + magazineSize;
        }
    }
}