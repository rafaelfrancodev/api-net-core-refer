using System.Threading.Tasks;

namespace DEV.Api.App.Application.Interfaces.Base
{
    public interface IAppServiceBase<TViewModel, TInput, TKey>
       where TViewModel : class
       where TInput : class
       where TKey : struct
    {
        Task<TViewModel> InsertAsync(TInput input);
        Task<TViewModel> UpdateAsync(TKey id, TInput obj);
        Task<bool> DeleteAsync(TKey id);
        Task<TViewModel> GetByIdAsync(TKey id);
    }
}
