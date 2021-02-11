import { Routes } from '@angular/router';

import { ListarTarefaComponent } from './listar';
import { EditarTarefaComponent } from './editar';
import { CadastrarLancamentoComponent } from './cadastrar';

export const TarefaRoutes: Routes = [
	{ 
		path: 'tarefas', 
		redirectTo: 'tarefas/listar' 
	},
	{ 
		path: 'tarefas/listar', 
		component: ListarTarefaComponent 
	}, 
	{ 
		path: 'tarefas/cadastrar', 
		component: CadastrarLancamentoComponent 
	},
	{ 
		path: 'tarefas/editar/:id', 
		component: EditarTarefaComponent 
	}
];
