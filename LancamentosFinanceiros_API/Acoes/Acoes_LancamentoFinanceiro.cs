using System;
using System.Collections.Generic;
using LancamentosFinanceiros.Data;
using LancamentosFinanceiros.Models;
using LancamentosFinanceiros.Models.Enums;

namespace LancamentosFinanceiros.Acoes
{
    public class Acoes_LancamentoFinanceiro
    {
        public LancamentoFinanceiro Atualiza(LancamentoFinanceiro atualizar, LancamentoFinanceiro novosDados)
        {
            atualizar.DataLancamento = novosDados.DataLancamento == null ? atualizar.DataLancamento : novosDados.DataLancamento;
            atualizar.Valor = novosDados.Valor == null ? atualizar.Valor : novosDados.Valor;
            atualizar.TipoLancamento = string.IsNullOrEmpty(novosDados.TipoLancamento) ? atualizar.TipoLancamento : novosDados.TipoLancamento;
            atualizar.StatusLancamento = string.IsNullOrEmpty(novosDados.StatusLancamento) ? atualizar.StatusLancamento : novosDados.StatusLancamento;
            atualizar.IdBalanco = novosDados.IdBalanco == null ? atualizar.IdBalanco : novosDados.IdBalanco;

            return atualizar;
        }

        public bool PodeAtualizarOuApagar(LancamentoFinanceiro lancamento)
        {
            if(lancamento.IdBalanco == null)
                return true;
            else
                return false;
        }

        public BalancoDia Conciliacao(List<LancamentoFinanceiro> lancamentos)
        {
            decimal totalCredito = 0;
            decimal totalDebito = 0;
            decimal total = 0;

            foreach(var item in lancamentos)
            {
                if(item.TipoLancamento.ToLower() == TipoLancamento.credito.ToString())
                {
                    totalCredito += Convert.ToDecimal(item.Valor);
                    total += Convert.ToDecimal(item.Valor);
                }
                else if(item.TipoLancamento.ToLower() == TipoLancamento.debito.ToString())
                {
                    totalDebito += Convert.ToDecimal(item.Valor);
                    total -= Convert.ToDecimal(item.Valor);
                }
            }
            
            BalancoDia balanco = new BalancoDia();
            balanco.DataBalanco = Convert.ToDateTime(lancamentos[0].DataLancamento);
            balanco.ValorSaldo = total;
            balanco.ValorTotalCredito = totalCredito;
            balanco.ValorTotalDebito = totalDebito;

            return balanco;
        }

        public void AtualizarLancamentosAposBalanco(DataContext context, List<LancamentoFinanceiro> lancamentosDia, int idBalanco)
        {
            try
            {
                foreach(var item in lancamentosDia)
                {
                    item.IdBalanco = idBalanco;
                    item.StatusLancamento = StatusLancamento.conciliado.ToString().ToUpper();
                    context.LancamentosFinanceiros.Update(item);
                    context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}