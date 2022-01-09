namespace Map.Cell.State
{
    public interface IState
    {
        bool IsClickable(GridCell cell);
    }
}
