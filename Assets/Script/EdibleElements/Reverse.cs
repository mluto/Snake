using System.Threading.Tasks;

public class Reverse : ActionColision
{
    public override void Use()
    {
        _ = Reversed();
    }

    public async Task Reversed()
    {
        gameMenager.snake.SetMoveReverse(true);
        gameMenager.reverseCount++;
        EffectObject();

        await Task.Delay(gameMenager.GetEffectDuration() * 1000);

        gameMenager.reverseCount--;
        if (gameMenager.reverseCount == 0)
        {
            gameMenager.snake.SetMoveReverse(false);
            EffectObject();
        }
    }
}
