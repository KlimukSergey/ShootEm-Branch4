using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneysSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawn;
    [SerializeField]
    private float timeToSpawn = 3f;
    [SerializeField]
    private GameObject[] enemyPrefab;
    [SerializeField]
    private GameObject janitor;
    private GameObject Boss;
    [SerializeField]
    private float timeToSpawnJanitor = 10f;
    public List<GameObject> enemies;
    public bool isSpawn;
    private int enemysCount;
    private float stoppingDistance = 5;
    private Transform janitorSpawn;

    void Awake()
    {
        enemies = new List<GameObject>(32);
        Boss = GameObject.Find("Janitor");
        Boss.SetActive(false);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< Updated upstream
      //  SpawnEnemy(stoppingDistance);
=======
       // SpawnEnemy(stoppingDistance);
>>>>>>> Stashed changes
=======
        SpawnEnemy(stoppingDistance);
>>>>>>> parent of 9c5c9f5 (0.3.1)
=======
        SpawnEnemy(stoppingDistance);
>>>>>>> parent of 9c5c9f5 (0.3.1)
=======
        SpawnEnemy(stoppingDistance);
>>>>>>> parent of 9c5c9f5 (0.3.1)
=======
        SpawnEnemy(stoppingDistance);
>>>>>>> parent of 9c5c9f5 (0.3.1)
=======
        SpawnEnemy(stoppingDistance);
>>>>>>> parent of 9c5c9f5 (0.3.1)
=======
        SpawnEnemy(stoppingDistance);
>>>>>>> parent of 9c5c9f5 (0.3.1)
=======
        SpawnEnemy(stoppingDistance);
>>>>>>> parent of 9c5c9f5 (0.3.1)
        StartCoroutine(Spawn());
        janitorSpawn = GameObject.Find("JanitorSpawn").GetComponent<Transform>();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeToSpawn);

        if (isSpawn && enemies.Count < 30)
            SpawnEnemy(stoppingDistance);
        StartCoroutine(Spawn());
    }
    void SpawnEnemy(float stopDistance)
    {
        Transform _home = spawn[Random.Range(0, spawn.Length)];
        string hihi_sound = $"Boy_hihi_{Random.Range(2, 4)}";
      //  AudioManager.instance.Play_SFX(hihi_sound, this.transform);
        GameObject obj = Instantiate(
            enemyPrefab[Random.Range(0, enemyPrefab.Length)],
            _home.position,
            Quaternion.identity
        );
        enemies.Add(obj);
        obj.GetComponent<EnemyContr>().home = _home; // Установка врагу домашнего адреса
    }
    public void LevelUp()
    {
        isSpawn = false;
        //  GetComponent<EnemyContr>().AtHome();
        EnemyContr[] enemies = FindObjectsOfType<EnemyContr>();
        AudioManager.instance.Play_SFX("Childrens_1", this.transform);
        AudioManager.instance.Play_SFX("Childrens_2", this.transform);
        foreach (var e in enemies)
        {
            e.AtHome();
        }
        StartCoroutine(Film());
    }
    IEnumerator Film()
    {
        yield return new WaitForSeconds(10);
        Boss.SetActive(true);
        //активация таймлинии
        //в таймлинию вставить маркер за движение игрока
        //

    }
    public void AgainSpawn()
    {
        isSpawn = true;
        stoppingDistance += 2; //Прокачка Карапузов

        StartCoroutine(Spawn());
        StartCoroutine(JanitorSpawn());
    }

    IEnumerator JanitorSpawn()
    {
        yield return new WaitForSeconds(timeToSpawnJanitor);
        if (isSpawn && Health.isAlive)
        {
            GameObject jan = Instantiate(janitor, janitorSpawn.position, Quaternion.identity);
            // jan.GetComponent<NavMeshAgent>().enabled=true;
        }
        JanitorSpawn();
    }
}
