using System;
using System.Collections.Generic;

namespace Cadastro.Domain.Interfaces.Base
{
    public interface IRepoBase<T> where T : class
    {
        T Add(T entity);
        T Get(Guid id);
        IEnumerable<T> GetAll();
    }
}
