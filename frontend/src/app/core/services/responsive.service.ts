import { Injectable } from '@angular/core';
import {BreakpointObserver, Breakpoints, BreakpointState} from "@angular/cdk/layout";
import {BehaviorSubject, Subscription} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ResponsiveService {
  private padding = new BehaviorSubject<string>('p-4');
  private subscriptions = new Subscription();

  constructor(private responsive: BreakpointObserver) {
    this.subscribeToResponsiveBreakpoints();
  }

  get padding$() {
    return this.padding.asObservable();
  }

  private subscribeToResponsiveBreakpoints() {
    this.subscriptions.add(
      this.responsive.observe([
        Breakpoints.HandsetPortrait,
        Breakpoints.HandsetLandscape,
        Breakpoints.TabletPortrait,
        Breakpoints.TabletLandscape,
        Breakpoints.WebLandscape
      ]).subscribe(result => {
        this.updateLayoutForBreakpoints(result);
      })
    );
  }

  private updateLayoutForBreakpoints = ({ breakpoints }: BreakpointState) => {
    const paddings: Record<string, string> = {
      [Breakpoints.HandsetPortrait]: 'p-4',
      [Breakpoints.HandsetLandscape]: 'p-8',
      [Breakpoints.TabletPortrait]: 'p-8',
      [Breakpoints.TabletLandscape]: 'p-10',
      [Breakpoints.WebLandscape]: 'p-10',
    };

    const entries = Object.entries(breakpoints) as ([string, boolean])[];
    const activeEntry = entries.find((entry) => entry[1]);

    this.padding.next(activeEntry ? paddings[activeEntry[0]] : paddings[Breakpoints.HandsetPortrait]);
  }
}
