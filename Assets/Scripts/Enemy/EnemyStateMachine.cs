
public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState _startingState)
    {
        currentState = _startingState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _nextState)
    {
        currentState.Exit();
        currentState = _nextState;
        currentState.Enter();
    }
}
