using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Models
{
    //permite deserializar las respuestas recibidas de la API.
    public class ResponseofAPI<T>
    {
       public bool EsCorrecto { get; set; }
        public T? Valor { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}