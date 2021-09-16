import { LevelTwoAuthGuard } from './../guard/level-two-auth.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { PostComponent } from './post/post.component';
import { PostsComponent } from './posts.component';

const routes: Routes = [
  { path: '', redirectTo: 'posts'},
  { path: 'posts', component: PostsComponent },
  { path: 'create', component: CreateComponent, canActivate: [LevelTwoAuthGuard]},
  { path: 'post', redirectTo: 'posts'},
  { path: 'post/:id', component: PostComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostsRoutingModule { }
