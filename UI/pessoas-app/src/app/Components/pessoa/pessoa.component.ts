import { Component, inject, ViewChild, OnInit } from '@angular/core';
import { PessoaFormComponent } from '../pessoa-form/pessoa-form.component';
import { PessoaService } from '../../Services/pessoa.service';
import { Pessoa } from '../../Models/pessoa';
import { CommonModule } from '@angular/common';
import { EnderecoService } from '../../Services/endereco.service';
import { EnderecoEditFormComponent } from '../endereco-edit-form/endereco-edit-form.component';
import { Endereco } from '../../Models/endereco';

@Component({
  selector: 'app-pessoa',
  standalone: true,
  imports: [
    PessoaFormComponent,
    CommonModule,
    EnderecoEditFormComponent,
  ],
  templateUrl: './pessoa.component.html',
  styleUrl: './pessoa.component.css',
})
export class PessoaComponent implements OnInit {
  @ViewChild(PessoaFormComponent) pessoaForm!: PessoaFormComponent;
  @ViewChild(EnderecoEditFormComponent) enderecoEditForm!: EnderecoEditFormComponent;

  pessoasList: Pessoa[] = [];
  pesService = inject(PessoaService);
  endService = inject(EnderecoService);

  ngOnInit() {
    this.listarPessoas();
  }

  enderecoSelecionado: Endereco | null = null;

  fecharModal() {
    this.enderecoSelecionado = null;
    this.listarPessoas();
  }

  abrirModal() {
    if (this.pessoaForm) {
      this.pessoaForm.abrirModal();
    }
  }

  enderecosAbrirModal(){
    if (this.enderecoEditForm) {
      this.enderecoEditForm.abrirModal();
    }
  }

  listarPessoas() {
    this.pesService.listarPessoas().subscribe((res) => {
      this.pessoasList = res;
    });
  }

  atualizarTabela() {
    this.listarPessoas();
  }

  atualizarPessoa(pessoa: Pessoa) {
    this.abrirModal();
    if (this.pessoaForm) {
      this.pessoaForm.atualizarFormulario(pessoa);
    }
  }

  AtualizarEndereco(endereco: Endereco, pessoa: Pessoa) {
    const idPessoa = pessoa.id
    this.enderecoEditForm.abrirModal();
    if (this.enderecoEditForm) {
      this.enderecoEditForm.atualizarFormulario(endereco, idPessoa);
    }
  }

  excluirPessoa(id: string) {
    const confirmar = confirm("Tem certeza que deseja excluir essa pessoa?");
    if (confirmar) {
      this.pesService.excluirPessoa(id).subscribe(() => {
        this.listarPessoas();
      });
    }
  }

  excluirEndereco(id: string) {
    const confirmar = confirm("Tem certeza que deseja excluir esse endereço?");
    if (confirmar) {
      this.endService.excluirEndereco(id).subscribe(() => {
        this.listarPessoas();
      });
    }
  }
}
