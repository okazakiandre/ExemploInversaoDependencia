using ExemploInversaoDependencia.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExemploInversaoDependencia.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController : ControllerBase
    {
        private IMediator Mdtr { get; }
        public ReservaController(IMediator mdt)
        {
            Mdtr = mdt;
        }

        [HttpPost(Name = "IncluirReserva")]
        public async Task<IActionResult> Incluir(IncluirReservaCmd req)
        {
            var res = await Mdtr.Send(req);
            return Ok(res);
        }
    }
}
