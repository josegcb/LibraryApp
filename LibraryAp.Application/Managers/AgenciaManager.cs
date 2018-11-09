using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
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
    public class AgenciaManager : DomainService, IAgenciaManager {
        private readonly IRepository<Agencia> _Repository;
        public AgenciaManager(IRepository<Agencia> initRepository) {
            _Repository = initRepository;
            
        }
        [UnitOfWork]
        public async Task<Agencia> Create(Agencia valRecord) {
            var vAgencia = _Repository.FirstOrDefault(x => x.Id == valRecord.Id);
            if (vAgencia != null) {
                throw new UserFriendlyException("Ya Existe");
            } else {
                return await _Repository.InsertAsync(valRecord);
            }

        }

        public void Delete(Agencia valRecord) {
            //var vAgencia = _Repository.FirstOrDefault(x => x.Id == valId);
            if (valRecord != null) {
                _Repository.Delete(valRecord);
            } else {
                throw new UserFriendlyException("Ya no Existe");
            }
        }

        public IEnumerable<Agencia> GetAllList() {
            
            return _Repository.GetAll();
            
        }

        public Agencia GetById(int valId) {
            return _Repository.Get(valId);
        }

        public void Update(Agencia valRecord) {
            _Repository.Update(valRecord);
        }

        public IEnumerable<Agencia> GetAllByFilterList(List<EntityFilter> valFilter) {
            if (valFilter == null) {
                return _Repository.GetAll();
            } else {                
                var filter = new Filter<Agencia>();
                filter = FilterHelper.CreateFilters<Agencia>(valFilter);
                return _Repository.GetAll().Where(filter);
            }

        }
        
        



    }
}
