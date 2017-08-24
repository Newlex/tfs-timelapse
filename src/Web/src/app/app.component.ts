import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Button, InputText, Panel, MenuItem, SelectItem } from 'primeng/primeng';
import { BrowserModule } from '@angular/platform-browser';
import { VersionControlService } from "./core/versioncontrol.service";
import { DiffMatchPatchService } from 'ng-diff-match-patch';
import { DiffMatchPatch, Diff, DiffOp } from 'ng-diff-match-patch';

declare var jQuery: any

function ConfigureRangeSlider(componentInstance, ids) {
    var sliderSelector=jQuery("#slider");
    var slider = sliderSelector.data("ionRangeSlider");
    if (slider != null) {
        slider.update({
            values: ids
        });
    }
    else {
        sliderSelector.ionRangeSlider({
            type: "single",
            grid: true,
            values: ids,
            keyboard: true,
            prettify: function (num) {
                return num;
            },
            onChange: function (sliderObj) {
                componentInstance.selectedChangeSetChanged(sliderObj.from_value);
            }
        });
    }
}

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    constructor(private vcService: VersionControlService) {
        this.diff_modes = [];
        this.diff_modes.push({ label: 'diff', value: 'diff' });
        this.diff_modes.push({ label: 'lineDiff', value: 'lineDiff' });
        this.diff_modes.push({ label: 'semanticDiff', value: 'semanticDiff' });
        this.selectedDiffMode = "lineDiff";
    }
    changeSets: any[] = [];
    path: string = "";
    content: string = "";
    right: string = "";
    left: string = "";
    currentChangeset: string;
    modes: SelectItem[];
    diff_modes: SelectItem[];
    selectedMode: number;
    selectedDiffMode: string;

    ngOnInit() {
    }

    pathChange() {
        this.vcService.getChangeSets(this.path).subscribe(values => {
            this.changeSets = values;
            var ids = this.changeSets.map(function (item: any) {
                return item.changesetId;
            });
            var sortedIds=ids.sort();
            ConfigureRangeSlider(this, sortedIds);
            this.selectedChangeSetChanged(sortedIds[0]);
        });
    }

    selectedChangeSetChanged(from_value: number) {
        this.currentChangeset = this.changeSets.filter(changeSet => changeSet.changesetId === from_value)[0];
        this.vcService.getContent(from_value, this.path).subscribe(value => {
            this.right = this.content = value;
            var nextChangeset = this.changeSets.filter(changeSet => changeSet.changesetId < from_value)[0];
            if (nextChangeset) {
                this.vcService.getContent(nextChangeset.changesetId, this.path).subscribe(value => {
                    this.left = value;
                });
            }
        });
    }
}