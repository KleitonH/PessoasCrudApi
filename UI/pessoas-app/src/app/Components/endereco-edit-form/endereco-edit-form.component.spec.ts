import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnderecoEditFormComponent } from './endereco-edit-form.component';

describe('EnderecoEditFormComponent', () => {
  let component: EnderecoEditFormComponent;
  let fixture: ComponentFixture<EnderecoEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnderecoEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EnderecoEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
