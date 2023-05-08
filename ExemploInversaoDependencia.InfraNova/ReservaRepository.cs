using ExemploInversaoDependencia.Application.Connectors;
using ExemploInversaoDependencia.Domain;

namespace ExemploInversaoDependencia.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        public async Task<bool> Incluir(Reserva rsv)
        {
            return true;
        }
    }
}
