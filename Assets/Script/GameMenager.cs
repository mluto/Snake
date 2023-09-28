using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    [HideInInspector] public float delay;
    [HideInInspector] public float reverseCount;
    [HideInInspector] public bool overLimit;
    public Grid grid;
    public Snake snake;

    [SerializeField] private SnakeMovement snakeMovement;
    [SerializeField] private TMP_Text textGridSize;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject panelGameUp;
    [SerializeField] private CinemachineVirtualCamera myCamera;
   

    [Header("Settings")]
    [SerializeField] private int minGridSize = 10;
    [SerializeField] private int maxGridSize = 30;
    [SerializeField] [Tooltip("Delay between snake movements in seconds")] private float normalMoveDelay = 0.5f;
    [SerializeField] [Tooltip("Speed ​​change value")] private float speedLeap = 0.2f;
    [SerializeField] [Tooltip("Min delay between snake movement in seconds")] private float minMoveDelay = 0.1f;
    [SerializeField] [Tooltip("Max delay between snake movement in seconds")] private float maxMoveDelay = 1.1f;
    [SerializeField] [Tooltip("Duration of effects from edible objects in seconds")] private int effectDuration = 10;

    private SpriteRenderer spriteRenderer;
    private int score;

    private void Start()
    {
        grid.gameObject.SetActive(false);
        panelGameUp.SetActive(true);
        panelGameOver.SetActive(false);  
        
        var size = (minGridSize + maxGridSize) /2;
        size = size % 2 == 0 ? size : size++;
        grid.SetSize(size);

        SchowGridSizeText();
        spriteRenderer = grid.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Public function that increases the grid size by 2 units.
    /// </summary>
    public void GridAdd()
    {
        if (grid.GetSize() >= maxGridSize)
        {
            grid.SetSize(maxGridSize);
            return;
        }

        grid.SetSize(grid.GetSize() + 2);
        SchowGridSizeText();
    }

    /// <summary>
    /// Public function that decreases the grid size by 2 units.
    /// </summary>
    public void GridDecres()
    {
        if (grid.GetSize() <= minGridSize)
        {
            grid.SetSize(minGridSize);
            return;
        }

        grid.SetSize(grid.GetSize() - 2);
        SchowGridSizeText();
    }

    /// <summary>
    /// Private function that updates the text displaying the grid size.
    /// </summary>
    private void SchowGridSizeText()
    {
        textGridSize.text = grid.GetSize().ToString();
    }

    /// <summary>
    /// Public function that starts the game, creating the grid, snake, adjusting the play field,
    /// and initiating the SnakeMovement.
    /// </summary>
    public void StartGame()
    {
        delay = normalMoveDelay;
        grid.CreatGrid();      

        var gridSize = grid.GetSize();
        var halfGridSize = gridSize / 2;
        Vector2 vec = new Vector2Int(halfGridSize, halfGridSize);

        snake.CreatSnakeHead(vec);

        Vector3 spritSize = new Vector3(gridSize, gridSize, 1);
        spriteRenderer.transform.localScale = spritSize;
        spriteRenderer.transform.localPosition = new Vector3((halfGridSize) -0.5f,(halfGridSize) -0.5f, 1f);

        grid.gameObject.SetActive(true);
        panelGameUp.SetActive(false);

        snakeMovement.StartGame();
    }

    /// <summary>
    /// Public function that sets the camera to follow a specified transform.
    /// </summary>
    public void CameraFallow(Transform fallowTransform)
    {
        myCamera.Follow = fallowTransform;
    }

    /// <summary>
    /// Public function that restarts the game.
    /// </summary>
    public void RestarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Public function that triggers game over by stopping the SnakeMovement, 
    /// hiding the play field, and showing the game over panel.
    /// </summary>
    public void GameOver()
    { 
        grid.gameObject.SetActive(false);
        panelGameOver.SetActive(true);
        snakeMovement.GameOver();
    }

    /// <summary>
    /// Add one point to score
    /// </summary>
    public void ScoreAdd()
    {
        score++;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Increase speed of snake
    /// </summary>
    public void IncreaseSpeed()
    {
        if (delay >= minMoveDelay + speedLeap)
        {
            delay -= speedLeap;
            overLimit = false;
        }
        else
        {
            overLimit = true;
        }
    }

    /// <summary>
    /// Decrease speed of snake
    /// </summary>
    public void DecreaseSpeed()
    {
        if (delay <= maxMoveDelay - speedLeap)
        {
            delay += speedLeap;
            overLimit = false;
        }
        else
        {
            overLimit = true;
        }
    }

    /// <summary>
    /// Get effectDuration
    /// </summary>
    public int GetEffectDuration()
    {
        return effectDuration;
    }
}
