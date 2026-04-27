import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { DashboardStats } from '../../../core/models/app.models';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  stats?: DashboardStats;

  constructor(private readonly api: ApiService) {}

  ngOnInit(): void {
    this.api.getDashboardStats().subscribe((stats) => (this.stats = stats));
  }
}
