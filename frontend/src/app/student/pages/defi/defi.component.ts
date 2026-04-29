import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { AuthService } from '../../../core/services/auth.service';
import { Defi, StudentDefi } from '../../../core/models/app.models';

@Component({
  selector: 'app-defi',
  templateUrl: './defi.component.html',
  styleUrls: ['./defi.component.css'],
  standalone: false
})
export class DefiComponent implements OnInit {
  studentProfileId?: number;
  allDefis: Defi[] = [];
  studentDefis: StudentDefi[] = [];
  loading = false;

  constructor(private readonly api: ApiService, private readonly auth: AuthService) {}

  ngOnInit(): void {
    const userId = this.auth.user?.id;
    if (!userId) return;

    this.loading = true;
    this.api.getStudentProfileByUser(userId).subscribe({
      next: (profile) => {
        this.studentProfileId = profile.id;
        this.api.getDefis().subscribe((defis) => (this.allDefis = defis));
        this.api.getStudentDefis(profile.id).subscribe((sd) => (this.studentDefis = sd));
      },
      complete: () => (this.loading = false)
    });
  }

  getStudentDefi(defiId: number): StudentDefi | undefined {
    return this.studentDefis.find((sd) => sd.defiId === defiId);
  }

  start(defiId: number): void {
    if (!this.studentProfileId) return;
    this.api.startDefi(this.studentProfileId, defiId).subscribe((sd) => {
      this.studentDefis = [...this.studentDefis.filter((x) => x.defiId !== defiId), sd];
    });
  }

  complete(defiId: number): void {
    if (!this.studentProfileId) return;
    this.api.completeDefi(this.studentProfileId, defiId).subscribe((sd) => {
      this.studentDefis = this.studentDefis.map((x) => (x.defiId === defiId ? sd : x));
    });
  }
}
