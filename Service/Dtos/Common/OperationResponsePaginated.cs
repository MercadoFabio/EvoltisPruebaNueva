using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos.Common
{
    public class OperationResponsePaginated : OperationResponse
    {
        public OperationResponsePaginated(object data, long totalRows, int code = 200, bool success = true, string message = "La operación se ha ejecutado exitosamente.")
        {
            Data = data;
            Success = success;
            Message = message;
            Code = code;
            TotalRows = totalRows;
        }
        public OperationResponsePaginated(bool success = false, string message = "No se pudo completar la operación solicitada.", int code = 400)
        {
            Success = success;
            Message = message;
            Code = code;
        }
        public long TotalRows { get; set; }
    }
}
