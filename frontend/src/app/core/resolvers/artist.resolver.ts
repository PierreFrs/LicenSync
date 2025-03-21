import {ArtistService} from "../services/entity-services/artists/artist.service";
import {ActivatedRouteSnapshot, ResolveFn} from "@angular/router";
import {Artist} from "../models/artist.model";
import {inject} from "@angular/core";

export const artistResolver: ResolveFn<Artist[]> = (
  route: ActivatedRouteSnapshot
) => {
  return inject(ArtistService).getArtistsByTrackId(route.paramMap.get('id')!);
};
