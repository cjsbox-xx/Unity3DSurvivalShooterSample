using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5.0f;

    private const string GAME_OVER_TRIGGER = "GameOver";
    Animator anim;
    private float restartTimer;
    FMODUnity.StudioEventEmitter gameOverSnapshot;


    void Awake()
    {
        anim = GetComponent<Animator>();
        gameOverSnapshot = GetComponent<FMODUnity.StudioEventEmitter>();
        restartTimer = 0.0f;
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            if (!gameOverSnapshot.IsPlaying())
            {
                gameOverSnapshot.Play();
            }
            anim.SetTrigger(GAME_OVER_TRIGGER);
            restartTimer += Time.deltaTime;
            if (restartTimer >= restartDelay)
            {
                gameOverSnapshot.Stop();
                SceneManager.LoadScene(0);
            }
        }
    }
}
