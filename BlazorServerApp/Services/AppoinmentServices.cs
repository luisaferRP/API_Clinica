using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using MiApi.Models;
using BlazorServerApp.Models;

namespace BlazorServerApp.Services
{
    public class AppoinmentServices : IAppoinmentServices
    {
        private readonly HttpClient _http;

        public AppoinmentServices (HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Appointment>> Lista()
        {
            
            var result = await _http.GetFromJsonAsync<ResponseofAPI<List<Appointment>>>("/appoitment/get");

            if(result?.EsCorrecto == true)
            {
                return result.Valor ?? new List<Appointment>();
            }else
            {
                throw new Exception(result?.Mensaje ?? "Error desconocido.");
            }
        }

        public async Task<Appointment> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseofAPI<Appointment>>($"/appoitment/find/{id}");

            if (result?.EsCorrecto == true)
            {
                return result.Valor;
            }
            else
            {
                throw new Exception(result?.Mensaje ?? "Error desconocido.");
            }
        }
        public async Task<Appointment> Guardar(Appointment appointment)
        {
             var result = await _http.PostAsJsonAsync($"/appointment/create", appointment);
            
            var response = await result.Content.ReadFromJsonAsync<ResponseofAPI<Appointment>>();
            if (response?.EsCorrecto == true)
            {
                return response.Valor;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Error desconocido al guardar.");
            }
        }

        public async Task<int> Editar(Appointment Appointment)
        {
            var result = await _http.PutAsJsonAsync($"/appointment/update/{Appointment.Id}", Appointment);
            
            // Deserializamos la respuesta como ResponseofAPI<int> para manejar el estado de éxito
            var response = await result.Content.ReadFromJsonAsync<ResponseofAPI<int>>();

            if (response?.EsCorrecto == true)
            {
                return response.Valor;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Error desconocido .");
            }
        }

        public async  Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"/appointments/delete/{id}");
            // var response = await result.Content.ReadFromJsonAsync<ResponseAPI<List<Tareas>>>
            var response = await result.Content.ReadFromJsonAsync<ResponseofAPI<bool>>();

            if(response?.EsCorrecto == true)
            {
                return response.EsCorrecto;
            }else
            {
               throw new Exception(response?.Mensaje ?? "Error desconocido al eliminar el usuario.");
            }
        }


        
    }
}