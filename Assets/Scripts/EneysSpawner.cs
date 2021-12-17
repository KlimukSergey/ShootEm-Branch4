using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneysSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawn;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject janitor;
    public bool isSpawn;
    private int enemysCount;
    private float stoppingDistance = 5;

    void Awake()
    {
        janitor = GameObject.Find("Janitor");
        janitor.SetActive(false);
        SpawnEnemy(stoppingDistance);
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeToSpawn);
        if (isSpawn) SpawnEnemy(stoppingDistance);
        StartCoroutine(Spawn());
    }
    void SpawnEnemy(float stopDistance)
    {
        Transform _home = spawn[Random.Range(0, spawn.Length)];
        GameObject obj = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)],
        _home.position, Quaternion.identity);
        enemysCount++;
        obj.GetComponent<EnemyContr>().home = _home;

    }
    public void LevelUp()
    {
        isSpawn = false;
        //  GetComponent<EnemyContr>().AtHome();
        EnemyContr[] enemies = FindObjectsOfType<EnemyContr>();
        foreach (var e in enemies)
        {
            e.AtHome();
        }
        StartCoroutine(Film());

    }
    IEnumerator Film()
    {
        yield return new WaitForSeconds(10);
        janitor.SetActive(true);
        //активация таймлинии
        //в таймлинию вставить маркер за движение игрока
        //

    }
    public void AgainSpawn()
    {
        isSpawn = true;
        stoppingDistance += 2; //Прокачка Карапузов

        StartCoroutine(Spawn());
    }
}
