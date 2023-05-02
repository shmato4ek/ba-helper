import axios from 'axios';
import { put, call, takeLeading } from 'redux-saga/effects';
import { ErrorCodes } from '../error';
import { globals } from '../services/is';
import {
  actionTypes, AppAction, FailureAppAction, FailureAppActionTypes, GetProject, PostProject, PostSubtask, PostTask, PutProject, PutSubtask, PutSubtaskState, PutTask, PutTaskAssign, PutTaskState,
} from './actions';

function* errorHandler(
  error: any,
  actionType: FailureAppActionTypes,
) {
  if (error.request && error.response) {
    yield put<FailureAppAction>({
      type: actionType,
      payload: {
        errors: ['stub error'],
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

function* getMe() {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.auth.me}`);
    });

    yield put({
      type: 'GET_ME_SUCCESS',
      payload: {
        me: response.data,
      },
    });
  } catch (error) {
    yield call(errorHandler, error, 'GET_ME_FAILURE');
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

function* getProject(getProject: GetProject) {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.project._}/${getProject.payload.id}`);
    });

    yield put<AppAction>({
      type: 'GET_PROJECT_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'GET_PROJECT_FAILURE');
  }
}

function* getProjects() {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.project.user}`);
    });

    console.log('@response.data');
    console.log(JSON.stringify(response.data, null, 2));

    yield put<AppAction>({
      type: 'GET_PROJECTS_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'GET_PROJECTS_FAILURE');
  }
}

function* getProjectsOwn() {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.project.userOwn}`);
    });

    yield put<AppAction>({
      type: 'GET_PROJECTS_OWN_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'GET_PROJECTS_OWN_FAILURE');
  }
}

function* postProject(postProject: PostProject) {
  try {
    console.log('Post project action: ' + postProject);

    const response: {
      data: any
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.auth.login}`, postProject.payload);
    });

    yield put<AppAction>({
      type: 'POST_PROJECT_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'POST_PROJECT_FAILURE');
  }
}

function* putProject(putProject: PutProject) {
  try {
    console.log('Put project action: ' + putProject);

    const response: {
      data: any
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.auth.login}`, putProject.payload);
    });

    yield put<AppAction>({
      type: 'PUT_PROJECT_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'PUT_PROJECT_FAILURE');
  }
}

function* postTask(postTask: PostTask) {
  try {
    console.log('Post task action: ' + postTask);

    const response: {
      data: any
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.task._}`, postTask.payload);
    });

    yield put<AppAction>({
      type: 'POST_TASK_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'POST_TASK_FAILURE');
  }
}

function* putTask(putTask: PutTask) {
  try {
    console.log('Put task action: ' + putTask);

    const response: {
      data: any
    } = yield call(() => {
      return axios.put(`${globals.endpoint}${globals.paths.task._}`, putTask.payload);
    });

    yield put<AppAction>({
      type: 'PUT_TASK_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'PUT_TASK_FAILURE');
  }
}

function* putTaskAssign(putTaskAssign: PutTaskAssign) {
  try {
    console.log('Put task assign action: ' + putTaskAssign);

    const response: {
      data: any
    } = yield call(() => {
      return axios.put(`${globals.endpoint}${globals.paths.task.assign}`, putTaskAssign.payload);
    });

    yield put<AppAction>({
      type: 'PUT_TASK_ASSIGN_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'PUT_TASK_ASSIGN_FAILURE');
  }
}

function* putTaskState(putTaskState: PutTaskState) {
  try {
    console.log('Put task state action: ' + putTaskState);

    const response: {
      data: any
    } = yield call(() => {
      return axios.put(`${globals.endpoint}${globals.paths.task.state}`, putTaskState.payload);
    });

    yield put<AppAction>({
      type: 'PUT_TASK_STATE_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'PUT_TASK_STATE_FAILURE');
  }
}

function* postSubtask(postSubtask: PostSubtask) {
  try {
    console.log('Post subtask action: ' + postSubtask);

    const response: {
      data: any
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.task.subtask}`, postSubtask.payload);
    });

    yield put<AppAction>({
      type: 'POST_SUBTASK_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'POST_SUBTASK_FAILURE');
  }
}

function* putSubtask(putSubtask: PutSubtask) {
  try {
    console.log('Put subtask action: ' + putSubtask);

    const response: {
      data: any
    } = yield call(() => {
      return axios.put(`${globals.endpoint}${globals.paths.task.subtask}`, putSubtask.payload);
    });

    yield put<AppAction>({
      type: 'PUT_SUBTASK_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'PUT_SUBTASK_FAILURE');
  }
}

function* putSubtaskState(putSubtaskState: PutSubtaskState) {
  try {
    console.log('Put subtask state action: ' + putSubtaskState);

    const response: {
      data: any
    } = yield call(() => {
      return axios.put(`${globals.endpoint}${globals.paths.task.subtaskState}`, putSubtaskState.payload);
    });

    yield put<AppAction>({
      type: 'PUT_SUBTASK_STATE_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'PUT_SUBTASK_STATE_FAILURE');
  }
}

export const rootSaga = function* rootSaga() {
  yield takeLeading(actionTypes.GET_ME, getMe);
  yield takeLeading(actionTypes.REGISTER, register);
  yield takeLeading(actionTypes.LOGIN, login);
  yield takeLeading(actionTypes.GET_PROJECT, getProject);
  yield takeLeading(actionTypes.GET_PROJECTS, getProjects);
  yield takeLeading(actionTypes.GET_PROJECTS_OWN, getProjectsOwn);
  yield takeLeading(actionTypes.POST_PROJECT, postProject);
  yield takeLeading(actionTypes.PUT_PROJECT, putProject);
  yield takeLeading(actionTypes.POST_TASK, postTask);
  yield takeLeading(actionTypes.PUT_TASK, putTask);
  yield takeLeading(actionTypes.PUT_TASK_ASSIGN, putTaskAssign);
  yield takeLeading(actionTypes.PUT_TASK_STATE, putTaskState);
  yield takeLeading(actionTypes.POST_SUBTASK, postSubtask);
  yield takeLeading(actionTypes.PUT_SUBTASK, putSubtask);
  yield takeLeading(actionTypes.PUT_SUBTASK_STATE, putSubtaskState);
};
