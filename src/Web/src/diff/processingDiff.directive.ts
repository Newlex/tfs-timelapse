import { Directive, ElementRef, Input, OnInit, OnChanges } from '@angular/core';
import { DiffMatchPatchService } from './diffMatchPatch.service';
import { Diff, DiffOp } from './diffMatchPatch';

@Directive({
  selector: '[processingDiff]'
})
export class ProcessingDiffDirective implements OnInit, OnChanges {
  @Input() left: string;
  @Input() right: string;

  public constructor(
    private el: ElementRef,
    private dmp: DiffMatchPatchService) {  }

  public ngOnInit(): void {
    this.updateHtml();
  }

  public ngOnChanges(): void {
    this.updateHtml();
  }

  private updateHtml(): void {
    this.el.nativeElement.innerHTML = this.createHtml(
      this.dmp.getProcessingDiff(this.left, this.right));
  }

  // TODO: Need to fix this for line diffs
  private createHtml(diffs: Array<Diff>): string {
    let html: string;
    html = '<div>';
    for (let diff of diffs) {
      diff[1] = diff[1].replace(/\n/g, '<br/>');
      var diffText = diff[1].replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
      if (diff[0] === DiffOp.Equal) {
        html += diffText;
      }
      if (diff[0] === DiffOp.Delete) {
        html += '<del>' + diffText + '</del>';
      }
      if (diff[0] === DiffOp.Insert) {
        html += '<ins>' + diffText + '</ins>';
      }
    }
    html += '</div>';
    return html;
  }
}
