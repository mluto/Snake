public class AddTail : ActionColision
{
    public override void Use()
    {
        gameMenager.snake.SetAddTail(true);
        EffectObject();
    }
}
