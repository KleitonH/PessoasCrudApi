import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PessoaService } from '../../Services/pessoa.service';
import { Pessoa } from '../../Models/pessoa';
import { EnderecoFormComponent } from '../endereco-form/endereco-form.component';
import { CommonModule } from '@angular/common';
import { Endereco } from '../../Models/endereco';
import { EnderecoService } from '../../Services/endereco.service';

@Component({
    selector: 'app-pessoa-form',
    standalone: true,
    imports: [ReactiveFormsModule, CommonModule, EnderecoFormComponent],
    templateUrl: './pessoa-form.component.html',
    styleUrls: ['./pessoa-form.component.css'],
})
export class PessoaFormComponent implements OnInit {
    @Output() pessoaAdicionada = new EventEmitter<void>();
    @ViewChild('minhaModal') modal: ElementRef | undefined;
    @ViewChild(EnderecoFormComponent) enderecoFormComponent!: EnderecoFormComponent;

    pessoaForm: FormGroup; // FormGroup para o formulário de pessoa
    enderecos: Endereco[] = [];

    constructor(
        private fb: FormBuilder,
        private pessoaService: PessoaService,
        private enderecoService: EnderecoService
    ) {
        this.pessoaForm = this.fb.group({
            id: [''],
            nome: ['', Validators.required],
            email: ['', Validators.required],
            dataNascimento: ['', Validators.required],
        });
    }

    ngOnInit(): void { }

    // Método para abrir o modal
    abrirModal() {
        this.pessoaForm.reset(); // Reseta os valores do formulário
        this.enderecos = []; // Opcional: limpar endereços ao abrir o modal

        const modal = document.getElementById('myModal');
        if (modal) modal.style.display = 'block';
    }

    // Método para fechar o modal
    fecharModal() {
        if (this.modal) this.modal.nativeElement.style.display = 'none';
    }

    // Método para adicionar endereço
    adicionarEndereco(endereco: Endereco) {
        this.enderecos.push(endereco);
    }

    // Método para aplicar patchValue no FormGroup
    atualizarFormulario(pessoa: Pessoa) {
        this.pessoaForm.patchValue(pessoa);
    }

    // Método para submeter o formulário
    onSubmit() {
        if (this.pessoaForm.invalid) {
            alert('Insira todos os campos para enviar');
            return;
        }

        const pessoa = this.pessoaForm.value;
        console.log(pessoa)
        if (pessoa.id) {
            // Se a pessoa já tem ID, faz a atualização dos dados básicos
            this.pessoaService.atualizarPessoa(pessoa).subscribe(() => {
                // Adiciona apenas os novos endereços
                const enderecoRequests = this.enderecos.map((endereco) => {
                    endereco.pessoaId = pessoa.id;
                    return this.enderecoService.adicionarEndereco(endereco).toPromise();
                });

                // Aguarda todas as requisições de endereço serem concluídas
                Promise.all(enderecoRequests)
                    .then(() => {
                        this.enderecos = []; // Limpa a lista de endereços
                        this.pessoaAdicionada.emit(); // Atualiza a tabela
                        this.fecharModal(); // Fecha o modal
                    })
                    .catch((error) => {
                        console.error('Erro ao adicionar endereços:', error);
                    });
            });
        } else {
            // Se não tem ID, cria uma nova pessoa
            this.pessoaService.adicionarPessoa(pessoa).subscribe((res: Pessoa) => {
                const pessoaId = res.id;

                // Adiciona os endereços para a nova pessoa
                const enderecoRequests = this.enderecos.map((endereco) => {
                    endereco.pessoaId = pessoaId;
                    return this.enderecoService.adicionarEndereco(endereco).toPromise();
                });

                // Aguarda todas as requisições de endereço serem concluídas
                Promise.all(enderecoRequests)
                    .then(() => {
                        this.enderecos = []; // Limpa a lista de endereços
                        this.pessoaAdicionada.emit(); // Atualiza a tabela
                        this.fecharModal(); // Fecha o modal
                    })
                    .catch((error) => {
                        console.error('Erro ao adicionar endereços:', error);
                    });
            });
        }
    }

}
