import produce from 'immer';
import { actionTypes, AppAction } from './actions';
import { DocumentDto, Me, ProjectDto, TaskDto } from './types';
import { BisFunctionDto } from './bis-function.types';
import { AlphaProject, BetaProject } from '../mock';

/* Initial State */
export const RootReducerInitialState = {
  forms: {
    provider: {
      providerIndex: null as string | null,
    },
  },

  ui: {
    //
    monitoringAndPlanning: {
      from: 202201 as number | null,
      to: 202301 as number | null,
    }
  },

  actions: {
    /**
     * Basically acts as initial auth route to check whether user is authenticated or not
     */
    getMe: {
      loading: false as boolean,
      errors: null as string[] | null,

      /**
       * Is modified by the following actions
       *  - postEnduserSuccess
       *  - loginEnduserSuccess
       *  - getMeSuccess
       *  - getMeFailure
       */
      success: null as boolean | null, // is user logged in
    },

    login: {
      loading: false as boolean,
      errors: null as string[] | null,
      success: null as boolean | null, // is there been at least one successful register
    },

    register: {
      loading: false as boolean,
      errors: null as string[] | null,
      success: null as boolean | null, // is there been at least one successful register
    },

    putUser: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    deleteUser: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    logout: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    getProjectsOwn: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    getProjects: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    getProject: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    postProject: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    putProject: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    postTask: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    putTask: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    putTaskAssign: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    putTaskState: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    postSubtask: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    putSubtask: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    putSubtaskState: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    getDocuments: {
      loading: false as boolean,
      errors: null as string[] | null,
    },

    postDocument: {
      loading: false as boolean,
      errors: null as string[] | null,
    },
  },

  /**
   * Is modified by the following actions
   *  - getMe
   */
  me: null as Me | null,
  projects: null as ProjectDto[] | null,
  projectsOwn: null as ProjectDto[] | null,
  currentProject: null as ProjectDto | null,
  documents: null as DocumentDto[] | null,
};

export type AppState = typeof RootReducerInitialState;

/* Reducer */
export const RootReducer = produce(
  (draft: AppState, action: AppAction) => {
    switch (action.type) {
      case actionTypes.GET_ME:
        draft.actions.getMe.errors = null;
        draft.actions.getMe.loading = true;
        break;
      case actionTypes.GET_ME_SUCCESS:
        draft.me = action.payload.me;
        draft.actions.getMe.errors = null;
        draft.actions.getMe.loading = false;
        draft.actions.getMe.success = true;
        break;
      case actionTypes.GET_ME_FAILURE:
        draft.actions.getMe.errors = action.payload.errors;
        draft.actions.getMe.loading = false;
        draft.actions.getMe.success = false;
        break;

      case actionTypes.LOGIN:
        console.log('actionTypes.LOGIN')
        draft.actions.login.errors = null;
        draft.actions.login.loading = true;
        break;
      case actionTypes.LOGIN_SUCCESS:
        console.log('actionTypes.LOGIN_SUCCESS')
        draft.actions.login.errors = null;
        draft.actions.login.loading = false;
        break;
      case actionTypes.LOGIN_FAILURE:
        console.log('actionTypes.LOGIN_FAILURE')
        draft.actions.login.errors = action.payload.errors;
        draft.actions.login.loading = false;
        break;

      case actionTypes.PUT_USER:
        console.log('actionTypes.PUT_USER')
        draft.actions.putUser.errors = null;
        draft.actions.putUser.loading = true;
        break;
      case actionTypes.PUT_USER_SUCCESS:
        console.log('actionTypes.PUT_USER_SUCCESS')
        draft.actions.putUser.errors = null;
        draft.actions.putUser.loading = false;
        break;
      case actionTypes.PUT_USER_FAILURE:
        console.log('actionTypes.PUT_USER_FAILURE')
        draft.actions.putUser.errors = action.payload.errors;
        draft.actions.putUser.loading = false;
        break;

      case actionTypes.DELETE_USER:
        console.log('actionTypes.DELETE_USER')
        draft.actions.deleteUser.errors = null;
        draft.actions.deleteUser.loading = true;
        break;
      case actionTypes.DELETE_USER_SUCCESS:
        console.log('actionTypes.DELETE_USER_SUCCESS')
        draft.actions.deleteUser.errors = null;
        draft.actions.deleteUser.loading = false;
        break;
      case actionTypes.DELETE_USER_FAILURE:
        console.log('actionTypes.DELETE_USER_FAILURE')
        draft.actions.deleteUser.errors = action.payload.errors;
        draft.actions.deleteUser.loading = false;
        break;

      case actionTypes.REGISTER:
        console.log('actionTypes.REGISTER')
        draft.actions.register.errors = null;
        draft.actions.register.loading = true;
        break;
      case actionTypes.REGISTER_SUCCESS:
        console.log('actionTypes.REGISTER_SUCCESS')
        draft.me = action.payload;
        draft.actions.register.errors = null;
        draft.actions.register.loading = false;
        break;
      case actionTypes.REGISTER_FAILURE:
        console.log('actionTypes.REGISTER_FAILURE')
        draft.actions.register.errors = action.payload.errors;
        draft.actions.register.loading = false;
        break;

      case actionTypes.LOG_OUT_ENDUSER:
        console.log('actionTypes.LOG_OUT_ENDUSER')
        draft.actions.logout.errors = null;
        draft.actions.logout.loading = true;
        break;
      case actionTypes.LOG_OUT_ENDUSER_SUCCESS:
        console.log('actionTypes.LOG_OUT_ENDUSER_SUCCESS')
        draft.actions.logout.errors = null;
        draft.actions.logout.loading = false;
        break;
      case actionTypes.LOG_OUT_ENDUSER_FAILURE:
        console.log('actionTypes.LOG_OUT_ENDUSER_FAILURE')
        draft.actions.logout.errors = action.payload.errors;
        draft.actions.logout.loading = false;
        break;

      case actionTypes.GET_PROJECTS_OWN:
        console.log('actionTypes.GET_PROJECTS_OWN')
        draft.actions.getProjectsOwn.errors = null;
        draft.actions.getProjectsOwn.loading = true;
        break;
      case actionTypes.GET_PROJECTS_OWN_SUCCESS:
        console.log('actionTypes.GET_PROJECTS_OWN_SUCCESS')
        draft.projectsOwn = action.payload;
        draft.actions.getProjectsOwn.errors = null;
        draft.actions.getProjectsOwn.loading = false;
        break;
      case actionTypes.GET_PROJECTS_OWN_FAILURE:
        console.log('actionTypes.GET_PROJECTS_OWN_FAILURE')
        draft.actions.getProjectsOwn.errors = action.payload.errors;
        draft.actions.getProjectsOwn.loading = false;
        break;

      case actionTypes.GET_PROJECTS:
        console.log('actionTypes.GET_PROJECTS')
        draft.actions.getProjects.errors = null;
        draft.actions.getProjects.loading = true;
        break;
      case actionTypes.GET_PROJECTS_SUCCESS:
        console.log('actionTypes.GET_PROJECTS_SUCCESS')
        draft.projects = action.payload;
        draft.actions.getProjects.errors = null;
        draft.actions.getProjects.loading = false;
        break;
      case actionTypes.GET_PROJECTS_FAILURE:
        console.log('actionTypes.GET_PROJECTS_FAILURE')
        draft.actions.getProjects.errors = action.payload.errors;
        draft.actions.getProjects.loading = false;
        break;

      case actionTypes.GET_PROJECT:
        console.log('actionTypes.GET_PROJECT')
        draft.actions.getProject.errors = null;
        draft.actions.getProject.loading = true;
        break;
      case actionTypes.GET_PROJECT_SUCCESS:
        console.log('actionTypes.GET_PROJECT_SUCCESS')
        draft.currentProject = action.payload;
        draft.actions.getProject.errors = null;
        draft.actions.getProject.loading = false;
        break;
      case actionTypes.GET_PROJECT_FAILURE:
        console.log('actionTypes.GET_PROJECT_FAILURE')
        draft.actions.getProject.errors = action.payload.errors;
        draft.actions.getProject.loading = false;
        break;

      case actionTypes.POST_PROJECT:
        console.log('actionTypes.POST_PROJECT')
        draft.actions.postProject.errors = null;
        draft.actions.postProject.loading = true;
        break;
      case actionTypes.POST_PROJECT_SUCCESS:
        console.log('actionTypes.POST_PROJECT_SUCCESS')
        draft.currentProject = action.payload;
        draft.actions.postProject.errors = null;
        draft.actions.postProject.loading = false;
        break;
      case actionTypes.POST_PROJECT_FAILURE:
        console.log('actionTypes.POST_PROJECT_FAILURE')
        draft.actions.postProject.errors = action.payload.errors;
        draft.actions.postProject.loading = false;
        break;

      case actionTypes.PUT_PROJECT:
        console.log('actionTypes.PUT_PROJECT')
        draft.actions.putProject.errors = null;
        draft.actions.putProject.loading = true;
        break;
      case actionTypes.PUT_PROJECT_SUCCESS:
        console.log('actionTypes.PUT_PROJECT_SUCCESS')
        draft.currentProject = action.payload;
        draft.actions.putProject.errors = null;
        draft.actions.putProject.loading = false;
        break;
      case actionTypes.PUT_PROJECT_FAILURE:
        console.log('actionTypes.PUT_PROJECT_FAILURE')
        draft.actions.putProject.errors = action.payload.errors;
        draft.actions.putProject.loading = false;
        break;

      case actionTypes.POST_TASK:
        console.log('actionTypes.POST_TASK')
        draft.actions.postTask.errors = null;
        draft.actions.postTask.loading = true;
        break;
      case actionTypes.POST_TASK_SUCCESS:
        console.log('actionTypes.POST_TASK_SUCCESS')
        draft.actions.postTask.errors = null;
        draft.actions.postTask.loading = false;
        break;
      case actionTypes.POST_TASK_FAILURE:
        console.log('actionTypes.POST_TASK_FAILURE')
        draft.actions.postTask.errors = action.payload.errors;
        draft.actions.postTask.loading = false;
        break;

      case actionTypes.PUT_TASK:
        console.log('actionTypes.PUT_TASK')
        draft.actions.putTask.errors = null;
        draft.actions.putTask.loading = true;
        break;
      case actionTypes.PUT_TASK_SUCCESS:
        console.log('actionTypes.PUT_TASK_SUCCESS')
        draft.actions.putTask.errors = null;
        draft.actions.putTask.loading = false;
        break;
      case actionTypes.PUT_TASK_FAILURE:
        console.log('actionTypes.PUT_TASK_FAILURE')
        draft.actions.putTask.errors = action.payload.errors;
        draft.actions.putTask.loading = false;
        break;

      case actionTypes.PUT_TASK_ASSIGN:
        console.log('actionTypes.PUT_TASK_ASSIGN')
        draft.actions.putTaskAssign.errors = null;
        draft.actions.putTaskAssign.loading = true;
        break;
      case actionTypes.PUT_TASK_ASSIGN_SUCCESS:
        console.log('actionTypes.PUT_TASK_ASSIGN_SUCCESS')
        draft.actions.putTaskAssign.errors = null;
        draft.actions.putTaskAssign.loading = false;
        break;
      case actionTypes.PUT_TASK_ASSIGN_FAILURE:
        console.log('actionTypes.PUT_TASK_ASSIGN_FAILURE')
        draft.actions.putTaskAssign.errors = action.payload.errors;
        draft.actions.putTaskAssign.loading = false;
        break;

      case actionTypes.PUT_TASK_STATE:
        console.log('actionTypes.PUT_TASK_STATE')
        draft.actions.putTaskState.errors = null;
        draft.actions.putTaskState.loading = true;
        break;
      case actionTypes.PUT_TASK_STATE_SUCCESS:
        console.log('actionTypes.PUT_TASK_STATE_SUCCESS')
        draft.actions.putTaskState.errors = null;
        draft.actions.putTaskState.loading = false;
        break;
      case actionTypes.PUT_TASK_STATE_FAILURE:
        console.log('actionTypes.PUT_TASK_STATE_FAILURE')
        draft.actions.putTaskState.errors = action.payload.errors;
        draft.actions.putTaskState.loading = false;
        break;

      case actionTypes.POST_SUBTASK:
        console.log('actionTypes.POST_SUBTASK')
        draft.actions.postSubtask.errors = null;
        draft.actions.postSubtask.loading = true;
        break;
      case actionTypes.POST_SUBTASK_SUCCESS:
        console.log('actionTypes.POST_SUBTASK_SUCCESS')
        draft.actions.postSubtask.errors = null;
        draft.actions.postSubtask.loading = false;
        break;
      case actionTypes.POST_SUBTASK_FAILURE:
        console.log('actionTypes.POST_SUBTASK_FAILURE')
        draft.actions.postSubtask.errors = action.payload.errors;
        draft.actions.postSubtask.loading = false;
        break;

      case actionTypes.PUT_SUBTASK:
        console.log('actionTypes.PUT_SUBTASK')
        draft.actions.putSubtask.errors = null;
        draft.actions.putSubtask.loading = true;
        break;
      case actionTypes.PUT_SUBTASK_SUCCESS:
        console.log('actionTypes.PUT_SUBTASK_SUCCESS')
        draft.actions.putSubtask.errors = null;
        draft.actions.putSubtask.loading = false;
        break;
      case actionTypes.PUT_SUBTASK_FAILURE:
        console.log('actionTypes.PUT_SUBTASK_FAILURE')
        draft.actions.putSubtask.errors = action.payload.errors;
        draft.actions.putSubtask.loading = false;
        break;

      case actionTypes.PUT_SUBTASK_STATE:
        console.log('actionTypes.PUT_SUBTASK_STATE')
        draft.actions.putSubtaskState.errors = null;
        draft.actions.putSubtaskState.loading = true;
        break;
      case actionTypes.PUT_SUBTASK_STATE_SUCCESS:
        console.log('actionTypes.PUT_SUBTASK_STATE_SUCCESS')
        draft.actions.putSubtaskState.errors = null;
        draft.actions.putSubtaskState.loading = false;
        break;
      case actionTypes.PUT_SUBTASK_STATE_FAILURE:
        console.log('actionTypes.PUT_SUBTASK_STATE_FAILURE')
        draft.actions.putSubtaskState.errors = action.payload.errors;
        draft.actions.putSubtaskState.loading = false;
        break;

      case actionTypes.GET_DOCUMENTS:
        console.log('actionTypes.GET_DOCUMENTS')
        draft.actions.getDocuments.errors = null;
        draft.actions.getDocuments.loading = true;
        break;
      case actionTypes.GET_DOCUMENTS_SUCCESS:
        console.log('actionTypes.GET_DOCUMENTS_SUCCESS')
        draft.documents = action.payload;
        draft.actions.getDocuments.errors = null;
        draft.actions.getDocuments.loading = false;
        break;
      case actionTypes.GET_DOCUMENTS_FAILURE:
        console.log('actionTypes.GET_DOCUMENTS_FAILURE')
        draft.actions.getDocuments.errors = action.payload.errors;
        draft.actions.getDocuments.loading = false;
        break;

      case actionTypes.POST_DOCUMENT:
        console.log('actionTypes.POST_DOCUMENT')
        draft.actions.postDocument.errors = null;
        draft.actions.postDocument.loading = true;
        break;
      case actionTypes.POST_DOCUMENT_SUCCESS:
        console.log('actionTypes.POST_DOCUMENT_SUCCESS')
        draft.actions.postDocument.errors = null;
        draft.actions.postDocument.loading = false;
        break;
      case actionTypes.POST_DOCUMENT_FAILURE:
        console.log('actionTypes.POST_DOCUMENT_FAILURE')
        draft.actions.postDocument.errors = action.payload.errors;
        draft.actions.postDocument.loading = false;
        break;

      /* UI actions */

      default:
        break;
    }
  },
  RootReducerInitialState,
);
