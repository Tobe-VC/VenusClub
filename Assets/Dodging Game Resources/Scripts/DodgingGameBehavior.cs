using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DodgingGameBehavior : MonoBehaviour
{
    public GameObject player;

    public float speed;

    public Image portrait;

    public SpriteRenderer background;

    private bool spawnedNextPortrait = false;

    public DodgingGameObstaclesSpawnerBehavior[] obstaclesSpawners;

    private float timeLeftBeforeSpawn;

    public float spawnTimer;

    public List<GameObject>obstaclesPrefabs;

    public GameObject videoTokenPrefab;

    public List<float> correspondingChanceToSpawn;

    public GameObject gameOverPopup;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timeText;

    private float timer = GMGlobalNumericVariables.gnv.DATE_GAME_TIME;

    [HideInInspector]
    public bool gameIsOver = false;

    public bool videoObstacleIsPresent = false;

    private int score = 0;
    private int health = GMGlobalNumericVariables.gnv.DATE_GAME_MAX_HP;

    private int currentScoreStreak = 0;

    /// <summary>
    /// The index in the obstaclesPrefab of the basic obstacle
    /// </summary>
    [Tooltip("The index in the obstaclesPrefab of the basic obstacle")]
    public int basicObstacleIndex;

    /// <summary>
    /// The index in the obstaclesPrefab of the next portrait obstacle
    /// </summary>
    [Tooltip("The index in the obstaclesPrefab of the next portrait obstacle")]
    public int nextPortraitObstacleIndex;

    /// <summary>
    /// The index in the obstaclesPrefab of the next video obstacle
    /// </summary>
    /*[Tooltip("The index in the obstaclesPrefab of the video obstacle")]
    public int videoObstacleIndex;
    */

    /// <summary>
    /// The index in the obstaclesPrefab of the basic score increase obstacle
    /// </summary>
    [Tooltip("The index in the obstaclesPrefab of the basic score increase obstacle")]
    public int scoreIncreaseObstacleIndex;

    public GameObject betterScorePrefab;

    public float betterScoreChanceToSpawn;

    public bool timeFreeze = false;

    private bool timeFreezeUsed = false;

    private float freezeTimeLeft = 2.0f;

    private bool nextVideoUnlocked = false;

    private bool lastDressTokenTaken = false;

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static DodgingGameBehavior s_Instance = null;

    // A static property that finds or creates an instance of the manager object and returns it.
    public static DodgingGameBehavior instance
    {
        get
        {
            if (s_Instance == null)
            {
                // FindObjectOfType() returns the first GMClubData object in the scene.
                s_Instance = FindObjectOfType(typeof(DodgingGameBehavior)) as DodgingGameBehavior;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("DodgingGameBehavior");
                s_Instance = obj.AddComponent<DodgingGameBehavior>();
            }

            return s_Instance;
        }
    }


    /// <summary>
    /// The list of portraits for this date
    /// </summary>
    public List<Sprite> portraitsToPlay;

    /// <summary>
    /// The index of the portrait in the above array
    /// </summary>
    public int portraitNumber;

    private void Awake()
    {
        portrait.sprite = RegisterDates.currentDate.portraits[RegisterDates.currentDate.currentPortraitLevel];
        background.sprite = RegisterDates.currentDate.backgrounds[RegisterDates.currentDate.currentBackgroundLevel];
        portraitsToPlay = RegisterDates.currentDate.portraits;
        portraitNumber = RegisterDates.currentDate.currentPortraitLevel;

        health = GMGlobalNumericVariables.gnv.DATE_GAME_MAX_HP;
    }

    public void GameOver(string displayedMessage, int earnedPoints)
    {
        RegisterDates.currentDate.timesDone++;
        gameIsOver = true;
        gameOverPopup.SetActive(true);
        gameOverPopup.GetComponentInChildren<Text>().text = displayedMessage;
            StaticAssistantData.data.assistantPoints += earnedPoints;
        
    }

    public void NextPortrait()
    {
        if (portraitNumber < portraitsToPlay.Count - 1)
        {
            RegisterDates.currentDate.currentPortraitLevel++;
            portraitNumber++;
            portrait.sprite = portraitsToPlay[portraitNumber];
        }
        if (portraitNumber == RegisterDates.currentDate.maxPortraitLevel - 1)
        {
            lastDressTokenTaken = true;
        }
    }

   /// <summary>
   /// What happens when the player collides with a video token
   /// </summary>
    public void VideoTokenCollision()
    {
        RegisterDates.currentDate.currentVideoTokenCollected++;
        if(RegisterDates.currentDate.currentVideoTokenCollected >= RegisterDates.currentDate.videoLevel + 1)
        {
            RegisterDates.currentDate.videoLevel++;
            RegisterDates.currentDate.currentVideoTokenCollected = 0;
            nextVideoUnlocked = true;
            videoObstacleIsPresent = false;
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        healthText.text = health.ToString();

        if (!gameIsOver)
        {
            int seconds = System.TimeSpan.FromSeconds(timer).Seconds;
            timeText.text = seconds.ToString();

        if (timeLeftBeforeSpawn <= 0f)
            {
                DodgingGameObstaclesSpawnerBehavior spawner = obstaclesSpawners[Random.Range(0, obstaclesSpawners.Length)];

                if (currentScoreStreak >= GMGlobalNumericVariables.gnv.DATE_GAME_SCORE_STREAK_TO_REACH_FOR_VIDEO_TOKEN
                    && !nextVideoUnlocked && !videoObstacleIsPresent && RegisterDates.currentDate.videoLevel < RegisterDates.currentDate.maxVideoLevel &&
                    RegisterDates.currentDate.currentPortraitLevel >= RegisterDates.currentDate.maxPortraitLevel - 1 
                    && !lastDressTokenTaken)
                {
                    spawner.SpawnObstacle(videoTokenPrefab);
                    videoObstacleIsPresent = true;
                    ResetCurrentScoreStreak();
                }
            else
                {
                    //If the value here is lesser or equal to the chance of spawn, then spawn it, otherwise spawn a basic one
                    float spawnObstacle = Random.Range(0f, 1f);
                    int indexToSpawn = Random.Range(1, obstaclesPrefabs.Count);
                    GameObject obstacleToSpawn = obstaclesPrefabs[indexToSpawn];

                    if (indexToSpawn == nextPortraitObstacleIndex)
                    {
                        if (spawnedNextPortrait)
                            spawner.SpawnObstacle(obstaclesPrefabs[basicObstacleIndex]);
                        else
                        {
                            if (spawnObstacle <= correspondingChanceToSpawn[indexToSpawn] &&
                                    RegisterDates.currentDate.currentPortraitLevel < RegisterDates.currentDate.maxPortraitLevel - 1)
                            {
                                spawnedNextPortrait = true;
                                spawner.SpawnObstacle(obstacleToSpawn);
                            }
                            else
                            {
                                spawner.SpawnObstacle(obstaclesPrefabs[basicObstacleIndex]);
                            }
                        }
                    }
                    /*
                    else if (indexToSpawn == videoObstacleIndex)
                    {
                        if (RegisterDates.currentDate.currentPortraitLevel >= RegisterDates.currentDate.maxPortraitLevel - 1
                            && spawnObstacle <= correspondingChanceToSpawn[indexToSpawn] && !spawnedNextVideo && !spawnedNextPortrait)
                        {
                            spawner.SpawnObstacle(obstacleToSpawn);
                            spawnedNextVideo = true;
                        }
                        else
                        {
                            spawner.SpawnObstacle(obstaclesPrefabs[basicObstacleIndex]);
                        }
                    }
                    */
                    else if (indexToSpawn == scoreIncreaseObstacleIndex)
                    {
                        //If the obstacle is a score increase
                        if (StaticBooleans.dateGameBetterScoreIncreaseBought && Random.Range(0f, 1f) <= betterScoreChanceToSpawn)
                        {
                            //If the better score is activated and the random is correct, spawn a better score increase
                            spawner.SpawnObstacle(betterScorePrefab);
                        }
                        else
                        {
                            //If not, spawn a basic one
                            spawner.SpawnObstacle(obstacleToSpawn);
                        }

                    }
                    else if (spawnObstacle <= correspondingChanceToSpawn[indexToSpawn])
                    {
                        spawner.SpawnObstacle(obstacleToSpawn);
                    }
                    else
                    {
                        spawner.SpawnObstacle(obstaclesPrefabs[basicObstacleIndex]);
                    }

                    timeLeftBeforeSpawn = spawnTimer;
                }
        }
        else
        {
            timeLeftBeforeSpawn -= Time.deltaTime;
        }

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (score < GMGlobalNumericVariables.gnv.MINIMUM_POINTS_EARNED_PER_DATE)
                    score = GMGlobalNumericVariables.gnv.MINIMUM_POINTS_EARNED_PER_DATE;
                GameOver("Time is up!\nYou earned " + score + " points.", score);
            }
            if(score >= GMGlobalNumericVariables.gnv.DATE_GAME_MAX_SCORE)
            {
                GameOver("You reached 30 gazes! Good job!\n You earned " + score + " points.", score);
            }
        }

        if (Input.GetButtonDown("StopTime") && StaticBooleans.dateGameFreezeTimeUnlocked && !timeFreeze && !timeFreezeUsed)
        {
            timeFreeze = true;
            timeFreezeUsed = true;
        }

        if (timeFreeze)
        {
            if (freezeTimeLeft <= 0)
            {
                timeFreeze = false;
            }
            else
            {
                freezeTimeLeft -= Time.deltaTime;
            }
        }
    }

    public void IncreaseScore(int value)
    {
        score += value;
        currentScoreStreak += value;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetCurrentScoreStreak()
    {
        currentScoreStreak = 0;
    }

    /// <summary>
    /// What happens when the player collides with an obstacle they should avoid (damaging ones)
    /// </summary>
    /// <param name="healthLost">The amount of health lost</param>
    public void DamagingCollision(int healthLost)
    {
        health -= healthLost;
        ResetCurrentScoreStreak();
    }

    public int GetHealth()
    {
        return health;
    }

}
