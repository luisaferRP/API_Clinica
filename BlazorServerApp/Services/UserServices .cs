using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// using BlazorCrud.Shared;
using System.Net.Http.Json;
using BlazorServerApp.Models;
using MiApi.Models;

namespace BlazorServerApp.Services
{
    //Implementamos interfaz
    public class UserServices : IUserServices
    {
        //añadir servicio para hacer solicitudes http
        private readonly HttpClient _http;

        public UserServices (HttpClient http)
        {
            _http = http;
        }

        //implementtacio de metodos de la interfaz
        public async Task<List<User>> Lista()
        {
            var response = await _http.GetFromJsonAsync<ResponseofAPI<List<User>>>("/users/get");

            if (response?.EsCorrecto == true)
            {
                return response.Valor ?? new List<User>();
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Error desconocido al obtener la lista de usuarios.");
            }
        }


        public async Task<bool> Eliminar(int id)
        {
            //solicitud de eliminación a la API
            var result = await _http.DeleteAsync($"/user/eliminar/{id}");
            
            // Deserializamos la respuesta como<bool>
            var response = await result.Content.ReadFromJsonAsync<ResponseofAPI<bool>>();

            if (response?.EsCorrecto == true)
            {
                return response.EsCorrecto;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Error desconocido al eliminar el usuario.");
            }
        }

        public async Task<bool> Editar(User user)
        {
            // Se envía la tarea como JSON al endpoint de edición
            var result = await _http.PutAsJsonAsync($"/user/editar/{user.Id}", user);
            
            // Deserializamos la respuesta como ResponseofAPI<int> para manejar el estado de éxito
            var response = await result.Content.ReadFromJsonAsync<ResponseofAPI<int>>();

            if (response?.EsCorrecto == true)
            {
                return response.EsCorrecto;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Error desconocido al editar la tarea.");
            }
        }

        public async Task<User> Buscar(int id)
        {
            // Se hace la solicitud GET para obtener una tarea específica
            var result = await _http.GetFromJsonAsync<ResponseofAPI<User>>($"/user/find/{id}");

            if (result?.EsCorrecto == true)
            {
                return result.Valor;
            }
            else
            {
                throw new Exception(result?.Mensaje ?? "Error desconocido al obtener la tarea.");
            }
        }
    }
}