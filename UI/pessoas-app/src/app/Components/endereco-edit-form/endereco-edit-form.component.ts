import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Endereco } from '../../Models/endereco';
import { EnderecoService } from '../../Services/endereco.service';
import { ReactiveFormsModule } from '@angular/forms'; // Importe o ReactiveFormsModule

@Component({
    selector: 'app-endereco-edit-form',
    templateUrl: './endereco-edit-form.component.html',
    standalone: true,
    imports: [ReactiveFormsModule], // Adicione o ReactiveFormsModule aqui
    styleUrls: ['./endereco-edit-form.component.css']
})
export class EnderecoEditFormComponent implements OnInit {
    @Output() enderecoAtualizado = new EventEmitter<any>();
    @ViewChild('enderecoModal') modal: ElementRef | undefined;
    enderecoId: string | null = null;
    endereco: Endereco | null = null;
    idPessoa: string | null = null;
    formulario: FormGroup;


    constructor(
        private fb: FormBuilder,
        private enderecoService: EnderecoService
    ) {
        this.formulario = this.fb.group({
            id: [''],
            pessoaId: ['', Validators.required],
            logradouro: ['', Validators.required],
            cidade: ['', Validators.required],
            estado: ['', Validators.required],
            cep: ['', Validators.required],
        });
    }

    ngOnInit() {
        // Lógica de inicialização (se necessária)
    }

    atualizarFormulario(endereco: Endereco, idPessoa: string | null) {
        console.log(idPessoa);

        // Atualiza os valores do formulário com os dados de 'endereco'
        this.formulario.patchValue(endereco);

        // Atualiza o campo 'pessoaId' com o valor de 'idPessoa'
        this.formulario.patchValue({ pessoaId: idPessoa });
    }

    abrirModal() {
        this.formulario.reset();
        const modal = document.getElementById('enderecoModal');
        if (modal) modal.style.display = 'block';
    }
    fecharModal() {
        if (this.modal) this.modal.nativeElement.style.display = 'none';
    }

    onSubmit() {
        if (this.formulario.invalid) {
            alert('Insira todos os campos para enviar');
            return;
        }

        const endereco = this.formulario.value;
        console.log(endereco)
        if (endereco.id) {
            this.enderecoService.atualizarEndereco(endereco).subscribe(
                (res) => {
                    // Sucesso
                    console.log('Endereço atualizado com sucesso:', res);
                    this.fecharModal(); // Fechar o modal após a atualização

                    // Emitir o evento para o componente pai (PessoaComponent)
                    console.log('Emitindo enderecoAtualizado');
                },
                (error) => {
                    // Tratar erro caso a atualização falhe
                    console.error('Erro ao atualizar endereço:', error);
                    alert('Houve um erro ao atualizar o endereço.');
                }
            );
        } else {
            alert('ID do endereço não encontrado.');
        }
    }

}
