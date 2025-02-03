import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Endereco } from '../Models/endereco';

@Injectable({
  providedIn: 'root',
})
export class EnderecoService {
  private apiUrl = 'https://localhost:7282/enderecos';

  constructor(private http: HttpClient) {}

  adicionarEndereco(endereco: Endereco): Observable<Endereco> {
    return this.http.post<Endereco>(this.apiUrl, endereco);
  }

  atualizarEndereco(endereco: Endereco) {
      return this.http.put(`${this.apiUrl}/${endereco.id}`, endereco);
    }


  excluirEndereco(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
