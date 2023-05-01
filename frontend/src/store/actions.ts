import { ErrorCodes } from '../error';
import { BisFunctionDto } from './bis-function.types';
import { BisMetriscDto, LoginDto, Myself, RegisterDto, UserDto } from './types';

/* Action Types */
export const actionTypes = {
  LOGGED_IN: 'LOGGED_IN' as 'LOGGED_IN',

  //

  GET_ME: 'GET_ME' as 'GET_ME',
  GET_ME_SUCCESS: 'GET_ME_SUCCESS' as 'GET_ME_SUCCESS',
  GET_ME_FAILURE: 'GET_ME_FAILURE' as 'GET_ME_FAILURE',

  LOGIN: 'LOGIN' as 'LOGIN',
  LOGIN_SUCCESS: 'LOGIN_SUCCESS' as 'LOGIN_SUCCESS',
  LOGIN_FAILURE: 'LOGIN_FAILURE' as 'LOGIN_FAILURE',

  REGISTER: 'REGISTER' as 'REGISTER',
  REGISTER_SUCCESS: 'REGISTER_SUCCESS' as 'REGISTER_SUCCESS',
  REGISTER_FAILURE: 'REGISTER_FAILURE' as 'REGISTER_FAILURE',

  LOG_OUT_ENDUSER: 'LOG_OUT_ENDUSER' as 'LOG_OUT_ENDUSER',
  LOG_OUT_ENDUSER_SUCCESS: 'LOG_OUT_ENDUSER_SUCCESS' as 'LOG_OUT_ENDUSER_SUCCESS',
  LOG_OUT_ENDUSER_FAILURE: 'LOG_OUT_ENDUSER_FAILURE' as 'LOG_OUT_ENDUSER_FAILURE',

  POST_PROJECT: 'POST_PROJECT' as 'POST_PROJECT',
  POST_PROJECT_SUCCESS: 'POST_PROJECT_SUCCESS' as 'POST_PROJECT_SUCCESS',
  POST_PROJECT_FAILURE: 'POST_PROJECT_FAILURE' as 'POST_PROJECT_FAILURE',

  PUT_PROJECT: 'PUT_PROJECT' as 'PUT_PROJECT',
  PUT_PROJECT_SUCCESS: 'PUT_PROJECT_SUCCESS' as 'PUT_PROJECT_SUCCESS',
  PUT_PROJECT_FAILURE: 'PUT_PROJECT_FAILURE' as 'PUT_PROJECT_FAILURE',

  /** ui actions */
  SET_PROVIDER_INITIAL_VALUES: 'SET_PROVIDER_INITIAL_VALUES' as 'SET_PROVIDER_INITIAL_VALUES',
  SET_PROVIDER_VALUES: 'SET_PROVIDER_VALUES' as 'SET_PROVIDER_VALUES',

  SET_CHOSEN_SERVICE_TYPE: 'SET_CHOSEN_SERVICE_TYPE' as 'SET_CHOSEN_SERVICE_TYPE',
};

/* Actions */
export interface LoggedIn {
  type: typeof actionTypes.LOGGED_IN;
}

//

export interface GetMyself {
  type: typeof actionTypes.GET_ME;
}

export interface GetMyselfSuccess {
  type: typeof actionTypes.GET_ME_SUCCESS;
  payload: {
    myself: Myself;
  };
}

export interface GetMyselfFailure {
  type: typeof actionTypes.GET_ME_FAILURE;
  payload: {
    errors: ErrorCodes[];
  };
}

export interface Login {
  type: typeof actionTypes.LOGIN;
  payload: LoginDto;
}

export interface LoginSuccess {
  type: typeof actionTypes.LOGIN_SUCCESS;
  payload: UserDto;
}

export interface LoginFailure {
  type: typeof actionTypes.LOGIN_FAILURE;
  payload: {
    errors: ErrorCodes[];
  };
}

export interface Register {
  type: typeof actionTypes.REGISTER;
  payload: RegisterDto;
}

export interface RegisterSuccess {
  type: typeof actionTypes.REGISTER_SUCCESS;
  payload: UserDto;
}

export interface RegisterFailure {
  type: typeof actionTypes.REGISTER_FAILURE;
  payload: {
    errors: ErrorCodes[];
  };
}

export interface LogOut {
  type: typeof actionTypes.LOG_OUT_ENDUSER;
}

export interface LogOutSuccess {
  type: typeof actionTypes.LOG_OUT_ENDUSER_SUCCESS;
}

export interface LogOutFailure {
  type: typeof actionTypes.LOG_OUT_ENDUSER_FAILURE;
  payload: {
    errors: ErrorCodes[];
  };
}

export interface PostProject {
  type: typeof actionTypes.LOG_OUT_ENDUSER;
}

export interface PostProjectSuccess {
  type: typeof actionTypes.LOG_OUT_ENDUSER_SUCCESS;
}

export interface PostProjectFailure {
  type: typeof actionTypes.LOG_OUT_ENDUSER_FAILURE;
  payload: {
    errors: ErrorCodes[];
  };
}

/** UI actions */



export type FailureAppActionTypes =
  typeof actionTypes.GET_ME_FAILURE |
  typeof actionTypes.REGISTER_FAILURE |
  typeof actionTypes.LOGIN_FAILURE
  ;

export type FailureAppAction =
  | GetMyselfFailure
  | LoginFailure
  | RegisterFailure
  | LogOutFailure;

export type AppAction =
  | LoggedIn
  | GetMyself
  | GetMyselfSuccess
  | GetMyselfFailure
  | Login
  | LoginSuccess
  | LoginFailure
  | Register
  | RegisterSuccess
  | RegisterFailure
  | LogOut
  | LogOutSuccess
  | LogOutFailure;
