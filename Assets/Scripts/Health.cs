using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Health : MonoBehaviour
{
    public float currentHealth;
    //  public float bossCurrentHealth;
    private bool isAlive;
    private Text healthText;
    private Text _bossText;
    private bool _noneDamage;
    PlayerAnim playerAnimator, bossAnimator;
    [SerializeField] private SkinnedMeshRenderer render;
    private Material _defaultMat;
    private Material _ghostMat;
    private Material[] mats;

    void Awake()
    {

        if (this.gameObject.CompareTag("Player"))
        {
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            healthText.text = $"Health: {currentHealth}";
        }
        else if (this.gameObject.CompareTag("Boss"))
        {
            _bossText = GameObject.Find("BossHealthText").GetComponent<Text>();
            _bossText.text = $"Health: {currentHealth}";

        }
        _noneDamage = false;
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<PlayerAnim>();
        
        mats = render.materials;
        _defaultMat = mats[0];
        _ghostMat = mats[2];

    }

    public void TakeDamage(float dmg)
    {
        if (!_noneDamage)

            currentHealth -= dmg;
        _noneDamage = true;


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            playerAnimator.Death();
            FindObjectOfType<PlayerInput>().isAlive = false; // запрет управления персонажем

            StartCoroutine(Death());
        }
        if (this.gameObject.CompareTag("Player"))
        {
            healthText.text = $"Health: {currentHealth.ToString()}";
        }
        else if (this.gameObject.CompareTag("Boss"))
        {
            _bossText.text = $"Health: {currentHealth.ToString()}";
        }
        render.material = _ghostMat; // замена дефолтного материала на полупрозрачный

        StartCoroutine(NonDamage());

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
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy")) TakeDamage(1);
        if (col.CompareTag("aidKit"))
        {
            Destroy(col.gameObject);
            currentHealth++;
            healthText.text = $"Health: {currentHealth}";
        }
    }


}
