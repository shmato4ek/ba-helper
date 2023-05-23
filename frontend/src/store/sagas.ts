import axios from 'axios';
import { put, call, takeLeading } from 'redux-saga/effects';
import { ErrorCodes } from '../error';
import { globals } from '../services/is';
import {
  actionTypes, AppAction, DeleteUser, DocumentDownload, FailureAppAction, FailureAppActionTypes, GetProject, GetProjectStatistics, Login, LoginSuccess, PostDocument, PostProject, PostSubtask, PostTask, PutProject, PutSubtask, PutSubtaskState, PutTask, PutTaskAssign, PutTaskState, PutUser, Register, RegisterSuccess,
} from './actions';
import { PutUserDto } from './types';
import { LocalStorageService } from '../services/local-storage';

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

function* getMeStatistics() {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.user.statisticsMe}`);
    });
    
    console.log('@response.data');
    console.log(JSON.stringify(response.data, null, 2));

    yield put({
      type: 'GET_ME_STATISTICS_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'GET_ME_STATISTICS_FAILURE');
  }
}

function* login(login: Login) {
  try {
    console.log('@---');
    const response: {
      data: LoginSuccess['payload']
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.auth.login}`, login.payload);
    });

    yield put<AppAction>({
      type: 'LOGIN_SUCCESS',
      payload: response.data
    });

    LocalStorageService.setState('x-auth-token', response.data.token.accessToken)

    login.navigate(`/my-projects`);
  } catch (error) {
    yield call(errorHandler, error, 'LOGIN_FAILURE');
  }
}

function* register(register: Register) {
  try {
    const response: {
      data: RegisterSuccess['payload']
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.auth.register}`, register.payload);
    });

    yield put<AppAction>({
      type: 'REGISTER_SUCCESS',
      payload: response.data
    });

    LocalStorageService.setState('x-auth-token', response.data.token.accessToken)

    register.navigate(`/services`);
  } catch (error) {
    yield call(errorHandler, error, 'REGISTER_FAILURE');
  }
}

function* putUser(putUser: PutUser) {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.put(`${globals.endpoint}${globals.paths.user._}`, putUser.payload);
    });

    yield put<AppAction>({
      type: 'PUT_USER_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'PUT_USER_FAILURE');
  }
}

function* deleteUser(deleteUser: DeleteUser) {
  try {
    yield call(() => {
      return axios.delete(`${globals.endpoint}${globals.paths.user._}`);
    });

    LocalStorageService.clearState();

    yield put<AppAction>({
      type: 'DELETE_USER_SUCCESS',
    });

    deleteUser.navigate('/')
  } catch (error) {
    yield call(errorHandler, error, 'DELETE_USER_FAILURE');
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

function* getProjectStatistics(getProjectStatistics: GetProjectStatistics) {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.project.stats}/${getProjectStatistics.payload.id}`);
    });

    yield put<AppAction>({
      type: 'GET_PROJECT_STATISTICS_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'GET_PROJECT_STATISTICS_FAILURE');
  }
}

function* getProjects() {
  try {
    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.project.user}`);
    });

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
      return axios.post(`${globals.endpoint}${globals.paths.project._}`, postProject.payload);
    });

    yield put<AppAction>({
      type: 'POST_PROJECT_SUCCESS',
      payload: response.data
    });

    postProject.navigate(`/projects/${response.data.id}`);
  } catch (error) {
    yield call(errorHandler, error, 'POST_PROJECT_FAILURE');
  }
}

function* putProject(putProject: PutProject) {
  try {
    console.log('Put project action');
    console.log(JSON.stringify(putProject, null, 2));

    const response: {
      data: any
    } = yield call(() => {
      return axios.put(`${globals.endpoint}${globals.paths.project._}`, putProject.payload);
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

function* getDocuments() {
  try {
    console.log('Get documents state action');

    const response: {
      data: any
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.document.user}`);
    });

    yield put<AppAction>({
      type: 'GET_DOCUMENTS_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'GET_DOCUMENTS_FAILURE');
  }
}

function* postDocument(postDocument: PostDocument) {
  try {
    console.log('Post document state action');

    const response: {
      data: any
    } = yield call(() => {
      return axios.post(`${globals.endpoint}${globals.paths.document._}`, postDocument.payload);
    });

    yield put<AppAction>({
      type: 'POST_DOCUMENT_SUCCESS',
      payload: response.data
    });

    postDocument.navigate(`/documents`);
  } catch (error) {
    yield call(errorHandler, error, 'POST_DOCUMENT_FAILURE');
  }
}

function* documentDownload(documentDownload: DocumentDownload) {
  try {
    console.log('Document download state action');

    const response: {
      data: any;
      blob: any;
    } = yield call(() => {
      return axios.get(`${globals.endpoint}${globals.paths.download._}/${documentDownload.payload}`, {
        data: documentDownload.payload
        // headers: {
        //   'Content-Type': 'application/problem+json; charset=utf-8'
        // }
      });
    });
    console.log('@response');
    console.log(JSON.stringify(response.data, null, 2));


    window.open(`${globals.endpoint}${globals.paths.download._}/${documentDownload.payload}`, '_blank');

    yield put<AppAction>({
      type: 'DOCUMENT_DOWNLOAD_SUCCESS',
      payload: response.data
    });
  } catch (error) {
    yield call(errorHandler, error, 'DOCUMENT_DOWNLOAD_FAILURE');
  }
}

export const rootSaga = function* rootSaga() {
  yield takeLeading(actionTypes.GET_ME, getMe);
  yield takeLeading(actionTypes.GET_ME_STATISTICS, getMeStatistics);
  yield takeLeading(actionTypes.LOGIN, login);
  yield takeLeading(actionTypes.REGISTER, register);
  yield takeLeading(actionTypes.PUT_USER, putUser);
  yield takeLeading(actionTypes.DELETE_USER, deleteUser);
  yield takeLeading(actionTypes.GET_PROJECT, getProject);
  yield takeLeading(actionTypes.GET_PROJECT_STATISTICS, getProjectStatistics);
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
  yield takeLeading(actionTypes.GET_DOCUMENTS, getDocuments);
  yield takeLeading(actionTypes.POST_DOCUMENT, postDocument);
  yield takeLeading(actionTypes.DOCUMENT_DOWNLOAD, documentDownload);
};
