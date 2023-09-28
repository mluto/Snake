using System.Threading.Tasks;

public class SpeedUp : ActionColision
{
    public override void Use()
    {
        _ = Faster();
    }

    public async Task Faster()
    {
        gameMenager.IncreaseSpeed();
        EffectObject();

        if (!gameMenager.overLimit)
        {
            gameMenager.overLimit = false;
            await Task.Delay(gameMenager.GetEffectDuration() * 1000);

            gameMenager.DecreaseSpeed();
        }
    }
}
