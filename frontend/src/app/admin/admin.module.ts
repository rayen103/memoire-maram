import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard.component';
import { AdminCrudPanelsComponent } from './components/admin-crud-panels/admin-crud-panels.component';

@NgModule({
  declarations: [AdminDashboardComponent, AdminCrudPanelsComponent],
  imports: [CommonModule, ReactiveFormsModule, AdminRoutingModule]
})
export class AdminModule {}
