using Abp.Application.Services;
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
    public class CajaAppService : ApplicationService, ICajaAppService {
        private readonly ICajaManager _Manager;

        public CajaAppService(ICajaManager initManager) {
            _Manager = initManager;
        }

        public async Task Create(CreateCajaInput valInput) {
            Caja vRecord = Mapper.Map<CreateCajaInput, Caja>(valInput);            
            await _Manager.Create(vRecord);
        }

        public void Delete(DeleteCajaInput valInput) {
            _Manager.Delete(valInput.Id);
        }

        public CajaOutput GetById(CajaInput valInput) {
            Caja vRecord = _Manager.GetById(valInput.Id);
            CajaOutput vResult = Mapper.Map<Caja, CajaOutput>(vRecord);
            return vResult;

        }

        public IEnumerable<CajaOutput> ListAll() {            
            var vRecords = _Manager.GetAllList(AbpSession.GetAgenciaId()).ToList();
            List<CajaOutput> vResult = Mapper.Map<List<Caja>, List<CajaOutput>>(vRecords);
            return vResult;

        }

        public void Update(UpdateCajaInput valInput) {
            Caja vRecord = Mapper.Map<UpdateCajaInput, Caja>(valInput);
            _Manager.Update(vRecord);
        }
    }
}
