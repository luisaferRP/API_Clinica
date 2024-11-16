using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MiApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiApi.DTOS;
using MiApi.Config;

using MiApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApi.Controllers.v1.Auth
{
    [ApiController]
    [Route("api/v1/Auth/[controller]")]
    [Tags("Auth")]
    public class AuthController(ApplicationDbContex applicationDbContex, Utilities utilities) : ControllerBase
    {
        private readonly Utilities _utilities = utilities; 
        private readonly ApplicationDbContex _applicationDbContex = applicationDbContex;

        [HttpPost("Register")]
        public async Task<ActionResult<Models.User>> PostRegister([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }
            
            var user = new Models.User{
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = _utilities.EncryptSHA256(userDTO.Password),
                Rol = userDTO.Rol
            };

            await applicationDbContex.Users.AddAsync(user);
            await applicationDbContex.SaveChangesAsync();
            return Ok(user);
        
        }

        // login
        //ignore blazor token
        // [IgnoreAntiforgeryToken]
        [HttpPost("/Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }

            var userFound = await applicationDbContex.Users.FirstOrDefaultAsync(i => i.Email == userLoginDTO.Email);
            if (userFound == null)
            {
                return Unauthorized(new ResponseAPI<bool>{
                    EsCorrecto = false,
                    Valor = false,
                    Mensaje = "Email invalido"
                });
            }
               
            var contraseñap = _utilities.EncryptSHA256(userFound.Password);
            var passwordvalid = contraseñap == _utilities.EncryptSHA256(userLoginDTO.Password);

            if (passwordvalid == false)
            {
                 return Unauthorized(new ResponseAPI<bool>{
                    EsCorrecto = false,
                    Valor = false,
                    Mensaje = "Contraseña invalida"
                });
            }

            //Aqui llamamos el metodo para crear el jwt
            var token = _utilities.GenerateJwtToken(userFound); 

            return Ok(new ResponseAPI<string>{
                EsCorrecto = true,
                Valor = token,
                Mensaje = "Login exitoso"
            });
        }
    }
}