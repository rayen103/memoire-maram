import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ParentRoutingModule } from './parent-routing.module';
import { ParentDashboardComponent } from './pages/parent-dashboard/parent-dashboard.component';

@NgModule({
  declarations: [ParentDashboardComponent],
  imports: [CommonModule, FormsModule, ParentRoutingModule]
})
export class ParentModule {}
