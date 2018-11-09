using Abp.Application.Services;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using AutoMapper;
using LibraryAp.Dtos;
using LibraryAp.Managers.Interfaces;
using LibraryAp.Models;
using LibraryAp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace LibraryAp.Services {
    public class AgenciaAppService : ApplicationService, IAgenciaAppService {
        private readonly IAgenciaManager _Manager;

        public AgenciaAppService(IAgenciaManager initManager) {
            _Manager = initManager;            
        }
        
        public async Task Create(AgenciaCreateInput valInput) {
            Agencia vRecord = Mapper.Map<AgenciaCreateInput, Agencia>(valInput);
            await _Manager.Create(vRecord);
        }
        public void Delete(AgenciaDeleteInput valInput) {
            Agencia vRecord = Mapper.Map<AgenciaDeleteInput, Agencia>(valInput);
            _Manager.Delete(vRecord);
        }
        public AgenciaOutput GetById(AgenciaInput valInput) {
            Agencia vRecord = _Manager.GetById(valInput.Id);
            AgenciaOutput vResult = Mapper.Map<Agencia, AgenciaOutput>(vRecord);
            return vResult;
        }

        public void Update(AgenciaUpdateInput valInput) {
            Agencia vRecord = Mapper.Map<AgenciaUpdateInput, Agencia>(valInput);
            _Manager.Update(vRecord);
        }

        public IEnumerable<AgenciaOutput> ListAll() {            
            var vRecords = _Manager.GetAllList().ToList();            
            List<AgenciaOutput> vResult = Mapper.Map<List<Agencia>, List<AgenciaOutput>>(vRecords);
            return vResult;
        }

        public IEnumerable<AgenciaOutput> ListAllByFilter(List<EntityFilter> valFilter) {
            var vRecords = _Manager.GetAllByFilterList(valFilter).ToList();
            List<AgenciaOutput> vResult = Mapper.Map<List<Agencia>, List<AgenciaOutput>>(vRecords);
            return vResult;
        }
    }
}
