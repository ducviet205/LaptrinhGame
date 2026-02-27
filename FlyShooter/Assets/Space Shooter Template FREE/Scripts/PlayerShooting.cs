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

    [HideInInspector] public float nextFire;

    [Tooltip("current weapon power")]
    [Range(1, 4)]
    public int weaponPower = 1;

    public Guns guns;

    bool shootingIsActive = true;
    [HideInInspector] public int maxweaponPower = 4;

    public static PlayerShooting instance;

    [Header("Bullet Spawn Settings")]
    public Vector3 bulletOffset = new Vector3(0, 0.7f, 0);

    [Header("Multi-shot Settings")]
    public float spreadAngle = 15f;

    // ✅ SOUND COMPONENT
    private AudioSource shootAudio;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        if (guns.centralGun != null)
            guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();

        // ✅ lấy AudioSource trên Player
        shootAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (shootingIsActive && Time.time > nextFire)
        {
            MakeAShot();
            nextFire = Time.time + 1 / fireRate;
        }
    }

    void MakeAShot()
    {
        // VFX
        if (guns.centralGunVFX != null)
            guns.centralGunVFX.Play();

        // ✅ PLAY SOUND (an toàn, không chồng tiếng)
        if (shootAudio != null && shootAudio.clip != null)
            shootAudio.PlayOneShot(shootAudio.clip);

        Vector3 spawnBasePos = guns.centralGun != null
            ? guns.centralGun.transform.position + bulletOffset
            : transform.position + bulletOffset;

        switch (weaponPower)
        {
            case 1:
                CreateLazerShot(projectileObject, spawnBasePos, Vector3.zero);
                break;

            case 2:
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, spreadAngle));
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, -spreadAngle));
                break;

            case 3:
                CreateLazerShot(projectileObject, spawnBasePos, Vector3.zero);
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, spreadAngle));
                CreateLazerShot(projectileObject, spawnBasePos, new Vector3(0, 0, -spreadAngle));
                break;

            case 4:
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
        Instantiate(lazer, pos, transform.rotation * rot);
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}