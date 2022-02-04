using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Common
{
    public class ClienteResponse
    {
     
            public int codigoCliente { get; set; }

          
            public string nombreCompleto { get; set; }

        
            public string nombreCorto { get; set; }
      
            public string abreviatura { get; set; }
      
            public string ruc { get; set; }

        
            public string estado { get; set; }

         
            public string? grupoFacturacion { get; set; }

       
            public DateTime? inactivoDesde { get; set; }
         
            public string? codigoSap { get; set; }
       
    }
}
