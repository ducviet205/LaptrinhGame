using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject centralGun;
    [HideInInspector] public ParticleSystem centralGunVFX;
}

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class PlayerShooting : MonoBehaviour
{
    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    // Time for next shot
    [HideInInspector] public float nextFire;

    [Tooltip("current weapon power")]
    [Range(1, 4)]
    public int weaponPower = 1;

    public Guns guns;

    bool shootingIsActive = true;
    [HideInInspector] public int maxweaponPower = 4;

    public static PlayerShooting instance;

    [Header("Bullet Spawn Settings")]
    [Tooltip("Offset from central gun position (e.g., forward to nose tip)")]
    public Vector3 bulletOffset = new Vector3(0, 0.7f, 0);  // Default Y+0.7 như tutorial slide

    [Header("Multi-shot Settings")]
    public float spreadAngle = 15f;  // Góc lệch cho level cao (độ)

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        // Receiving shooting visual effects components
        if (guns.centralGun != null)
        {
            guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
        }
    }

    private void Update()
    {
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                MakeAShot();
                nextFire = Time.time + 1 / fireRate;
            }
        }
    }

    // Method for a shot
    void MakeAShot()
    {
        // Luôn play VFX ở central gun nếu có
        if (guns.centralGunVFX != null)
            guns.centralGunVFX.Play();

        // Tính vị trí base spawn (centralGun + offset)
        Vector3 spawnBasePos = guns.centralGun != null
            ? guns.centralGun.transform.position + bulletOffset
            : transform.position + bulletOffset;

        switch (weaponPower)
        {
            case 1:
                // Level 1: Chỉ 1 đạn giữa
                CreateLazerShot(projectileObject, spawnBasePos, Vector3.zero);
                break;

            case 2:
                // Level 2: 2 đạn, lệch trái phải
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, spreadAngle));
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, -spreadAngle));
                break;

            case 3:
                // Level 3: 3 đạn (giữa + lệch)
                CreateLazerShot(projectileObject, spawnBasePos, Vector3.zero);
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, spreadAngle));
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, -spreadAngle));
                break;

            case 4:
                // Level 4: 5 đạn, spread rộng
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, spreadAngle * 1.5f));
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, spreadAngle * 0.5f));
                CreateLazerShot(projectileObject, spawnBasePos, Vector3.zero);
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, -spreadAngle * 0.5f));
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, -spreadAngle * 1.5f));
                break;

            default:
                CreateLazerShot(projectileObject, spawnBasePos, Vector3.zero);
                break;
        }
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rotEuler)
    {
        Quaternion rot = Quaternion.Euler(rotEuler);
        Instantiate(lazer, pos, transform.rotation * rot);  // Rotation của tàu + spread angle
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}