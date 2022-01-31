using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health;

    public static float currentHealth;
    //  public float bossCurrentHealth;
    public static bool isAlive = true;
    
    private Text healthText;

    private Text _bossText;

    private bool _noneDamage;

    PlayerAnim playerAnimator;

    [SerializeField]
    private SkinnedMeshRenderer render;

    private Material _defaultMat;

    private Material _ghostMat;

    private Material[] mats;

    void Awake()
    {
        //healthText = GameObject.Find("HealthText").GetComponent<Text>();
        //healthText.text = $"Health: {currentHealth}";
        currentHealth = _health;
        _noneDamage = false;
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<PlayerAnim>();

        mats = render.materials;
        _defaultMat = mats[0];
        _ghostMat = mats[2];
    }

    public void TakeDamage(float dmg)
    {
        if (!_noneDamage && isAlive)
        {
            string soundName = $"Pl_Dam_{Random.Range(1, 3)}";
            AudioManager.instance.Play_SFX(soundName, this.transform);
            currentHealth -= dmg;
            _noneDamage = true;

            /// Death
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isAlive = false;
                AudioManager.instance.Play_SFX("Player_dead", this.transform);
                AudioManager.instance.GameOverMusic();
                playerAnimator.Death();
                FindObjectOfType<PlayerInput>().isAlive = false; // запрет управления персонажем

                StartCoroutine(Death());
            }

          //  healthText.text = $"Health: {currentHealth.ToString()}";
            render.material = _ghostMat; // замена дефолтного материала на полупрозрачный

            StartCoroutine(NonDamage());
        }
    }
    IEnumerator NonDamage()
    {
        yield return new WaitForSeconds(3f);
        render.material = _defaultMat; //смена материала на дефолтный
        _noneDamage = false;
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<GameManager>().GameOver();
        AudioManager.instance.PlayFinalMusic();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
            TakeDamage(1);
        if (col.CompareTag("aidKit"))
        {
            //collect sound
            AudioManager.instance.Play_SFX("Kit_Collect", this.transform);
            AudioManager.instance.Play_SFX("collect", this.transform);
            Destroy(col.gameObject);
            currentHealth++;
         //   healthText.text = $"Health: {currentHealth}";
        }
    }
}
