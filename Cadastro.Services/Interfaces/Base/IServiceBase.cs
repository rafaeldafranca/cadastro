using System;
using System.Collections.Generic;

namespace Cadastro.Services.Interfaces.Base
{
    public interface IServiceBase<T> where T : class
    {
        T Add(T user);
        IEnumerable<T> GetAll();
        T GetbyId(Guid Id);
    }
}
