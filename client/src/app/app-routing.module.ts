import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'announcements', component: MembersComponent },
  { path: '**', component: HomeComponent, pathMatch:'full' },
  // { path: 'my-announcements', component: MembersComponent },
  // { path: 'add-announcement', component: MembersComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
