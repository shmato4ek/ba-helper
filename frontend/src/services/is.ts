export const is = {
  localhost: window.location.hostname.includes('localhost')
};

export const globals = {
  localhost: 'localhost',
  endpoint: (() => {
    if (is.localhost) {
      return 'http://localhost:5054';
    }

    return 'http://localhost:5054';
  })(),
  ports: {
    be: 5054,
    fe: 3000
  },
  paths: {
    auth: {
      register: '/api/auth/register',
      login: '/api/auth/login',
      me: '/api/auth/me',
    },
    document: {
      document: '/api/document',
    },
    project: {
      user: '/api/project/user',
    },
  },
  fePaths: {
    services: 'services',
    myProjects: 'my-projects',
    ownedProjects: 'owned-projects',
  },
  restrictedFePaths: [] as string[]
};

globals.restrictedFePaths = [
  globals.fePaths.myProjects,
  globals.fePaths.ownedProjects,
  globals.fePaths.services,
]