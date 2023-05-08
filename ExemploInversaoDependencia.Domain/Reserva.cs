using ExemploInversaoDependencia.Domain.SeedWork.Exceptions;
using System;

namespace ExemploInversaoDependencia.Domain
{
    public class Reserva
    {
        public string NumeroReserva { get; private set; }
        public long CpfCliente { get; private set; }
        public string NomeCliente { get; private set; }
        public DateTime DataEntrada { get; private set; }
        public DateTime DataSaida { get; private set; }
        public short StatusReserva { get; private set; }
        public DateTime UltimaAtualizacao { get; private set; }

        public void DefinirNumeroReserva(string numero) => NumeroReserva = numero;

        public static Reserva CriarNova(DateTime entrada, 
                                        DateTime saida,
                                        long cpfCliente,
                                        string nomeCliente)
        {
            ValidarDatasEntradaSaida(entrada, saida);

            var reserva = new Reserva()
            {
                DataEntrada = entrada,
                DataSaida = saida,
                CpfCliente = cpfCliente,
                NomeCliente = nomeCliente,
                StatusReserva = 1,
                UltimaAtualizacao = DateTime.Now
            };

            return reserva;
        }

        public void Alterar(DateTime entrada,
                            DateTime saida)
        {
            ValidarDatasEntradaSaida(entrada, saida);
            DataEntrada = entrada;
            DataSaida = saida;
            UltimaAtualizacao = DateTime.Now;
        }

        public void ConfirmarPagamento() => StatusReserva = 3;

        //criado apenas para o teste
        public void Alterar(DateTime entrada) => DataEntrada = entrada;

        private static void ValidarDatasEntradaSaida(DateTime entrada,
                                                     DateTime saida)
        {
            if (entrada <= DateTime.Today.AddDays(5))
            {
                throw new DomainValidationException("A reserva deve ser feita com 5 dias de antecedência.");
            }
            if (saida <= entrada)
            {
                throw new DomainValidationException("A data de entrada deve ser anterior à data de saída.");
            }
            if ((saida - entrada).TotalDays < 3)
            {
                throw new DomainValidationException("A estadia mínima é de 3 dias.");
            }
            if ((saida - entrada).TotalDays > 100)
            {
                throw new DomainValidationException("A estadia máxima é de 100 dias.");
            }
        }

        public double CalcularValorEstadia(double valorDiaria)
        {
            var valorMinimo = 10;
            if (valorDiaria < valorMinimo)
            {
                throw new DomainValidationException($"O valor da diária não pode ser menor que R$ {valorMinimo}.");
            }

            var dias = (DataSaida - DataEntrada).TotalDays;
            return dias * valorDiaria;
        }

        public double Calcular(double valorDiaria,
                               DateTime dataEntrada,
                               DateTime dataSaida,
                               bool permiteCancelamento,
                               double taxaCancelamento,
                               int quantidadeParcelas,
                               double jurosParcelamento,
                               double descontoCupom,
                               double valorMinimoCobranca)
        {
            var dias = (dataSaida - dataEntrada).TotalDays;
            if (dias < 1)
            {
                throw new DomainValidationException("A estadia mínima é de 1 dia.");
            }

            var valorEstadia = dias * valorDiaria;

            if (permiteCancelamento)
            {
                valorEstadia *= (1 + taxaCancelamento);
            }

            if (quantidadeParcelas > 1) 
            {
                valorEstadia *= (1 + jurosParcelamento);
            }

            if (descontoCupom > 0)
            {
                var valorDesconto = descontoCupom / 100 * valorEstadia;
                valorEstadia = valorEstadia - valorDesconto;
            }

            if (valorEstadia < valorMinimoCobranca)
            {
                valorEstadia = valorMinimoCobranca;
            }

            return valorEstadia;
        }
    }
}
