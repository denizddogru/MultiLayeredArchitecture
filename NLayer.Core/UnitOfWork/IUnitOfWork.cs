namespace NLayer.Core.UnitOfWork
{
    internal interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
