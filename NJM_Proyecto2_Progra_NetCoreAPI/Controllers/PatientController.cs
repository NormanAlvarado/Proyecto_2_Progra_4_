using Microsoft.AspNetCore.Mvc;

namespace NJM_Proyecto2_Progra_NetCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        [HttpGet]
        [Route("list")]
        public dynamic ListPatient()
        {
            // Lógica para obtener la lista de pacientes
        }

        [HttpPost]
        [Route("save")]
        public dynamic SavePatient()
        {
            // Lógica para guardar un nuevo paciente
        }

        [HttpPut]
        [Route("update")]
        public dynamic UpdatePatient()
        {
            // Lógica para actualizar un paciente existente
        }

        [HttpDelete]
        [Route("delete")]
        public dynamic DeletePatient()
        {
            // Lógica para eliminar un paciente
        }
    }
}
