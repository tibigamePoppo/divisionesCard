public abstract class StateBase<T>
{
    public IngameView view;
    public virtual void Init(IngameView ingameView)
    {
        view = ingameView;
    }

    public virtual void StateAction(T arg1)
    {
    }
}
