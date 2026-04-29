import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { ParkingZone, SafetyTip, Score } from '../../../core/models/app.models';

@Component({
  selector: 'app-parent-dashboard',
  templateUrl: './parent-dashboard.component.html',
  styleUrls: ['./parent-dashboard.component.css'],
  standalone: false
})
export class ParentDashboardComponent implements OnInit {
  parkingZones: ParkingZone[] = [];
  tips: SafetyTip[] = [];
  childScores: Score[] = [];
  childStudentProfileId?: number;

  constructor(private readonly api: ApiService) {}

  ngOnInit(): void {
    this.api.getParkingZones(1, 20).subscribe((zones) => (this.parkingZones = zones.items));
    this.api.getSafetyTips(1, 20).subscribe((tips) => (this.tips = tips.items));
  }

  loadChildScores(): void {
    if (this.childStudentProfileId) {
      this.api.getStudentScores(this.childStudentProfileId).subscribe((scores) => (this.childScores = scores));
    }
  }
}
