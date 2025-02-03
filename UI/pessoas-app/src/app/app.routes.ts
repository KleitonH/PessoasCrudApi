import { Routes } from '@angular/router';
import { PessoaComponent } from './Components/pessoa/pessoa.component';

export const routes: Routes = [
    {
        path: "", component:PessoaComponent
    },
    {
        path:"pessoa", component:PessoaComponent
    }
];
