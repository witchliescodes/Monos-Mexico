namespace UNAM.PrimatesApi.Interfaces
{
    public interface IDbInitializer
    {
        Task InitRoles();
        Task InitApp();
    }
}
