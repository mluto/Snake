using UnityEngine;

public class Tail : ActionColision
{
    [SerializeField] public GameObject particle;

    public override void Use()
    {
        gameMenager.GameOver();
    }

    /// <summary>
    /// Public function that set active ParticleSystem on Snake Tail.
    /// </summary>
    public void Particle(bool play)
    {
        particle.SetActive(play);   
    }
}
