using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    public class ConcurrencyException : UserFriendlyException {
        public ConcurrencyException ()
            :base ("El registro fue modificado o eliminado por otro usuario") {
        }
    }
}
