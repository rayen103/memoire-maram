import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentDashboardComponent } from './pages/student-dashboard/student-dashboard.component';
import { DefiComponent } from './pages/defi/defi.component';

const routes: Routes = [
  { path: '', component: StudentDashboardComponent },
  { path: 'defis', component: DefiComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule {}
