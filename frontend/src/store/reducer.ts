import produce from 'immer';
import { ErrorCodes } from '../error';
import { actionTypes, AppAction } from './actions';
import { BisMetriscDto, Myself } from './types';
import { BisFunctionDto } from './bis-function.types';

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
    getMyself: {
      loading: false as boolean,
      errors: null as ErrorCodes[] | null,

      /**
       * Is modified by the following actions
       *  - postEnduserSuccess
       *  - loginEnduserSuccess
       *  - getMyselfSuccess
       *  - getMyselfFailure
       */
      success: null as boolean | null, // is user logged in
    },

    login: {
      loading: false as boolean,
      errors: null as ErrorCodes[] | null,
      success: null as boolean | null, // is there been at least one successful register
    },

    register: {
      loading: false as boolean,
      errors: null as ErrorCodes[] | null,
      success: null as boolean | null, // is there been at least one successful register
    },

    logout: {
      loading: false as boolean,
      errors: null as ErrorCodes[] | null,
    },
  },

  /**
   * Is modified by the following actions
   *  - getMyself
   */
  myself: null as Myself | null,

  bisFunctions: null as BisFunctionDto[] | null,
  bisMetrics: null as BisMetriscDto | null,
};

export type AppState = typeof RootReducerInitialState;

/* Reducer */
export const RootReducer = produce(
  (draft: AppState, action: AppAction) => {
    switch (action.type) {
      case actionTypes.GET_ME:
        draft.actions.getMyself.errors = null;
        draft.actions.getMyself.loading = true;
        break;
      case actionTypes.GET_ME_SUCCESS:
        draft.myself = action.payload.myself;
        draft.actions.getMyself.errors = null;
        draft.actions.getMyself.loading = false;
        draft.actions.getMyself.success = true;
        break;
      case actionTypes.GET_ME_FAILURE:
        draft.actions.getMyself.errors = action.payload.errors;
        draft.actions.getMyself.loading = false;
        draft.actions.getMyself.success = false;
        break;

      case actionTypes.LOGIN:
        draft.actions.login.errors = null;
        draft.actions.login.loading = true;
        break;
      case actionTypes.LOGIN_SUCCESS:
        draft.actions.login.errors = null;
        draft.actions.login.loading = false;
        break;
      case actionTypes.LOGIN_FAILURE:
        draft.actions.login.errors = action.payload.errors;
        draft.actions.login.loading = false;
        break;

      case actionTypes.REGISTER:
        draft.actions.register.errors = null;
        draft.actions.register.loading = true;
        break;
      case actionTypes.REGISTER_SUCCESS:
        draft.myself = action.payload;
        draft.actions.register.errors = null;
        draft.actions.register.loading = false;
        break;
      case actionTypes.REGISTER_FAILURE:
        draft.actions.register.errors = action.payload.errors;
        draft.actions.register.loading = false;
        break;

      case actionTypes.LOG_OUT_ENDUSER:
        draft.actions.logout.errors = null;
        draft.actions.logout.loading = true;
        break;
      case actionTypes.LOG_OUT_ENDUSER_SUCCESS:
        draft.actions.logout.errors = null;
        draft.actions.logout.loading = false;
        break;
      case actionTypes.LOG_OUT_ENDUSER_FAILURE:
        draft.actions.logout.errors = action.payload.errors;
        draft.actions.logout.loading = false;
        break;

      /* UI actions */

      default:
        break;
    }
  },
  RootReducerInitialState,
);
