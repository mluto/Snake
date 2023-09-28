using UnityEngine;

/// <summary>
/// Abstract class that needs to be implemented by any object that interacts with the snake's head.
/// These objects should be created as prefabs and placed in the Grid spawnObjeck list
/// </summary>
public abstract class ActionColision : MonoBehaviour
{
    protected GameMenager gameMenager;

    /// <summary>
    /// Get reference to the GameMenager and add the object to the grid
    /// </summary>
    public void GetRef(GameMenager gameRef)
    {
        gameMenager = gameRef;
        gameMenager.grid.AddToGrid(new Vector2Int((int)transform.position.x, (int)transform.position.y), this.gameObject);
    }

    /// <summary>
    /// Abstract method to define the action upon collision
    /// </summary>
    public abstract void Use();

    /// <summary>
    /// Add point to score and destroy the object after a delay
    /// </summary>
    protected void EffectObject()
    {
        gameMenager.ScoreAdd();
        Invoke("DestroyObject", gameMenager.delay);
    }

    /// <summary>
    /// Destroy the edible game object
    /// </summary>
    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}
