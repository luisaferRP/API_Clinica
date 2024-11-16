using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//usamos
using MiApi.Repositories;
using Microsoft.AspNetCore.Mvc;

// using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers.v1.User
{
    //inyectar el repositorio en el controlador en el contructor especificamente
    public class UserControllers(IUserRepositories userRepositories) : ControllerBase
    {
        //usamos el repositorio que necesitamos
        private readonly IUserRepositories _userRepositories;

    }
}