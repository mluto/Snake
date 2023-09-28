using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameMenager gameMenager;
    [SerializeField] private Transform edibleElements;

    [Header("Settings")]
    [SerializeField] private int spawnInterval = 5;
    [SerializeField] private ActionColision[] spawnObjeck;

    private GameObject[,] grid;
    private int countGrid; 
    private bool isOccupied;
    private int gridSize;

    /// <summary>
    /// Private function that spawns a game object at a random unoccupied grid position.
    /// </summary>
    private void SpawnObject()
    {
        int randx = Random.Range(0, spawnObjeck.Length);
        int randgridX;
        int randgridY;

        while (isOccupied)
        {
            randgridX = Random.Range(0, grid.GetLength(0));
            randgridY = Random.Range(0, grid.GetLength(1));
            
            if (grid[randgridX, randgridY] == null)
            {
                countGrid = 0;
                isOccupied = false;

                GameObject tempObject = Instantiate(spawnObjeck[randx].gameObject, edibleElements);
                tempObject.transform.position = new Vector2(randgridX, randgridY);
                tempObject.GetComponent<ActionColision>().GetRef(gameMenager);
                AddToGrid(new Vector2Int(randgridX, randgridY), tempObject);      
            }
        }
    }

    /// <summary>
    /// Public function that checks the grid position for any collision and triggers the associated action.
    /// </summary>
    public void ChcekGridPosition(Vector2Int position)
    {
        ActionColision tempAction;

        if (grid[position.x,position.y] != null)
        {
            tempAction = grid[position.x,position.y].gameObject.GetComponent<ActionColision>();          
            tempAction.Use();
        }
    }

    /// <summary>
    /// Public function that adds a game object to the grid at the specified grid position.
    /// </summary>
    public void AddToGrid(Vector2Int positionGrig, GameObject snakeTransform)
    {
        grid[positionGrig.x, positionGrig.y] = snakeTransform;
    }

    /// <summary>
    /// Public function that cleans the grid position by removing the game object reference.
    /// </summary>
    public void CleanGridPosition(Vector2Int positionGrig)
    {
        grid[positionGrig.x, positionGrig.y] = null;
    }

    /// <summary>
    /// Public function that creates the grid with the specified size.
    /// </summary>
    public void CreatGrid()
    {
        grid = new GameObject[gridSize, gridSize];
    }

    /// <summary>
    /// Public function that returns the size of the grid.
    /// </summary>
    public int GetSize()
    {
        return gridSize;
    }

    /// <summary>
    /// Public function that set the size of the grid.
    /// </summary>
    public void SetSize(int size)
    {
        gridSize = size;
    }

    /// <summary>
    /// Public function that triggers the spawning of edible object at regular intervals.
    /// </summary>
    public void SpawnObiectPower()
    {
        countGrid++;

        if (countGrid >= spawnInterval)
        {
            isOccupied = true;
            SpawnObject();
        }
    }
}
