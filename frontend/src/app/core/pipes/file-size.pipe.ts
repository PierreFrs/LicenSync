import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fileSize',
  standalone: true
})
export class FileSizePipe implements PipeTransform {

  transform(value: number): unknown {

    if (isNaN(value)) return null;

    const units = ['bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    const power = Math.min(Math.floor((value ? Math.log(value) : 0) / Math.log(1024)), units.length - 1);
    const size = value / Math.pow(1024, power);
    return `${Math.round(size * 100) / 100} ${units[power]}`;
  }
}
