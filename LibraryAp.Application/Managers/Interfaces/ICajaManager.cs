using Abp.Domain.Services;
using LibraryAp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAp.Managers.Interfaces {
    public interface ICajaManager : IDomainService {
        IEnumerable<Caja> GetAllList(int AgenciaId);
        Caja GetById(int valId);
        Task<Caja> Create(Caja valRecord);
        void Update(Caja valRecord);
        void Delete(int valId);
    }
}
