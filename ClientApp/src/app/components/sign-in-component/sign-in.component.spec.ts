import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignInComponentComponent } from './sign-in.component';

describe('SignInComponentComponent', () => {
  let component: SignInComponentComponent;
  let fixture: ComponentFixture<SignInComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ SignInComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignInComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
