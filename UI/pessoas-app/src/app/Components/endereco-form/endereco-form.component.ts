import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Endereco } from '../../Models/endereco';

@Component({
  selector: 'app-endereco-form',
  templateUrl: './endereco-form.component.html',
  imports: [ReactiveFormsModule, CommonModule],
  standalone: true,
  styleUrls: ['./endereco-form.component.css']
})
export class EnderecoFormComponent {
  enderecoForm: FormGroup;
  enderecos: any[] = []; // Lista local de endereços
  @Output() enderecoAdicionado = new EventEmitter<any>(); // Emitir evento para o PessoaForm

  constructor(private fb: FormBuilder) {
    this.enderecoForm = this.fb.group({
      logradouro: ['', Validators.required],
      cidade: ['', Validators.required],
      estado: ['', Validators.required],
      cep: ['', Validators.required],
    });
  }

  adicionarEndereco() {
    if (this.enderecoForm.valid) {
      const endereco = this.enderecoForm.value;
      this.enderecoAdicionado.emit(endereco); // Envia para o PessoaForm
      this.enderecoForm.reset(); // Limpa o formulário
    }
  }

  removerEndereco(endereco: Endereco) {
    const index = this.enderecos.indexOf(endereco);
    if (index !== -1) {
        this.enderecos.splice(index, 1); // Remove o endereço da lista
    }
}
}
