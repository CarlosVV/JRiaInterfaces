namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IRepositoryFactory
    {
        T GetInstance<T>() where T : class;
    }
}