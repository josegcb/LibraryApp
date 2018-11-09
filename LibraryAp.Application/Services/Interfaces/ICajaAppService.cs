using Abp.Application.Services;
using LibraryAp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAp.Services.Interfaces {
    public interface ICajaAppService : IApplicationService {
        IEnumerable<CajaOutput> ListAll();
        Task Create(CreateCajaInput valInput);
        void Update(UpdateCajaInput valInput);
        void Delete(DeleteCajaInput valInput);
        CajaOutput GetById(CajaInput valInput);
    }
}
