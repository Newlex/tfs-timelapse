import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { ButtonModule, InputTextModule, PanelModule, MenubarModule, MenuItem, DropdownModule } from "primeng/primeng";
import { AppComponent } from './app.component';
import { DiffMatchPatchModule } from '../diff/diffMatchPatch.module';
import { VersionControlService } from "./core/versioncontrol.service";
import * as $ from 'jquery';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    ButtonModule,
    InputTextModule,
    PanelModule,
    MenubarModule,
    DropdownModule,
    DiffMatchPatchModule
  ],
  providers: [
    VersionControlService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
