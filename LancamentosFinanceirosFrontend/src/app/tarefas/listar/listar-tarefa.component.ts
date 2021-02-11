import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs/operators';

import { LancamentoService, Lancamento } from '../shared';

@Component({
  selector: 'app-listar-tarefa',
  templateUrl: './listar-tarefa.component.html',
  styleUrls: ['./listar-tarefa.component.css']
})
export class ListarTarefaComponent implements OnInit {

  totalRegistros: number;
  listaLancamentos: Lancamento[];
  columns = [];
  constructor(private lancamentoService: LancamentoService) {}

  ngOnInit() {
  	this.listarTodos();
  }

  buildColumns(): void{
    this.columns = [
      {prop: 'dataLancamento', name: 'Data do Lançamento'},
      {prop: 'valor', name: 'Valor'},
      {prop: 'tipoLancamento', name:'Tipo de Lançamento'},
      {prop: 'statusLancamento', name:'Status do Lançamento'}
    ];
  }

  listarTodos(): void{
  	this.lancamentoService.listarTodos()
    .pipe(tap((r) => (this.totalRegistros = r.length > 0 ? r.length + 1 : 0)))
    .subscribe((r) => (this.listaLancamentos = r));
    debugger;
    console.log(this.listaLancamentos);
  }

  remover($event: any, lancamento: Lancamento): void {
    $event.preventDefault();
    if (confirm('Deseja remover o lançamento?')) {
      this.lancamentoService.remover(lancamento.idLancamento);
      this.lancamentoService.listarTodos();
    }
  }

}
