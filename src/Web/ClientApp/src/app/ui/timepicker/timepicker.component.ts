import { Component } from '@angular/core';
import { NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { UntypedFormControl } from '@angular/forms';

@Component({
  selector: 'app-timepicker',
  templateUrl: './timepicker.component.html',
  styleUrls: ['./timepicker.component.sass'],
})
export class TimepickerComponent {
  meridian = true;
  time: NgbTimeStruct = { hour: 13, minute: 30, second: 30 };
  meridianTime: NgbTimeStruct = { hour: 13, minute: 30, second: 30 };
  secondsTime: NgbTimeStruct = { hour: 13, minute: 30, second: 30 };
  spinnersTime: NgbTimeStruct = { hour: 13, minute: 30, second: 30 };
  stepsTime: NgbTimeStruct = { hour: 13, minute: 30, second: 30 };
  validationTime: NgbTimeStruct = { hour: 13, minute: 30, second: 30 };
  seconds = true;
  spinners = true;
  hourStep = 1;
  minuteStep = 15;
  secondStep = 30;

  ctrl = new UntypedFormControl('', (control: UntypedFormControl) => {
    const value = control.value;

    if (!value) {
      return null;
    }

    if (value.hour < 12) {
      return { tooEarly: true };
    }
    if (value.hour > 13) {
      return { tooLate: true };
    }

    return null;
  });

  toggleMeridian() {
    this.meridian = !this.meridian;
  }

  toggleSeconds() {
    this.seconds = !this.seconds;
  }

  toggleSpinners() {
    this.spinners = !this.spinners;
  }
}
