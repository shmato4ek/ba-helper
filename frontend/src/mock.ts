import { ProjectDto, UserDto } from "./store/types";

export const AlphaUser: UserDto = {
  id: 1,
  email: 'alpha@gmail.com',
  name: 'Alpha',
  documents: [],
  projects: [],
  tasks: [],
};

export const BetaUser: UserDto = {
  id: 1,
  email: 'beta@gmail.com',
  name: 'Beta',
  documents: [],
  projects: [],
  tasks: [],
};

export const AlphaProject: ProjectDto  = {
  id: 1,
  projectName: 'Test Project',
  description: 'Alpha pr description',
  authorId: 1,
  authorName: AlphaUser.name,
  hours: 30,
  isDeleted: false,
  canEdit: true,
  tasks: [],
  users: [],
  deadline: '2023-05-02T05:34:45.151Z',
};

export const BetaProject: ProjectDto = {
  id: 2,
  projectName: 'Beta Project',
  description: 'Beta pr description',
  authorId: 2,
  authorName: BetaUser.name,
  hours: 32,
  isDeleted: false,
  canEdit: false,
  tasks: [],
  users: [],
  deadline: '2023-05-02T05:34:45.151Z',
};
