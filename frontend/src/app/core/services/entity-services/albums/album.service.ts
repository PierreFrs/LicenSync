import {Observable} from "rxjs";
import {inject, Injectable} from "@angular/core";
import { HttpClient } from "@angular/common/http";
import {Album} from "../../../models/album.model";
import {AlbumCard} from "../../../models/album-card.model";
import {Pagination} from "../../../models/pagination.model";
import {environment} from "../../../../../environments/environment";

@Injectable({
  providedIn: 'root',
})
export class AlbumService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.BASE_URL;

  getAlbumById(albumId: string | undefined): Observable<Album> {
    return this.httpClient.get<Album>(`${this.baseUrl}/Album/${albumId}`);
  }

  getAlbumsByUserId(userId: string): Observable<Album[]> {
    return this.httpClient.get<Album[]>(`${this.baseUrl}/Album/user/${userId}`);
  }

  postAlbum(albumDto: Album, albumVisual?: File): Observable<Album> {
    const formData: FormData = new FormData();

    if (albumVisual) {
      formData.append('albumVisual', albumVisual);
    }
    formData.append('albumTitle', albumDto.albumTitle);
    formData.append('userId', albumDto.userId);

    return this.httpClient.post<Album>(`${this.baseUrl}/Album/`, formData);
  }

  getAlbumCardsByUserId(userId: string): Observable<Pagination<AlbumCard>> {
    return this.httpClient.get<Pagination<AlbumCard>>(`${this.baseUrl}/Album/album-card-list/${userId}`);
  }
}
