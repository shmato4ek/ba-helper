import Axios from 'axios'
import { Config } from '../config';
import { firebaseAuth } from '../firebase';
import { LocalStorageService } from './local-storage';

export class InterceptorService {
  public static init(appConfig: Config): void {
    Axios.defaults.baseURL = `${appConfig.host}${appConfig.apiBaseUrl}`;

    InterceptorService.addRequestInterceptor();
    InterceptorService.addTimeInterceptor();
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

  private static addTimeInterceptor(): void {
    const axios = require('axios').default;

    axios.interceptors.request.use( (x: { meta: { requestStartedAt?: any; }; }) => {
        x.meta = x.meta || {}
        x.meta.requestStartedAt = new Date().getTime();
        return x;
    }) 
    axios.interceptors.response.use((x: { config: { url: any; meta: { requestStartedAt: number; }; }; }) => {
      console.log(`Execution time for: ${x.config.url} - ${new Date().getTime() - x.config.meta.requestStartedAt} ms`)
      return x;
    })
  }
}
