using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameMenager gameMenager;
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private Transform snake;

    private Grid grid;
    private List<Tail> snakeBody = new List<Tail>();
    private bool firstReversedMove;
    private bool addTail = false;
    private bool destroyTail = false;
    private bool moveRevers = false;


    private void Start()
    {
        grid = gameMenager.grid;
    }

    /// <summary>
    /// Private function that converts a Vector2 to Vector2Int
    /// </summary>
    private Vector2Int ConwertToVector2Init(Vector2 vector)
    {
        return new Vector2Int((int)Math.Round(vector.x), (int)Math.Round(vector.y));
    }

    /// <summary>
    /// Public function that creates the snake's head at the specified position.
    /// </summary>
    public void CreatSnakeHead(Vector2 transform)
    {
        foreach (Transform child in snake)
        {
            DestroyImmediate(child.gameObject);
        }

        GameObject tempGameObject = Instantiate(snakeBodyPrefab, transform, Quaternion.identity, snake);
        snakeBody.Add(tempGameObject.GetComponent<Tail>());
        snakeBody.Last().GetRef(gameMenager);
        gameMenager.CameraFallow(tempGameObject.transform);
        
    }

    /// <summary>
    /// Public function that returns the position of the snake's head as a Vector2Int.
    /// </summary>
    public Vector2Int GetSnakHeadPosition()
    {
        if (moveRevers)
        {
            return ConwertToVector2Init(snakeBody.Last().transform.position);
        }
        else
        {
            return ConwertToVector2Init(snakeBody.First().transform.position);
        }
    }

    /// <summary>
    /// Public function that moves the snake forward to the given movePosition, updating the grid accordingly.
    /// </summary>
    public void MoveSnake(Vector2 movePosition)
    {
        grid.ChcekGridPosition(ConwertToVector2Init(movePosition));
        Vector2 prevPosition;

        if (moveRevers && !firstReversedMove)
        {
            firstReversedMove = false;
            prevPosition = snakeBody.Last().transform.position;
            gameMenager.CameraFallow(snakeBody.Last().transform);

            for (int i = snakeBody.Count - 1; i >= 0; i--)
            {
                if (i == snakeBody.Count - 1)
                {
                    MoveHead(i, prevPosition, movePosition);
                }
                else
                {
                    prevPosition = MoveTail(i, prevPosition);
                }
            }
        }
        else
        {
            firstReversedMove = !moveRevers;

            prevPosition = snakeBody[0].transform.position;
            gameMenager.CameraFallow(snakeBody[0].transform);

            for (int i = 0; i < snakeBody.Count; i++)
            {
                if (i == 0)
                {
                    MoveHead(i, prevPosition, movePosition);
                }
                else
                {
                    prevPosition = MoveTail(i, prevPosition);
                }
            }
        }

        if (addTail)
        {
            addTail = false;

            if (transform)
            {
                transform.SetAsLastSibling();
            }

            GameObject tempGameObject = Instantiate(snakeBodyPrefab, prevPosition, Quaternion.identity, transform);

            grid.AddToGrid(ConwertToVector2Init(prevPosition), tempGameObject);

            if (moveRevers)
            {
                snakeBody.Insert(0, tempGameObject.GetComponent<Tail>());
                snakeBody.First().GetRef(gameMenager);
            }
            else
            {
                snakeBody.Add(tempGameObject.GetComponent<Tail>());
                snakeBody.Last().GetRef(gameMenager);
            }
        }

        if (destroyTail)
        {
            destroyTail = false;
            if (snakeBody.Count > 1)
            {
                if (moveRevers)
                {
                    Destroy(snakeBody.First().gameObject);
                    snakeBody.RemoveAt(0);
                }
                else
                {
                    Destroy(snakeBody.Last().gameObject);
                    snakeBody.RemoveAt(snakeBody.Count - 1);
                }
            }
        }

        ShowParticle();
    }

    /// <summary>
    /// Private function that move snake's head.
    /// </summary>
    private void MoveHead(int i, Vector2 prevPosition, Vector2 movePosition)
    {
        grid.CleanGridPosition(ConwertToVector2Init(prevPosition));
        snakeBody[i].transform.position = movePosition;
        grid.AddToGrid(ConwertToVector2Init(movePosition), snakeBody[i].gameObject);
    }

    /// <summary>
    /// Private function that move snake's tail.
    /// </summary>
    private Vector3 MoveTail(int i, Vector2 prevPosition)
    {
        grid.CleanGridPosition(ConwertToVector2Init(snakeBody[i].transform.position));
        Vector3 currentPosition = snakeBody[i].transform.position;
        snakeBody[i].transform.position = prevPosition;
        grid.AddToGrid(ConwertToVector2Init(prevPosition), snakeBody[i].gameObject);
        return currentPosition;
    }

    /// <summary>
    /// Private function that show particle on snake's tail.
    /// </summary>
    private void ShowParticle()
    {
        foreach (Tail tail in snakeBody)
        {
            tail.Particle(moveRevers);
        }
    }

    /// <summary>
    /// Public function that set addTail.
    /// </summary>
    public void SetAddTail(bool status)
    {
        addTail = status;
    }

    /// <summary>
    /// Public function that set destroyTail.
    /// </summary>
    public void SetDestroyTail(bool status)
    {
        destroyTail = status;
    }

    /// <summary>
    /// Public function that set moveRevers.
    /// </summary>
    public void SetMoveReverse(bool status)
    {
        moveRevers = status;
    }

    /// <summary>
    /// Public function that return moveRevers.
    /// </summary>
    public bool GetMoveReverse()
    {
        return moveRevers;
    }
}
