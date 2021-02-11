import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms'; 

import { LancamentoService, Lancamento } from '../shared';

@Component({
  selector: 'app-cadastrar-lancamento',
  templateUrl: './cadastrar-lancamento.component.html',
  styleUrls: ['./cadastrar-lancamento.component.css']
})
export class CadastrarLancamentoComponent implements OnInit {

  @ViewChild('formLancamento', { static: true }) formLancamento: NgForm;
  lancamento: Lancamento;

  constructor(private lancamentoService: LancamentoService,
  	private router: Router) { }

  ngOnInit() {
  	this.lancamento = new Lancamento();
  }

  cadastrar(): void {
    if (this.formLancamento.form.valid) {
  	  this.lancamentoService.cadastrar(this.lancamento);
  	  this.router.navigate(["/tarefas"]);
    }
  }

}
