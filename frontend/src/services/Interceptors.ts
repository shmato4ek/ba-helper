import Axios from 'axios'
import { Config } from '../config';
import { firebaseAuth } from '../firebase';
import { LocalStorageService } from './local-storage';

export class InterceptorService {
  public static init(appConfig: Config): void {
    Axios.defaults.baseURL = `${appConfig.host}${appConfig.apiBaseUrl}`;

    InterceptorService.addRequestInterceptor();
  }

  private static requestInterceptorId = -1;

  private static addRequestInterceptor(): void {
    if (InterceptorService.requestInterceptorId === -1) {
      InterceptorService.requestInterceptorId = Axios.interceptors.request.use(
        async (axiosConfig) => {
          const token = LocalStorageService.getState('x-auth-token')
          axiosConfig.headers['x-auth-token'] = token;
          return axiosConfig;
        },
      );
    }
  }
}
