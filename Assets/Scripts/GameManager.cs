using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //public ParticleSystem EffectParticle;

    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";

    private Health playerHealth;

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Image hpGauge;
    [SerializeField] private Image ExpGauge;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private int currentWaveIndex = 0;
    private int currentSpawnCount = 0;
    private int waveSpawnCount = 0;
    private int waveSpawnPosCount = 0;

    public float spawnInterval = .5f;
    public List<GameObject> enemyPrefebs = new List<GameObject>();

    [SerializeField] private Transform spawnPositionsRoot;
    private List<Transform> spawnPositions = new List<Transform>();

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        //EffectParticle = GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>();


        playerHealth = Player.GetComponent<Health>();
        playerHealth.OnDamage += UpdateHealthUI;
        playerHealth.OnHeal += UpdateHealthUI;
        playerHealth.OnDie += GameOver;

        // ������ġ�� �ϳ��ϳ� �� ����س��� ���� �ͺ��� ���� �θ� �Ʒ��� �ΰ� �θ��ϳ��� ����ؼ� ������ �ϴ°ſ���!
        for (int i = 0; i < spawnPositionsRoot.childCount; i++)
        {
            spawnPositions.Add(spawnPositionsRoot.GetChild(i));
        }
    }

    private void UpdateHealthUI()
    {
        hpGauge.fillAmount = playerHealth.health / playerHealth.maxHealth;

    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
    }
}
