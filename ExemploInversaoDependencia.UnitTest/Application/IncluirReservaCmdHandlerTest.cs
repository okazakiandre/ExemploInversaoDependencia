using ExemploInversaoDependencia.Application.Commands;
using ExemploInversaoDependencia.Application.Connectors;
using ExemploInversaoDependencia.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExemploInversaoDependencia.UnitTest.Application.Commands
{
    [TestClass]
    [TestCategory("UnitTest > Incluir Reserva")]
    public class IncluirReservaCmdHandlerTest
    {
        [TestMethod]
        public async Task DeveriaIncluirReservaSeClienteExistir()
        {
            var mockRepo = new Mock<IReservaRepository>();
            mockRepo.Setup(m => m.Incluir(It.IsAny<Reserva>())).ReturnsAsync(true);
            var mockCli = new Mock<IClienteConsulta>();
            var cli = new Cliente
            { 
                Cpf = 1, 
                Nome = "teste cliente"
            };
            mockCli.Setup(m => m.ObterCliente(It.IsAny<long>())).ReturnsAsync(cli);
            var req = new IncluirReservaCmd(1, DateTime.Today.AddDays(6), DateTime.Today.AddDays(16));
            var hdl = new IncluirReservaCmdHandler(mockCli.Object, mockRepo.Object);

            var res = await hdl.Handle(req, new CancellationToken());

            Assert.IsTrue(res.Sucesso);
            mockCli.Verify(m => m.ObterCliente(cli.Cpf), Times.Once);
            mockRepo.Verify(m => m.Incluir(It.IsAny<Reserva>()), Times.Once);
        }

        [TestMethod]
        public async Task NaoDeveriaIncluirReservaSeClienteNaoExistir()
        {
            var mockRepo = new Mock<IReservaRepository>();
            var mockCli = new Mock<IClienteConsulta>();
            var req = new IncluirReservaCmd(1, DateTime.Today, DateTime.Today.AddDays(5));
            var hdl = new IncluirReservaCmdHandler(mockCli.Object, mockRepo.Object);

            var res = await hdl.Handle(req, new CancellationToken());

            Assert.IsFalse(res.Sucesso);
            mockCli.Verify(m => m.ObterCliente(It.IsAny<long>()), Times.Once);
            mockRepo.Verify(m => m.Incluir(It.IsAny<Reserva>()), Times.Never);
        }
    }
}
