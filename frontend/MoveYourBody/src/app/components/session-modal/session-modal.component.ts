import { Component, Input, OnInit } from '@angular/core';
import { ApplicantService } from 'src/app/services/applicant.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { LocationService } from 'src/app/services/location.service';
import { ApplicantModel } from 'src/app/models/applicant-model';
import { LocationModel } from 'src/app/models/location-model';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { UserModel } from 'src/app/models/user-model';

@Component({
  selector: 'app-session-modal',
  templateUrl: './session-modal.component.html',
  styleUrls: ['./session-modal.component.css'],
})
export class SessionModalComponent implements OnInit {
  @Input() session = new TrainingSessionModel();
  @Input() mode = '';
  
  constructor(
    private applicantService: ApplicantService,
    private locationService: LocationService,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe((x) => (this.user = x));
  }
  user: UserModel;
  location: LocationModel;
  applicants: ApplicantModel[] = [];
  applicantUsers: UserModel[] = [];
  ngOnInit(): void {
    if (this.mode == 'trainer') {
      this.applicantService.listBySessionId(this.session.id).subscribe(
        (result) => {
          this.applicants = result.applicants;
          this.applicantUsers = result.users;
        },
        (error) => console.log(error)
      );
    } else if(this.mode == 'applied') {
      this.locationService.getById(this.session.location_id).subscribe(
        (result) => {
          this.location = result[0];
        },
        (error) => console.log(error)
      );
    }
  }
}
