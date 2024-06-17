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

    public Health playerHealth;

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private TextMeshProUGUI EXPText;
    [SerializeField] private TextMeshProUGUI LvText;
    [SerializeField] private Image hpGauge;
    [SerializeField] private Image ExpGauge;
    [SerializeField] private GameObject gameOverUI;
    private float CurExp = 0;
    private float MaxExp = 3;
    private int CurLv = 1;

  [SerializeField] private int currentWaveIndex = 0;
    private int currentSpawnCount = 0;
    private int waveSpawnCount = 0;
    private int waveSpawnPosCount = 0;

    public float spawnInterval = 0.5f;
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

        for (int i = 0; i < spawnPositionsRoot.childCount; i++)
        {
            spawnPositions.Add(spawnPositionsRoot.GetChild(i));
        }
    }
    private void Start()
    {
        UpgradeStatInit();
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        while (true)
        {
            if (currentSpawnCount == 0) //현재 소환된 몬스터가 0이라면.
            {
                UpdateWaveUI(); //UI를 업데이트하도록 명령

                yield return new WaitForSeconds(2f);

                ProcessWaveConditions(); //웨이브 세팅

                yield return StartCoroutine(SpawnEnemiesInWave()); //몬스터 웨이브를 생성할 때 까지 기다림

                currentWaveIndex++; //현재 웨이브 인덱스를 +1
            }

            yield return null;
        }
    }

    void ProcessWaveConditions()
    {
        if (currentWaveIndex % 20 == 0) //몬스터들이 랜덤하게 업그레이드
        {
            RandomUpgrade();
        }

        if (currentWaveIndex % 10 == 0) //몬스터 생성 위치 증가
        {
            IncreaseSpawnPositions();
        }

        if (currentWaveIndex % 5 == 0) //보상이 늘어나고
        {
            IncreaseReward();
        }

        if (currentWaveIndex % 3 == 0) //몬스터 생성 숫자 증가
        {
            IncreaseWaveSpawnCount();
        }
    }

    IEnumerator SpawnEnemiesInWave()
    {
        for (int i = 0; i < waveSpawnPosCount; i++) // 스폰 포지션 카운트를 랜덤레인지로 생성
        {
            for (int j = 0; j < waveSpawnCount; j++)
            {
                SpawnEnemyAtPosition(); // 랜덤 포지션에 적을 생성
                yield return new WaitForSeconds(spawnInterval); //생성 간격만큼 대기
            }
        }
    }

    void SpawnEnemyAtPosition()
    {
        int posIdx = Random.Range(0, spawnPositions.Count);
        int prefabIdx = Random.Range(0, enemyPrefebs.Count); //적 리스트중 중 랜덤한 것을 뽑아옴
        GameObject enemy = Instantiate(enemyPrefebs[prefabIdx], spawnPositions[posIdx].position, Quaternion.identity); // 랜덤하게 뽑은 적 생성
        enemy.GetComponent<Health>().OnDie += OnEnemyDeath; //적이 죽는 함수를 구독함(0이 될 때 코루틴 실행 트리거)
        currentSpawnCount++; //스폰 카운트 증가
    }

    void IncreaseSpawnPositions()
    {
        waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPositions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
        // 웨이브 스폰 포지션 카운트에 +1을 했을때 스폰 포지션의 카운트보다 크다면 +하지 않음 (빈 자리가 있다면 올려라)
        waveSpawnCount = 0;
    }

    void IncreaseWaveSpawnCount() //웨이브를 1씩 늘려줌
    {
        waveSpawnCount += 1;
    }

    void UpgradeStatInit()
    {
    }

    private void RandomUpgrade()
    {
    }


    private void IncreaseReward()
    {
        // 5단계마다 리워드 얻음
    }

    private void OnEnemyDeath()
    {
        currentSpawnCount--;
        UpdateEXPUI();
    }

    private void UpdateHealthUI()
    {
        float amount = playerHealth.health / playerHealth.maxHealth;
        hpGauge.fillAmount = amount;
        HPText.text = $"HP\n{playerHealth.health}/{playerHealth.maxHealth}";
    }

    private void UpdateWaveUI()
    {
        waveText.text = (currentWaveIndex + 1).ToString();
    }

    private void UpdateEXPUI()
    {
        CurExp++;
        if(CurExp == MaxExp)
        {
            CurLv++;
            LvText.text = $"Lv. {CurLv}";
            MaxExp++;
            CurExp = 0;
        }
        ExpGauge.fillAmount = CurExp / MaxExp;
        EXPText.text = $"{CurExp} / {MaxExp}";
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //현재 씬을 확인후 그 씬을 다시 로드
    }

    // 버튼에 연결될 함수
    public void ExitGame()
    {
        Application.Quit();
    }
}
