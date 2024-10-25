namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ISampleFormRepository SampleForm { get; }
        IFileFormRepository FileForm { get; }
        IMultiFileRepository MultiFile { get; }
        IMultiFileFormRepository MultiFileForm { get; }
        IApplicationUserRepository ApplicationUser { get; }
        Task Save();
    }
}