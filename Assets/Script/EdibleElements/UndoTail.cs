public class UndoTail : ActionColision
{
    public override void Use()
    {
        gameMenager.snake.SetDestroyTail(true);
        EffectObject();
    }
}
