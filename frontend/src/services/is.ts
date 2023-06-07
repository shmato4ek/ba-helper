export const is = {
  localhost: window.location.hostname.includes('localhost')
};

export const globals = {
  localhost: 'localhost',
  endpoint: (() => {
    //if (is.localhost) {
    //return "https://stupid-colts-thank.loca.lt";
    //}

    return 'http://localhost:5054';
  })(),
  ports: {
    be: 5054,
    fe: 3000
  },
  paths: {
    auth: {
      register: '/api/register',
      login: '/api/login',
      me: '/api/auth/me',
    },
    user: {
      _: '/api/user',
      statisticsMe: '/api/user/statistics/me',
    },
    document: {
      _: '/api/document',
      user: '/api/document/user',
    },
    project: {
      _: '/api/project',
      user: '/api/project/user',
      userOwn: '/api/project/user/own',
      userArchive: '/api/project/user/own/archive',
      stats: '/api/project/statistics',
      statsV2: '/api/project/statistics/dbscan',
      archive: '/api/project/archive',
      restore: '/api/project/restore'
    },
    task: {
      _: '/api/task',
      assign: '/api/task/assign',
      state: '/api/task/state',
      approve: '/api/task/approve',
      subtask: '/api/task/subtask',
      subtaskState: '/api/task/subtask/state',
      subtaskApprove: '/api/task/subtask/approve',
    },
    download: {
      _: '/api/download',
      plan: '/api/download/plan',
      raci: '/api/download/raci'
    }
  },
  fePaths: {
    services: 'services',
    myProjects: 'my-projects',
    ownedProjects: 'owned-projects',
    archivedProjects: 'archived-projects',
  },
  restrictedFePaths: [] as string[]
};

globals.restrictedFePaths = [
  globals.fePaths.myProjects,
  globals.fePaths.ownedProjects,
  globals.fePaths.services,
]