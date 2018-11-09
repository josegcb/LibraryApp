using Abp.Domain.Services;
using Lib;
using LibraryAp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAp.Managers.Interfaces {
    public interface IAgenciaManager : IDomainService {
        IEnumerable<Agencia> GetAllList();
        Agencia GetById(int valId);
        Task<Agencia> Create(Agencia valRecord);
        void Update(Agencia valRecord);
        void Delete(Agencia valRecord);
        IEnumerable<Agencia> GetAllByFilterList(List<EntityFilter> valFilter);
    }
}
