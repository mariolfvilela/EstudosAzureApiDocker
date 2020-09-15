using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CalculoFinanceiroController : Controller
    {
        [HttpGet("juroscompostos")]
        [ProducesResponseType(typeof(Emprestimo), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FalhaCalculo), (int)HttpStatusCode.BadRequest)]
        public ActionResult<Emprestimo> Get(
           [FromServices] ILogger<CalculoFinanceiroController> logger,
           double valorEmprestimo, int numMeses, double percTaxa)
        {
            if (valorEmprestimo <= 0)
                return new BadRequestObjectResult(new FalhaCalculo() { Mensagem = "O Valor do Empréstimo deve ser maior do que zero!" });

            if (numMeses <= 0)
                return new BadRequestObjectResult(new FalhaCalculo() { Mensagem = "O Número de Meses deve ser maior do que zero!" });

            if (percTaxa <= 0)
                return new BadRequestObjectResult(new FalhaCalculo() { Mensagem = "O Percentual da Taxa de Juros deve ser maior do que zero!" });

            logger.LogInformation(
                "Recebida nova requisição|" +
               $"Valor do empréstimo: {valorEmprestimo}|" +
               $"Número de meses: {numMeses}|" +
               $"% Taxa de Juros: {percTaxa}");

            double valorFinalJuros =
                CalculoFinanceiro.CalcularValorComJurosCompostos(
                    valorEmprestimo, numMeses, percTaxa);
            logger.LogInformation($"Valor Final com Juros: {valorFinalJuros}");

            return new Emprestimo()
            {
                ValorEmprestimo = valorEmprestimo,
                NumMeses = numMeses,
                TaxaPercentual = percTaxa,
                ValorFinalComJuros = valorFinalJuros
            };
        }
    }
}
