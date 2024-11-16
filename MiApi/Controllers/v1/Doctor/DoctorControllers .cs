using MiApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers.v1.Doctor
{
    public class DoctorControllers(IDoctorRepositories doctorRepositories) : ControllerBase
    {
        private readonly IDoctorRepositories _doctorRepositories;
    }
}