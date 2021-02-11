import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { Lancamento } from '.';

@Injectable()
export class LancamentoService {

  public httpHeaders: HttpHeaders;

  constructor(
    protected http: HttpClient,
  ) {
      this.httpHeaders = new HttpHeaders();
   }

  listarTodos(): Observable<Lancamento[]> {
  	const lancamentos = 'http://localhost:5000/lancamento/';
  	const teste = this.http.get<Lancamento[]>(lancamentos, {headers: this.httpHeaders}).pipe(take(1));
    debugger;
    return teste;
  }

  listarPorId(id: number): Observable<Lancamento>{
    const lancamentos = 'http://localhost:5000/lancamento/';
  	return this.http.get<Lancamento>('${lancamentos}${id}', {headers: this.httpHeaders}).pipe(take(1));
  }

  cadastrar(lancamento: Lancamento): Observable<Lancamento> {
  	const lancamentos = 'http://localhost:5000/lancamento/';
  	return this.http.post<Lancamento>(lancamentos, lancamento, {headers: this.httpHeaders}).pipe(take(1));
  }

  atualizar(lancamento: Lancamento, id: number): Observable<Lancamento> {
    const lancamentos = 'http://localhost:5000/lancamento/';
  	return this.http.put<Lancamento>('${lancamentos}${id}', lancamento, {headers: this.httpHeaders}).pipe(take(1));
  }

  remover(id: number): Observable<Lancamento> {
    const lancamentos = 'http://localhost:5000/lancamento/';
  	return this.http.delete<Lancamento>('${lancamentos}${id}', {headers: this.httpHeaders}).pipe(take(1));
  }

}
