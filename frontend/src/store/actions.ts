import { NavigateFunction } from 'react-router';
import {
  ClusterInfo,
  ClusterType,
  DocumentDto,
  LoginDto,
  Me,
  PostDocumentDto,
  PostProjectDto,
  PostSubtaskDto,
  PostTaskDto,
  ProjectDto,
  PutProjectDto,
  PutSubtaskApproveDto,
  PutSubtaskDto,
  PutSubtaskStateDto,
  PutTaskApproveDto,
  PutTaskAssignDto,
  PutTaskDto,
  PutTaskStateDto,
  PutUserDto,
  RegisterDto,
  StatisticDataInfo,
  SubtaskDto,
  TaskDto,
  UserDto
} from './types';

/* Action Types */
export const actionTypes = {
  LOGGED_IN: 'LOGGED_IN' as 'LOGGED_IN',

  //

  GET_ME: 'GET_ME' as 'GET_ME',
  GET_ME_SUCCESS: 'GET_ME_SUCCESS' as 'GET_ME_SUCCESS',
  GET_ME_FAILURE: 'GET_ME_FAILURE' as 'GET_ME_FAILURE',

  GET_ME_STATISTICS: 'GET_ME_STATISTICS' as 'GET_ME_STATISTICS',
  GET_ME_STATISTICS_SUCCESS: 'GET_ME_STATISTICS_SUCCESS' as 'GET_ME_STATISTICS_SUCCESS',
  GET_ME_STATISTICS_FAILURE: 'GET_ME_STATISTICS_FAILURE' as 'GET_ME_STATISTICS_FAILURE',

  LOGIN: 'LOGIN' as 'LOGIN',
  LOGIN_SUCCESS: 'LOGIN_SUCCESS' as 'LOGIN_SUCCESS',
  LOGIN_FAILURE: 'LOGIN_FAILURE' as 'LOGIN_FAILURE',

  REGISTER: 'REGISTER' as 'REGISTER',
  REGISTER_SUCCESS: 'REGISTER_SUCCESS' as 'REGISTER_SUCCESS',
  REGISTER_FAILURE: 'REGISTER_FAILURE' as 'REGISTER_FAILURE',

  // PUT /api/user
  PUT_USER: 'PUT_USER' as 'PUT_USER',
  PUT_USER_SUCCESS: 'PUT_USER_SUCCESS' as 'PUT_USER_SUCCESS',
  PUT_USER_FAILURE: 'PUT_USER_FAILURE' as 'PUT_USER_FAILURE',

  // DELETE /api/user
  DELETE_USER: 'DELETE_USER' as 'DELETE_USER',
  DELETE_USER_SUCCESS: 'DELETE_USER_SUCCESS' as 'DELETE_USER_SUCCESS',
  DELETE_USER_FAILURE: 'DELETE_USER_FAILURE' as 'DELETE_USER_FAILURE',

  LOG_OUT_ENDUSER: 'LOG_OUT_ENDUSER' as 'LOG_OUT_ENDUSER',
  LOG_OUT_ENDUSER_SUCCESS: 'LOG_OUT_ENDUSER_SUCCESS' as 'LOG_OUT_ENDUSER_SUCCESS',
  LOG_OUT_ENDUSER_FAILURE: 'LOG_OUT_ENDUSER_FAILURE' as 'LOG_OUT_ENDUSER_FAILURE',

  // GET /api/project/user/own
  GET_PROJECTS_OWN: 'GET_PROJECTS_OWN' as 'GET_PROJECTS_OWN',
  GET_PROJECTS_OWN_SUCCESS: 'GET_PROJECTS_OWN_SUCCESS' as 'GET_PROJECTS_OWN_SUCCESS',
  GET_PROJECTS_OWN_FAILURE: 'GET_PROJECTS_OWN_FAILURE' as 'GET_PROJECTS_OWN_FAILURE',

  // GET /api/project/user
  GET_PROJECTS: 'GET_PROJECTS' as 'GET_PROJECTS',
  GET_PROJECTS_SUCCESS: 'GET_PROJECTS_SUCCESS' as 'GET_PROJECTS_SUCCESS',
  GET_PROJECTS_FAILURE: 'GET_PROJECTS_FAILURE' as 'GET_PROJECTS_FAILURE',

  // GET /api/project/:id
  GET_PROJECT: 'GET_PROJECT' as 'GET_PROJECT',
  GET_PROJECT_SUCCESS: 'GET_PROJECT_SUCCESS' as 'GET_PROJECT_SUCCESS',
  GET_PROJECT_FAILURE: 'GET_PROJECT_FAILURE' as 'GET_PROJECT_FAILURE',

  // GET /api/project/statistics/:id
  GET_PROJECT_STATISTICS: 'GET_PROJECT_STATISTICS' as 'GET_PROJECT_STATISTICS',
  GET_PROJECT_STATISTICS_SUCCESS: 'GET_PROJECT_STATISTICS_SUCCESS' as 'GET_PROJECT_STATISTICS_SUCCESS',
  GET_PROJECT_STATISTICS_FAILURE: 'GET_PROJECT_STATISTICS_FAILURE' as 'GET_PROJECT_STATISTICS_FAILURE',

  // POST /api/project
  POST_PROJECT: 'POST_PROJECT' as 'POST_PROJECT',
  POST_PROJECT_SUCCESS: 'POST_PROJECT_SUCCESS' as 'POST_PROJECT_SUCCESS',
  POST_PROJECT_FAILURE: 'POST_PROJECT_FAILURE' as 'POST_PROJECT_FAILURE',

  // PUT /api/project
  PUT_PROJECT: 'PUT_PROJECT' as 'PUT_PROJECT',
  PUT_PROJECT_SUCCESS: 'PUT_PROJECT_SUCCESS' as 'PUT_PROJECT_SUCCESS',
  PUT_PROJECT_FAILURE: 'PUT_PROJECT_FAILURE' as 'PUT_PROJECT_FAILURE',

  // POST /api/task
  POST_TASK: 'POST_TASK' as 'POST_TASK',
  POST_TASK_SUCCESS: 'POST_TASK_SUCCESS' as 'POST_TASK_SUCCESS',
  POST_TASK_FAILURE: 'POST_TASK_FAILURE' as 'POST_TASK_FAILURE',

  // PUT /api/task
  PUT_TASK: 'PUT_TASK' as 'PUT_TASK',
  PUT_TASK_SUCCESS: 'PUT_TASK_SUCCESS' as 'PUT_TASK_SUCCESS',
  PUT_TASK_FAILURE: 'PUT_TASK_FAILURE' as 'PUT_TASK_FAILURE',

  // PUT /api/task/assign
  PUT_TASK_ASSIGN: 'PUT_TASK_ASSIGN' as 'PUT_TASK_ASSIGN',
  PUT_TASK_ASSIGN_SUCCESS: 'PUT_TASK_ASSIGN_SUCCESS' as 'PUT_TASK_ASSIGN_SUCCESS',
  PUT_TASK_ASSIGN_FAILURE: 'PUT_TASK_ASSIGN_FAILURE' as 'PUT_TASK_ASSIGN_FAILURE',

  // PUT /api/task/state
  PUT_TASK_STATE: 'PUT_TASK_STATE' as 'PUT_TASK_STATE',
  PUT_TASK_STATE_SUCCESS: 'PUT_TASK_STATE_SUCCESS' as 'PUT_TASK_STATE_SUCCESS',
  PUT_TASK_STATE_FAILURE: 'PUT_TASK_STATE_FAILURE' as 'PUT_TASK_STATE_FAILURE',

  // PUT /api/task/approve
  PUT_TASK_APPROVE: 'PUT_TASK_APPROVE' as 'PUT_TASK_APPROVE',
  PUT_TASK_APPROVE_SUCCESS: 'PUT_TASK_APPROVE_SUCCESS' as 'PUT_TASK_APPROVE_SUCCESS',
  PUT_TASK_APPROVE_FAILURE: 'PUT_TASK_APPROVE_FAILURE' as 'PUT_TASK_APPROVE_FAILURE',

  // POST /api/task/subtask
  POST_SUBTASK: 'POST_SUBTASK' as 'POST_SUBTASK',
  POST_SUBTASK_SUCCESS: 'POST_SUBTASK_SUCCESS' as 'POST_SUBTASK_SUCCESS',
  POST_SUBTASK_FAILURE: 'POST_SUBTASK_FAILURE' as 'POST_SUBTASK_FAILURE',

  // PUT /api/task/subtask
  PUT_SUBTASK: 'PUT_SUBTASK' as 'PUT_SUBTASK',
  PUT_SUBTASK_SUCCESS: 'PUT_SUBTASK_SUCCESS' as 'PUT_SUBTASK_SUCCESS',
  PUT_SUBTASK_FAILURE: 'PUT_SUBTASK_FAILURE' as 'PUT_SUBTASK_FAILURE',

  // PUT /api/task/subtask/state
  PUT_SUBTASK_STATE: 'PUT_SUBTASK_STATE' as 'PUT_SUBTASK_STATE',
  PUT_SUBTASK_STATE_SUCCESS: 'PUT_SUBTASK_STATE_SUCCESS' as 'PUT_SUBTASK_STATE_SUCCESS',
  PUT_SUBTASK_STATE_FAILURE: 'PUT_SUBTASK_STATE_FAILURE' as 'PUT_SUBTASK_STATE_FAILURE',

  // PUT /api/subtask/approve
  PUT_SUBTASK_APPROVE: 'PUT_SUBTASK_APPROVE' as 'PUT_SUBTASK_APPROVE',
  PUT_SUBTASK_APPROVE_SUCCESS: 'PUT_SUBTASK_APPROVE_SUCCESS' as 'PUT_SUBTASK_APPROVE_SUCCESS',
  PUT_SUBTASK_APPROVE_FAILURE: 'PUT_SUBTASK_APPROVE_FAILURE' as 'PUT_SUBTASK_APPROVE_FAILURE',

  // GET /api/document/user
  GET_DOCUMENTS: 'GET_DOCUMENTS' as 'GET_DOCUMENTS',
  GET_DOCUMENTS_SUCCESS: 'GET_DOCUMENTS_SUCCESS' as 'GET_DOCUMENTS_SUCCESS',
  GET_DOCUMENTS_FAILURE: 'GET_DOCUMENTS_FAILURE' as 'GET_DOCUMENTS_FAILURE',

  // POST /api/document
  POST_DOCUMENT: 'POST_DOCUMENT' as 'POST_DOCUMENT',
  POST_DOCUMENT_SUCCESS: 'POST_DOCUMENT_SUCCESS' as 'POST_DOCUMENT_SUCCESS',
  POST_DOCUMENT_FAILURE: 'POST_DOCUMENT_FAILURE' as 'POST_DOCUMENT_FAILURE',

  // GET /api/download
  DOCUMENT_DOWNLOAD: 'DOCUMENT_DOWNLOAD' as 'DOCUMENT_DOWNLOAD',
  DOCUMENT_DOWNLOAD_SUCCESS: 'DOCUMENT_DOWNLOAD_SUCCESS' as 'DOCUMENT_DOWNLOAD_SUCCESS',
  DOCUMENT_DOWNLOAD_FAILURE: 'DOCUMENT_DOWNLOAD_FAILURE' as 'DOCUMENT_DOWNLOAD_FAILURE',

  /** ui actions */
  SET_PROVIDER_INITIAL_VALUES: 'SET_PROVIDER_INITIAL_VALUES' as 'SET_PROVIDER_INITIAL_VALUES',
  SET_PROVIDER_VALUES: 'SET_PROVIDER_VALUES' as 'SET_PROVIDER_VALUES',

  SET_CHOSEN_SERVICE_TYPE: 'SET_CHOSEN_SERVICE_TYPE' as 'SET_CHOSEN_SERVICE_TYPE',
};

/* Actions */
export interface ErrorPayload {
  payload: { errors: string[]; };
}

export interface LoggedIn {
  type: typeof actionTypes.LOGGED_IN;
}

//

export interface GetMe {
  type: typeof actionTypes.GET_ME;
}

export interface GetMeSuccess {
  type: typeof actionTypes.GET_ME_SUCCESS;
  payload: {
    me: Me;
  };
}

export interface GetMeFailure extends ErrorPayload {
  type: typeof actionTypes.GET_ME_FAILURE;
}

export interface GetMeStatistics {
  type: typeof actionTypes.GET_ME_STATISTICS;
}

export interface GetMeStatisticsSuccess {
  type: typeof actionTypes.GET_ME_STATISTICS_SUCCESS;
  payload: StatisticDataInfo[];
}

export interface GetMeStatisticsFailure extends ErrorPayload {
  type: typeof actionTypes.GET_ME_STATISTICS_FAILURE;
}

export interface Login {
  type: typeof actionTypes.LOGIN;
  payload: LoginDto;
  navigate: NavigateFunction;
}

export interface LoginSuccess {
  type: typeof actionTypes.LOGIN_SUCCESS;
  payload: {
    user: UserDto;
    token: { accessToken: string; }
  };
}

export interface LoginFailure extends ErrorPayload {
  type: typeof actionTypes.LOGIN_FAILURE;
}

export interface PutUser {
  type: typeof actionTypes.PUT_USER;
  payload: PutUserDto;
}

export interface PutUserSuccess {
  type: typeof actionTypes.PUT_USER_SUCCESS;
  payload: UserDto;
}

export interface PutUserFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_USER_FAILURE;
}

export interface DeleteUser {
  type: typeof actionTypes.DELETE_USER;
  navigate: NavigateFunction;
}

export interface DeleteUserSuccess {
  type: typeof actionTypes.DELETE_USER_SUCCESS;
}

export interface DeleteUserFailure extends ErrorPayload {
  type: typeof actionTypes.DELETE_USER_FAILURE;
}

export interface Register {
  type: typeof actionTypes.REGISTER;
  payload: RegisterDto;
  navigate: NavigateFunction;
}

export interface RegisterSuccess {
  type: typeof actionTypes.REGISTER_SUCCESS;
  payload: {
    user: UserDto;
    token: { accessToken: string; }
  };
}

export interface RegisterFailure extends ErrorPayload {
  type: typeof actionTypes.REGISTER_FAILURE;
}

export interface LogOut {
  type: typeof actionTypes.LOG_OUT_ENDUSER;
  navigate: NavigateFunction;
}

export interface LogOutSuccess {
  type: typeof actionTypes.LOG_OUT_ENDUSER_SUCCESS;
}

export interface LogOutFailure extends ErrorPayload {
  type: typeof actionTypes.LOG_OUT_ENDUSER_FAILURE;
}

export interface GetProjects {
  type: typeof actionTypes.GET_PROJECTS;
}

export interface GetProjectsSuccess {
  type: typeof actionTypes.GET_PROJECTS_SUCCESS;
  payload: ProjectDto[];
}

export interface GetProjectsFailure extends ErrorPayload {
  type: typeof actionTypes.GET_PROJECTS_FAILURE;
}

export interface GetProjectsOwn {
  type: typeof actionTypes.GET_PROJECTS_OWN;
}

export interface GetProjectsOwnSuccess {
  type: typeof actionTypes.GET_PROJECTS_OWN_SUCCESS;
  payload: ProjectDto[];
}

export interface GetProjectsOwnFailure extends ErrorPayload {
  type: typeof actionTypes.GET_PROJECTS_OWN_FAILURE;
}

export interface GetProject {
  type: typeof actionTypes.GET_PROJECT;
  payload: { id: number; }
}

export interface GetProjectSuccess {
  type: typeof actionTypes.GET_PROJECT_SUCCESS;
  payload: ProjectDto;
}

export interface GetProjectFailure extends ErrorPayload {
  type: typeof actionTypes.GET_PROJECT_FAILURE;
}

export interface GetProjectStatistics {
  type: typeof actionTypes.GET_PROJECT_STATISTICS;
  payload: { id: number; type: ClusterType; }
}

export interface GetProjectStatisticsSuccess {
  type: typeof actionTypes.GET_PROJECT_STATISTICS_SUCCESS;
  payload: ClusterInfo[];
}

export interface GetProjectStatisticsFailure extends ErrorPayload {
  type: typeof actionTypes.GET_PROJECT_STATISTICS_FAILURE;
}

export interface PostProject {
  type: typeof actionTypes.POST_PROJECT;
  payload: PostProjectDto;
  navigate: NavigateFunction;
}

export interface PostProjectSuccess {
  type: typeof actionTypes.POST_PROJECT_SUCCESS;
  payload: ProjectDto;
}

export interface PostProjectFailure extends ErrorPayload {
  type: typeof actionTypes.POST_PROJECT_FAILURE;
}

export interface PutProject {
  type: typeof actionTypes.PUT_PROJECT;
  payload: PutProjectDto;
}

export interface PutProjectSuccess {
  type: typeof actionTypes.PUT_PROJECT_SUCCESS;
  payload: ProjectDto;
}

export interface PutProjectFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_PROJECT_FAILURE;
}

export interface PostTask {
  type: typeof actionTypes.POST_TASK;
  payload: PostTaskDto;
}

export interface PostTaskSuccess {
  type: typeof actionTypes.POST_TASK_SUCCESS;
  payload: TaskDto;
}

export interface PostTaskFailure extends ErrorPayload {
  type: typeof actionTypes.POST_TASK_FAILURE;
}

export interface PutTask {
  type: typeof actionTypes.PUT_TASK;
  payload: PutTaskDto;
}

export interface PutTaskSuccess {
  type: typeof actionTypes.PUT_TASK_SUCCESS;
  payload: TaskDto;
}

export interface PutTaskFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_TASK_FAILURE;
}

export interface PutTaskAssign {
  type: typeof actionTypes.PUT_TASK_ASSIGN;
  payload: PutTaskAssignDto;
}

export interface PutTaskAssignSuccess {
  type: typeof actionTypes.PUT_TASK_ASSIGN_SUCCESS;
  payload: TaskDto;
}

export interface PutTaskAssignFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_TASK_ASSIGN_FAILURE;
}

export interface PutTaskState {
  type: typeof actionTypes.PUT_TASK_STATE;
  payload: PutTaskStateDto;
}

export interface PutTaskStateSuccess {
  type: typeof actionTypes.PUT_TASK_STATE_SUCCESS;
  payload: TaskDto;
}

export interface PutTaskStateFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_TASK_STATE_FAILURE;
}

export interface PutTaskApprove {
  type: typeof actionTypes.PUT_TASK_APPROVE;
  payload: PutTaskApproveDto;
}

export interface PutTaskApproveSuccess {
  type: typeof actionTypes.PUT_TASK_APPROVE_SUCCESS;
  payload: TaskDto;
}

export interface PutTaskApproveFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_TASK_APPROVE_FAILURE;
}


export interface PostSubtask {
  type: typeof actionTypes.POST_SUBTASK;
  payload: PostSubtaskDto;
}

export interface PostSubtaskSuccess {
  type: typeof actionTypes.POST_SUBTASK_SUCCESS;
  payload: SubtaskDto;
}

export interface PostSubtaskFailure extends ErrorPayload {
  type: typeof actionTypes.POST_SUBTASK_FAILURE;
}

export interface PutSubtask {
  type: typeof actionTypes.PUT_SUBTASK;
  payload: PutSubtaskDto;
}

export interface PutSubtaskSuccess {
  type: typeof actionTypes.PUT_SUBTASK_SUCCESS;
  payload: SubtaskDto;
}

export interface PutSubtaskFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_SUBTASK_FAILURE;
}

export interface PutSubtaskState {
  type: typeof actionTypes.PUT_SUBTASK_STATE;
  payload: PutSubtaskStateDto;
}

export interface PutSubtaskStateSuccess {
  type: typeof actionTypes.PUT_SUBTASK_STATE_SUCCESS;
  payload: SubtaskDto;
}

export interface PutSubtaskApprove {
  type: typeof actionTypes.PUT_SUBTASK_APPROVE;
  payload: PutSubtaskApproveDto;
}

export interface PutSubtaskApproveSuccess {
  type: typeof actionTypes.PUT_SUBTASK_APPROVE_SUCCESS;
  payload: SubtaskDto;
}

export interface PutSubtaskApproveFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_SUBTASK_APPROVE_FAILURE;
}

export interface PutSubtaskStateFailure extends ErrorPayload {
  type: typeof actionTypes.PUT_SUBTASK_STATE_FAILURE;
}

export interface GetDocuments {
  type: typeof actionTypes.GET_DOCUMENTS;
}

export interface GetDocumentsSuccess {
  type: typeof actionTypes.GET_DOCUMENTS_SUCCESS;
  payload: DocumentDto[];
}

export interface GetDocumentsFailure extends ErrorPayload {
  type: typeof actionTypes.GET_DOCUMENTS_FAILURE;
}

export interface PostDocument {
  type: typeof actionTypes.POST_DOCUMENT;
  payload: PostDocumentDto;
  navigate: NavigateFunction;
}

export interface PostDocumentSuccess {
  type: typeof actionTypes.POST_DOCUMENT_SUCCESS;
  payload: DocumentDto;
}

export interface PostDocumentFailure extends ErrorPayload {
  type: typeof actionTypes.POST_DOCUMENT_FAILURE;
}

export interface DocumentDownload {
  type: typeof actionTypes.DOCUMENT_DOWNLOAD;
  payload: number;
}

export interface DocumentDownloadSuccess {
  type: typeof actionTypes.DOCUMENT_DOWNLOAD_SUCCESS;
  payload: string;
}

export interface DocumentDownloadFailure extends ErrorPayload {
  type: typeof actionTypes.DOCUMENT_DOWNLOAD_FAILURE;
}

/** UI actions */
export type FailureAppActionTypes =
  | typeof actionTypes.GET_ME_FAILURE
  | typeof actionTypes.GET_ME_STATISTICS_FAILURE
  | typeof actionTypes.REGISTER_FAILURE
  | typeof actionTypes.PUT_USER_FAILURE
  | typeof actionTypes.DELETE_USER_FAILURE
  | typeof actionTypes.LOGIN_FAILURE
  | typeof actionTypes.GET_PROJECTS_FAILURE
  | typeof actionTypes.GET_PROJECTS_OWN_FAILURE
  | typeof actionTypes.GET_PROJECT_FAILURE
  | typeof actionTypes.GET_PROJECT_STATISTICS_FAILURE
  | typeof actionTypes.POST_PROJECT_FAILURE
  | typeof actionTypes.PUT_PROJECT_FAILURE
  | typeof actionTypes.POST_TASK_FAILURE
  | typeof actionTypes.PUT_TASK_FAILURE
  | typeof actionTypes.PUT_TASK_ASSIGN_FAILURE
  | typeof actionTypes.PUT_TASK_STATE_FAILURE
  | typeof actionTypes.PUT_TASK_APPROVE_FAILURE
  | typeof actionTypes.POST_SUBTASK_FAILURE
  | typeof actionTypes.PUT_SUBTASK_FAILURE
  | typeof actionTypes.PUT_SUBTASK_STATE_FAILURE
  | typeof actionTypes.PUT_SUBTASK_APPROVE_FAILURE
  | typeof actionTypes.GET_DOCUMENTS_FAILURE
  | typeof actionTypes.POST_DOCUMENT_FAILURE
  | typeof actionTypes.DOCUMENT_DOWNLOAD_FAILURE
  | typeof actionTypes.LOG_OUT_ENDUSER_FAILURE
  ;

export type FailureAppAction =
  | GetMeFailure
  | GetMeStatisticsFailure
  | LoginFailure
  | RegisterFailure
  | PutUserFailure
  | DeleteUserFailure
  | LogOutFailure
  | GetProjectsFailure
  | GetProjectsOwnFailure
  | GetProjectFailure
  | GetProjectStatisticsFailure
  | PostProjectFailure
  | PutProjectFailure
  | PostTaskFailure
  | PutTaskFailure
  | PutTaskAssignFailure
  | PutTaskStateFailure
  | PutTaskApproveFailure
  | PostSubtaskFailure
  | PutSubtaskFailure
  | PutSubtaskStateFailure
  | PutSubtaskApproveFailure
  | GetDocumentsFailure
  | PostDocumentFailure
  | DocumentDownloadFailure
  | LogOutFailure
  ;

export type AppAction =
  | LoggedIn
  | GetMe
  | GetMeSuccess
  | GetMeFailure
  | GetMeStatistics
  | GetMeStatisticsSuccess
  | GetMeStatisticsFailure
  | Login
  | LoginSuccess
  | LoginFailure
  | Register
  | RegisterSuccess
  | RegisterFailure
  | PutUser
  | PutUserSuccess
  | PutUserFailure
  | DeleteUser
  | DeleteUserSuccess
  | DeleteUserFailure
  | LogOut
  | LogOutSuccess
  | LogOutFailure
  | GetProjects
  | GetProjectsSuccess
  | GetProjectsFailure
  | GetProjectsOwn
  | GetProjectsOwnSuccess
  | GetProjectsOwnFailure
  | GetProject
  | GetProjectSuccess
  | GetProjectFailure
  | GetProjectStatistics
  | GetProjectStatisticsSuccess
  | GetProjectStatisticsFailure
  | PostProject
  | PostProjectSuccess
  | PostProjectFailure
  | PutProject
  | PutProjectSuccess
  | PutProjectFailure
  | PostTask
  | PostTaskSuccess
  | PostTaskFailure
  | PutTask
  | PutTaskSuccess
  | PutTaskFailure
  | PutTaskAssign
  | PutTaskAssignSuccess
  | PutTaskAssignFailure
  | PutTaskState
  | PutTaskStateSuccess
  | PutTaskStateFailure
  | PutTaskApprove
  | PutTaskApproveSuccess
  | PutTaskApproveFailure
  | PostSubtask
  | PostSubtaskSuccess
  | PostSubtaskFailure
  | PutSubtask
  | PutSubtaskSuccess
  | PutSubtaskFailure
  | PutSubtaskState
  | PutSubtaskStateSuccess
  | PutSubtaskStateFailure
  | PutSubtaskApprove
  | PutSubtaskApproveSuccess
  | PutSubtaskApproveFailure
  | GetDocuments
  | GetDocumentsSuccess
  | GetDocumentsFailure
  | PostDocument
  | PostDocumentSuccess
  | PostDocumentFailure
  | DocumentDownload
  | DocumentDownloadSuccess
  | DocumentDownloadFailure
  | LogOut
  | LogOutSuccess
  | LogOutFailure
  ;
