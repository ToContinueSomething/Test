namespace Sources.Infrastructure.States
{
    public interface IPayLoadState<TPayLoad> : IExitableState 
    {
        void Enter(TPayLoad payLoad);
    }
}