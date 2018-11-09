using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Lib;
using LibraryAp.Managers.Interfaces;
using LibraryAp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAp.Managers {
    public class CajaManager : DomainService, ICajaManager {
        private readonly IRepository<Caja> _Repository;
        public CajaManager(IRepository<Caja> initRepository) {
            _Repository = initRepository;
        }

        public async Task<Caja> Create(Caja valRecord) {
            var vCaja = _Repository.FirstOrDefault(x => x.Id == valRecord.Id);
            if (vCaja != null) {
                throw new UserFriendlyException("Ya Existe");
            } else {
                return await _Repository.InsertAsync(valRecord);
            }

        }

        public void Delete(int valId) {
            var vCaja = _Repository.FirstOrDefault(x => x.Id == valId);
            if (vCaja != null) {
                _Repository.Delete(vCaja);
            } else {
                throw new UserFriendlyException("Ya no Existe");
            }
        }

        public IEnumerable<Caja> GetAllList(int AgenciaId) {
            if (!CurrentUnitOfWork.IsFilterEnabled(DataFilters.MustHaveAgency)) {
                CurrentUnitOfWork.EnableFilter(DataFilters.MustHaveAgency);
            }
            using (CurrentUnitOfWork.SetFilterParameter(DataFilters.MustHaveAgency, 
                DataFilters.MustHaveAgencyParam, AgenciaId)) {
                return _Repository.GetAll();
            }
        }

        public Caja GetById(int valId) {
            return _Repository.Get(valId);
        }

        public void Update(Caja valRecord) {
            _Repository.Update(valRecord);
        }
    }
}
