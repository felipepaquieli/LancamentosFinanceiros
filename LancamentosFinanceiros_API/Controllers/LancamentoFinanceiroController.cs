using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LancamentosFinanceiros.Acoes;
using LancamentosFinanceiros.Data;
using LancamentosFinanceiros.Models;
using LancamentosFinanceiros.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LancamentosFinanceiros.Controllers
{
    [ApiController]
    [Route("lancamento")]
    public class LancamentoFinanceiroController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<LancamentoFinanceiro[]>> Get([FromServices] DataContext context)
        {
            var lancamentos = await context.LancamentosFinanceiros.ToArrayAsync();
            return lancamentos;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<LancamentoFinanceiro>> GetById([FromServices] DataContext context, int id)
        {
            var lancamento = await context.LancamentosFinanceiros
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdLancamento == id);
            return lancamento;
        }

        [HttpPost]
        [Route("")]
        public async Task<DefaultResponse<LancamentoFinanceiro>> Post(
            [FromServices] DataContext context,
            [FromBody] LancamentoFinanceiro model)
        {
            try
            {
                context.LancamentosFinanceiros.Add(model);
                await context.SaveChangesAsync();
                return new DefaultResponse<LancamentoFinanceiro>()
                {
                    success = true,
                    message = "Lançamento efetuado com sucesso!",
                    result = model
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<DefaultResponse<LancamentoFinanceiro>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody] LancamentoFinanceiro model)
        {
            Acoes_LancamentoFinanceiro aux = new Acoes_LancamentoFinanceiro();
            try
            {
                var lancamento = await GetById(context, id);

                bool altera = aux.PodeAtualizarOuApagar(lancamento.Value);

                if (altera)
                {
                    var lancamentoAtualizado = aux.Atualiza(lancamento.Value, model);

                    context.Update(lancamento.Value);

                    await context.SaveChangesAsync();
                    return new DefaultResponse<LancamentoFinanceiro>()
                    {
                        success = true,
                        message = "Lançamento atualizado com sucesso!",
                        result = lancamentoAtualizado
                    };
                }
                else
                {
                    return new DefaultResponse<LancamentoFinanceiro>()
                    {
                        success = false,
                        message = "Lançamento não pode ser atualizado porque já foi conciliado!",
                        result = lancamento.Value
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<DefaultResponse<LancamentoFinanceiro>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            Acoes_LancamentoFinanceiro aux = new Acoes_LancamentoFinanceiro();
            try
            {
                var lancamento = await GetById(context, id);

                bool podeApagar = aux.PodeAtualizarOuApagar(lancamento.Value);

                if(podeApagar)
                {
                    context.Remove(lancamento.Value);
                    await context.SaveChangesAsync();

                    return new DefaultResponse<LancamentoFinanceiro>()
                    {
                        success = true,
                        message = "Lancamento apagado com sucesso!",
                        result = lancamento.Value
                    };
                }
                else
                {
                    return new DefaultResponse<LancamentoFinanceiro>()
                    {
                        success = false,
                        message = "Lançamento não pode ser apagado porque já foi conciliado!",
                        result = lancamento.Value
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}