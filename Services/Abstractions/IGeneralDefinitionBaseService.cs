using Multiple.Models;
using Multiple.Models.Abstractions;

namespace Multiple.Services.Abstractions
{
    public interface IGeneralDefinitionBaseService<TViewModel, TDatabaseModel> : IBaseService<TViewModel, TDatabaseModel>
        where TViewModel : IViewModel
        where TDatabaseModel : BaseEntity
    {
        ServiceResponse<TViewModel> GetByName(string name);
    }
}
