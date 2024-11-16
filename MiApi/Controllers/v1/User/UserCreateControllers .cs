using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//usarrusing API_Productos_Pedidos.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MiApi.Repositories;
using MiApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace MiApi.Controllers.v1.User
{
    [ApiController]
    [Route("api/v1/users")] 
    [Tags("users")]
    public class UserCreateControllers(IUserRepositories userRepositories) : UserControllers(userRepositories)
    {
       
        [HttpPost]
        // [Authorize]
        [SwaggerOperation(
            Summary = "Create a new users",
            Description ="This endpoint is for create a new users"
        )]
        [SwaggerResponse(201,"users created successfully")]
        [SwaggerResponse(400,"An error occurred")]
        public async Task<ActionResult<Models.User>> Create(Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseAPI<string>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = "Los datos son requeridos"
                });
            }
            try
            {
                var newUser = new Models.User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Rol = user.Rol
                };

                await userRepositories.Add(newUser);

                return Ok(new ResponseAPI<Models.User>
                {
                    EsCorrecto = true,
                    Valor = newUser,
                    Mensaje = "Usuario creado correctamente"

                });     
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI<string>
                {
                    EsCorrecto = false,
                    Valor = null,
                    Mensaje = $"Ocurrió un error: {ex.Message}"
                });
            }


        }

        
    }
}