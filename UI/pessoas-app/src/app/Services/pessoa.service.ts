import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pessoa } from '../Models/pessoa';

@Injectable({
  providedIn: 'root',
})
export class PessoaService {
  private apiUrl = 'https://localhost:7282/pessoas';

  constructor(private http: HttpClient) {}

  listarPessoas(): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(this.apiUrl);
  }

  adicionarPessoa(pessoa: Pessoa): Observable<Pessoa> {
    return this.http.post<Pessoa>(this.apiUrl, pessoa);
  }

  atualizarPessoa(pessoa: Pessoa) {
    return this.http.put(`${this.apiUrl}/${pessoa.id}`, pessoa);
  }

  excluirPessoa(id: string) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
