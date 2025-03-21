import { NgModule } from "@angular/core";
import { DomSanitizer, SafeResourceUrl } from "@angular/platform-browser";
import { MatIconRegistry } from "@angular/material/icon";
@NgModule({})
export class ImagesModule {
  private path: string = "assets/images";
  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer) {
    this.matIconRegistry
      .addSvgIcon('licensync-grey',this.setPath(`${this.path}/logos/licensync-grey.svg`))
      .addSvgIcon('licensync-blue',this.setPath(`${this.path}/logos/licensync-blue.svg`))
      .addSvgIcon('licensync-title',this.setPath(`${this.path}/logos/licensync-title.svg`))
      .addSvgIcon('licensync-title-catch',this.setPath(`${this.path}/logos/licensync-title-catch.svg`));
  }
  private setPath(url: string): SafeResourceUrl {
    return this.domSanitizer.bypassSecurityTrustResourceUrl(url);
  }
}
