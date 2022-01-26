import { Component, Input, OnInit } from '@angular/core';
import { ApplicantModel } from 'src/app/models/applicant-model';
import { TrainingSessionModel } from 'src/app/models/training-session-model';
import { UserModel } from 'src/app/models/user-model';
import { ApplicantService } from 'src/app/services/applicant.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-session-modal',
  templateUrl: './session-modal.component.html',
  styleUrls: ['./session-modal.component.css']
})
export class SessionModalComponent implements OnInit {
@Input() session = new TrainingSessionModel();
  constructor(
    private applicantService: ApplicantService,
    private userService: UserService,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe((x) => (this.user = x));
   }
  user: UserModel;
  public applicants: ApplicantModel[] = [];
  public applicantUsers: UserModel[] = [];
  ngOnInit(): void {
    this.applicantService.listBySessionId(this.session.id).subscribe(
      (result) => {
        this.session.numberOfApplicants = result.length;
              this.applicants = result;
              this.applicants.forEach((applicant) => {
                this.userService.getUserById(applicant.user_id).subscribe(
                  (result) => {
                    this.applicantUsers.push(result);
                  },
                  (error) => console.log(error)
                );
              });
      }, (error) => console.log(error)
    );
    
  }

}
