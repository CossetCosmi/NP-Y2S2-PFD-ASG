using PFD.Model.Interface;
using System.Collections.Generic;

namespace PFD.Repository.Interface
{
    internal interface IRepository<MODEL, ID> where MODEL : IModel<ID>
    {
        List<MODEL> FindAll();
        MODEL FindById(ID id);
    }
}
