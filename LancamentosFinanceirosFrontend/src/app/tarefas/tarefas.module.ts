import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { 
  LancamentoService, 
  TarefaConcluidaDirective 
} from './shared';
import { ListarTarefaComponent } from './listar';
import { CadastrarLancamentoComponent } from './cadastrar';
import { EditarTarefaComponent } from './editar';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule
  ],
  declarations: [
  	ListarTarefaComponent,
  	CadastrarLancamentoComponent,
  	EditarTarefaComponent,
  	TarefaConcluidaDirective
  ],
  providers: [
  	LancamentoService
  ]
})
export class TarefasModule { }
