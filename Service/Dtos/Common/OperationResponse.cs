using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos.Common
{
    public class OperationResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public int? Code { get; set; }
        public object? Data { get; set; }

        public OperationResponse(List<String> errors, bool success = false, string message = "No se pudo completar la operación solicitada.", int code = 400)
        {
            Success = success;
            Message = message;
            Errors = errors;
            Code = code;
        }
        public OperationResponse(int code, string message = "Ha ocurrido un error interno en el servidor. Por favor, inténtelo de nuevo más tarde.", bool success = false)
        {
            Success = success;
            Message = message;
            Code = code;
        }

        public OperationResponse(object? data, bool success = true, string message = "Operación ejecutada con éxito.", int code = 200)
        {
            Success = success;
            Message = message;
            Data = data;
            Code = code;
        }


        public OperationResponse(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public OperationResponse()
        {
        }
    }
}
