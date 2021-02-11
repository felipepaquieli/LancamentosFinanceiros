import { TestBed, inject } from '@angular/core/testing';
import { LancamentoService } from './lancamento.service';

describe('TarefaService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LancamentoService]
    });
  });

  it('should ...', inject([LancamentoService], (service: LancamentoService) => {
    expect(service).toBeTruthy();
  }));
});
