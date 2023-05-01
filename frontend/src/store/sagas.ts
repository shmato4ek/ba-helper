import axios from 'axios';
import { put, call, takeLatest, takeLeading } from 'redux-saga/effects';
import { ErrorCodes } from '../error';
import { firebaseAuth } from '../firebase';
import router from '../router';
import { globals } from '../services/is';
import {
  actionTypes, AppAction, FailureAppAction, FailureAppActionTypes,
} from './actions';

function* errorHandler(
  error: any,
  actionType: FailureAppActionTypes,
) {
  if (error.request && error.response) {
    yield put<FailureAppAction>({
      type: actionType,
      payload: {
        errors:
          error.response.data.message,
      },
    });
  } else if (
    error instanceof TypeError &&
    /network request failed/gi.test(error.message)
  ) {
    yield put<FailureAppAction>({
      type: actionType,
      payload: {
        errors: [ErrorCodes.SERVER_IS_UNAVAILABLE],
      },
    });
  } else {
    yield put<FailureAppAction>({
      type: actionType,
      payload: {
        errors: [ErrorCodes.INTERNAL_FRONTEND_ERROR],
      },
    });
  }
}

function* login() {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.auth.login}`);
    });

    yield put<AppAction>({
      type: 'LOGIN_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'LOGIN_FAILURE');
  }
}

function* register() {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.auth.register}`);
    });

    yield put<AppAction>({
      type: 'REGISTER_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'REGISTER_FAILURE');
  }
}

function* getMyself() {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.auth.me}`);
    });

    yield put({
      type: 'GET_ME_SUCCESS',
      payload: {
        myself: response.data,
      },
    });
  } catch (error) {
    yield call(errorHandler, error, 'GET_ME_FAILURE');
  }
}

export const rootSaga = function* rootSaga() {
  yield takeLeading(actionTypes.GET_ME, getMyself);
  yield takeLeading(actionTypes.LOGIN, login);
  yield takeLeading(actionTypes.REGISTER, register);
};
