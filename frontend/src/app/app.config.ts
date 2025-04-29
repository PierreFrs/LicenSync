import {APP_INITIALIZER, ApplicationConfig, provideZoneChangeDetection} from "@angular/core";
import {provideRouter, withComponentInputBinding} from "@angular/router";
import { routes } from "./app.routes";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {provideAnimations} from "@angular/platform-browser/animations";
import {authInterceptor} from "./core/interceptors/auth.interceptor";
import {errorInterceptor} from "./core/interceptors/error.interceptor";
import {InitService} from "./core/services/init.service";
import {lastValueFrom} from "rxjs";
import {loadingInterceptor} from "./core/interceptors/loading.interceptor";

function initializeApp(initService: InitService) {
  return () => lastValueFrom(initService.init())
    .finally(() => {
      const splash = document.getElementById('initial-splash');
      if (splash) {
        splash.remove();
      }
  });
}
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({eventCoalescing: true}),
    provideRouter(routes, withComponentInputBinding()),
    provideAnimations(),
    provideHttpClient(withInterceptors([
      errorInterceptor,
      authInterceptor,
      loadingInterceptor
    ])),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      multi: true,
      deps: [InitService]
    }
  ]
};
