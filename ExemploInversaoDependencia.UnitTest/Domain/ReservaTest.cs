using ExemploInversaoDependencia.Domain;
using ExemploInversaoDependencia.Domain.SeedWork.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExemploInversaoDependencia.UnitTest.Domain
{
    [TestClass]
    [TestCategory("UnitTest > Domain")]
    public class ReservaTest
    {
        [TestMethod]
        public void DeveriaCriarNovaReserva()
        {
            var entrada = DateTime.Today.AddDays(10);
            var saida = DateTime.Today.AddDays(15);

            var res = Reserva.CriarNova(entrada, saida, 1, "cliente");

            Assert.AreEqual(entrada, res.DataEntrada);
            Assert.AreEqual(saida, res.DataSaida);
            Assert.AreEqual(1, res.StatusReserva);
            Assert.IsTrue(res.UltimaAtualizacao > DateTime.MinValue);
        }

        [TestMethod]
        public void NaoDeveriaCriarNovaReservaComEntradaSemAntecedencia()
        {
            var entrada = DateTime.Today.AddDays(4);
            var saida = DateTime.Today.AddDays(15);

            var exc = Assert.ThrowsException<DomainValidationException>(() => Reserva.CriarNova(entrada, saida, 1, "cliente"));

            Assert.AreEqual("A reserva deve ser feita com 5 dias de antecedência.", exc.Message);
        }

        [TestMethod]
        public void NaoDeveriaCriarNovaReservaComEstadiaMenorQue3Dias()
        {
            var entrada = DateTime.Today.AddDays(6);
            var saida = DateTime.Today.AddDays(8);

            var exc = Assert.ThrowsException<DomainValidationException>(() => Reserva.CriarNova(entrada, saida, 1, "cliente"));

            Assert.AreEqual("A estadia mínima é de 3 dias.", exc.Message);
        }

        [TestMethod]
        public void NaoDeveriaCriarNovaReservaComEntradaPosteriorASaida()
        {
            var entrada = DateTime.Today.AddDays(8);
            var saida = DateTime.Today.AddDays(6);

            var exc = Assert.ThrowsException<DomainValidationException>(() => Reserva.CriarNova(entrada, saida, 1, "cliente"));

            Assert.AreEqual("A data de entrada deve ser anterior à data de saída.", exc.Message);
        }

        [TestMethod]
        public void NaoDeveriaCriarNovaReservaComMaisDe100DiasDeEstadia()
        {
            var entrada = DateTime.Today.AddDays(6);
            var saida = DateTime.Today.AddDays(107);

            var exc = Assert.ThrowsException<DomainValidationException>(() => Reserva.CriarNova(entrada, saida, 1, "cliente"));

            Assert.AreEqual("A estadia máxima é de 100 dias.", exc.Message);
        }

        [TestMethod]
        public void DeveriaCriarNovaReservaCom100DiasDeEstadia()
        {
            var entrada = DateTime.Today.AddDays(6);
            var saida = DateTime.Today.AddDays(106);

            var res = Reserva.CriarNova(entrada, saida, 1, "cliente");

            Assert.AreEqual(100, (res.DataSaida - res.DataEntrada).TotalDays);
        }

        [TestMethod]
        public void DeveriaCriarNovaReservaCom99DiasDeEstadia()
        {
            var entrada = DateTime.Today.AddDays(6);
            var saida = DateTime.Today.AddDays(105);

            var res = Reserva.CriarNova(entrada, saida, 1, "cliente");

            Assert.AreEqual(99, (res.DataSaida - res.DataEntrada).TotalDays);
        }
    }
}
