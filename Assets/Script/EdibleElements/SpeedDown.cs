using System.Threading.Tasks;

public class SpeedDown : ActionColision
{
    public override void Use()
    {
        _ = Slower();
    }

    public async Task Slower()
    {
        gameMenager.DecreaseSpeed();
        EffectObject();

        if (!gameMenager.overLimit)
        {
            gameMenager.overLimit = false;
            await Task.Delay(gameMenager.GetEffectDuration() * 1000);

            gameMenager.IncreaseSpeed();
        } 
    }
}
