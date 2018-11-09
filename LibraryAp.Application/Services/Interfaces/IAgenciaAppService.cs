using Abp.Application.Services;
using Lib;
using LibraryAp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAp.Services.Interfaces {
    public interface IAgenciaAppService : IApplicationService {
        IEnumerable<AgenciaOutput> ListAll();
        Task Create(AgenciaCreateInput valInput);
        void Update(AgenciaUpdateInput valInput);
        void Delete(AgenciaDeleteInput valInput);
        AgenciaOutput GetById(AgenciaInput valInput);
        IEnumerable<AgenciaOutput> ListAllByFilter(List<EntityFilter> valFilter);
    }
}
