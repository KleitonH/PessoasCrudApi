import { Endereco } from "./endereco";

export interface Pessoa {
  id: string;
  nome: string;
  dataNascimento: string;
  email: string;
  enderecos: Endereco[];
}
