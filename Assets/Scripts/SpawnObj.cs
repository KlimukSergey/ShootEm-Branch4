using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    private GameObject[] bulletSpawn;
    private GameObject[] aidKitSpawn;
    [SerializeField] private GameObject bulletPref, aidKitPref;
    [SerializeField] private float timeSpawnBullet, timeSpawnAid_Kit;

    // Start is called before the first frame update
    void Awake()
    {
        bulletSpawn = GameObject.FindGameObjectsWithTag("BulletSpawn");
        aidKitSpawn = GameObject.FindGameObjectsWithTag("AidSpawn");
        SpawnBullet();
        SpawnAidKit();
    }

    void Update()
    {
    }
    void SpawnBullet()
    {
        StartCoroutine(WaitBullet(timeSpawnBullet));
        GameObject bul = Instantiate(bulletPref,
                     bulletSpawn[Random.Range(0, bulletSpawn.Length)].transform.position,
                     transform.rotation);
        Destroy(bul, 60f);

    }
    void SpawnAidKit()
    {
        StartCoroutine(WaitAidKit(timeSpawnAid_Kit));
        GameObject kit = Instantiate(aidKitPref,
        aidKitSpawn[Random.Range(0, aidKitSpawn.Length)].transform.position,
        transform.rotation);
        //  GameObject kit=  Instantiate(aidKitPref,
        //               aidKitSpawn[Random.Range(0, aidKitSpawn.Length)].transform.position,
        //             transform.rotation);

        Destroy(kit, 60f);

    }
    IEnumerator WaitBullet(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnBullet();
    }

    IEnumerator WaitAidKit(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnAidKit();
    }

}
