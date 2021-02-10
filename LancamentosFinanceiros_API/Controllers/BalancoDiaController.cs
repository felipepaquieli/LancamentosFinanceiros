using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LancamentosFinanceiros.Acoes;
using LancamentosFinanceiros.Data;
using LancamentosFinanceiros.Models;
using LancamentosFinanceiros.Models.Enums;
using LancamentosFinanceiros.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LancamentosFinanceiros.Controllers
{
    [ApiController]
    [Route("balanco")]
    public class BalancoDiaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<BalancoDia>>> Get([FromServices] DataContext context)
        {
            var balancos = await context.BalancosDia.ToListAsync();
            return balancos;
        }

        [HttpGet]
        [Route("{dataInicio:DateTime}/{dataFim:DateTime}")]
        public async Task<ActionResult<List<BalancoDia>>> GetByPeriod([FromServices] DataContext context, DateTime dataInicio, DateTime dataFim)
        {
            var balancos = await context.BalancosDia.Where(x => x.DataBalanco >= dataInicio && x.DataBalanco <= dataFim).ToListAsync();
            return balancos;
        }

        [HttpPost]
        [Route("")]
        public async Task<DefaultResponse<BalancoDia>> Post(
            [FromServices] DataContext context,
            [FromBody] BalancoDia dataBalanco)
        {
            try
            {
                Acoes_LancamentoFinanceiro acoes = new Acoes_LancamentoFinanceiro();
                List<LancamentoFinanceiro> lancamentosDia = await context.LancamentosFinanceiros.Where(x => x.DataLancamento == dataBalanco.DataBalanco).ToListAsync();
                
                var model = acoes.Conciliacao(lancamentosDia);

                context.BalancosDia.Add(model);
                await context.SaveChangesAsync();

                acoes.AtualizarLancamentosAposBalanco(context, lancamentosDia, model.IdBalanco);

                return new DefaultResponse<BalancoDia>()
                {
                    success = true,
                    message = $"Balanço do dia {dataBalanco.DataBalanco} efetuado com sucesso!",
                    result = model
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}