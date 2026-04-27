import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ApiService } from '../../../core/services/api.service';
import { Quiz, Video, SafetyTip, ParkingZone } from '../../../core/models/app.models';

@Component({
  selector: 'app-admin-crud-panels',
  templateUrl: './admin-crud-panels.component.html',
  styleUrls: ['./admin-crud-panels.component.css'],
  standalone: false
})
export class AdminCrudPanelsComponent implements OnInit {
  quizzes: Quiz[] = [];
  videos: Video[] = [];
  tips: SafetyTip[] = [];
  zones: ParkingZone[] = [];

  readonly quizForm = this.fb.group({
    title: ['', [Validators.required]],
    level: [1, [Validators.required, Validators.min(1), Validators.max(10)]]
  });

  readonly videoForm = this.fb.group({
    title: ['', [Validators.required]],
    url: ['', [Validators.required]],
    description: ['', [Validators.required]]
  });

  readonly tipForm = this.fb.group({
    content: ['', [Validators.required]]
  });

  readonly zoneForm = this.fb.group({
    schoolName: ['', [Validators.required]],
    type: ['ALLOWED', [Validators.required]],
    location: ['', [Validators.required]]
  });

  constructor(private readonly fb: FormBuilder, private readonly api: ApiService) {}

  ngOnInit(): void {
    this.reload();
  }

  reload(): void {
    this.api.getQuizzes(1, 50).subscribe((data) => (this.quizzes = data.items));
    this.api.getVideos(1, 50).subscribe((data) => (this.videos = data.items));
    this.api.getSafetyTips(1, 50).subscribe((data) => (this.tips = data.items));
    this.api.getParkingZones(1, 50).subscribe((data) => (this.zones = data.items));
  }

  addQuiz(): void {
    if (this.quizForm.invalid) return;
    this.api.createQuiz(this.quizForm.getRawValue() as { title: string; level: number }).subscribe(() => {
      this.quizForm.reset({ title: '', level: 1 });
      this.reload();
    });
  }

  addVideo(): void {
    if (this.videoForm.invalid) return;
    this.api.createVideo(this.videoForm.getRawValue() as { title: string; url: string; description: string }).subscribe(() => {
      this.videoForm.reset({ title: '', url: '', description: '' });
      this.reload();
    });
  }

  addTip(): void {
    if (this.tipForm.invalid) return;
    this.api.createSafetyTip(this.tipForm.getRawValue() as { content: string }).subscribe(() => {
      this.tipForm.reset({ content: '' });
      this.reload();
    });
  }

  addZone(): void {
    if (this.zoneForm.invalid) return;
    this.api.createParkingZone(this.zoneForm.getRawValue() as { schoolName: string; type: string; location: string }).subscribe(() => {
      this.zoneForm.reset({ schoolName: '', type: 'ALLOWED', location: '' });
      this.reload();
    });
  }
}
