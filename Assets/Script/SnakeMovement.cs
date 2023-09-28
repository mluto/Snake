using System.Collections;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private GameMenager gameMenager;

    private IEnumerator carutinGame;
    private Vector2Int direction;
    private Vector2Int newDirection = Vector2Int.right;
    private Vector2Int oldDirection;
    private Snake snake;
    private Grid grid;
    private bool gameRun = true;

    private void Start()
    {
        snake = gameObject.GetComponent<Snake>();
        grid = gameMenager.grid;
    }

    /// <summary>
    /// Function called every game frame, checks for pressed buttons and
    /// sets the new movement direction accordingly.
    /// </summary>
    /// 
    private void Update()
    {
        if (!Input.anyKey || !gameRun)
        {
            return;
        }

        if (Input.GetButtonDown("Up") )
        {
            newDirection = Vector2Int.up;
            return;
        }

        if (Input.GetButtonDown("Down"))
        {
            newDirection = Vector2Int.down;
            return;
        }

        if (Input.GetButtonDown("Left"))
        {
            newDirection = Vector2Int.left;
            return;
        }

        if (Input.GetButtonDown("Right"))
        {
            newDirection = Vector2Int.right;
            return;
        }

        CheckOldPosition();
    }

    /// <summary>
    /// Function that changes the position of the snake on the board if it goes beyond its boundaries.
    /// </summary> 
    private void ChangePosition(Vector2Int position)
    {
        if (position.x == -1)
        {
            snake.MoveSnake(new Vector2(grid.GetSize() - 1, (int)position.y));
        }
        else if (position.y == -1)
        {
            snake.MoveSnake(new Vector2(position.x, grid.GetSize() - 1));
        }
        else if (position.x == grid.GetSize())
        {
            snake.MoveSnake(new Vector2(0, position.y));
        }
        else  if (position.y == grid.GetSize())
        {
            snake.MoveSnake(new Vector2(position.x, 0));
        }
    }

    /// <summary>
    /// Function that checks if the new snake position is within the board boundaries
    /// </summary>
    private bool ChceckPositionInGrid(Vector2Int nextPosition)
    {
        if (nextPosition.x < 0 || nextPosition.x >= grid.GetSize() || nextPosition.y < 0 || nextPosition.y >= grid.GetSize())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Function that checks if the new movement direction is opposite to
    /// the previous one and updates the direction accordingly.
    /// </summary>
    private void CheckOldPosition()
    {
        if (oldDirection == -newDirection)
        {
            return;
        }
        else
        {
            direction = newDirection;
        }

        oldDirection = newDirection;
    }

    /// <summary>
    /// Enumerator for the game loop, performs snake movement and waits for a specified time.
    /// </summary>
    private IEnumerator GameCoroutine()
    {
        while (gameRun)
        {
            Move();

            yield return new WaitForSeconds(gameMenager.delay);

            if (gameRun)
            {
                grid.SpawnObiectPower();
            }          
        }
    }

    /// <summary>
    /// Function that executes the snake's movement based on the current direction.
    /// </summary>
    private void Move()
    {
        var temp = snake.GetSnakHeadPosition();
        var isRevers = snake.GetMoveReverse() ? -1 : 1;

        Vector2Int nextPosition = new Vector2Int((int)temp.x, (int)temp.y) + direction * isRevers;

        if (ChceckPositionInGrid(nextPosition))
        {
            snake.MoveSnake(nextPosition);
        }
        else
        {
            ChangePosition(nextPosition);
        } 
    }

    /// <summary>
    ///  Function that starts the game, runs GameCoroutine().
    /// </summary>
    public void StartGame()
    {
        carutinGame = GameCoroutine();
        StartCoroutine(carutinGame);
    }

    /// <summary>
    /// Function that ends the game, stops the GameCoroutine().
    /// </summary>
    public void GameOver()
    {
        gameRun = false;
        StopCoroutine(GameCoroutine());
    }
}
