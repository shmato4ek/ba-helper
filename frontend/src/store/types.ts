/**
 * Data types that are result of frontend quires
 * @description Stored in redux. Serve as single source of truth about data
 * on frontend. Are different from view data types, that are transformed version
 * of these data types, user for showing data
 */
export interface Me extends UserDto {

}

export enum ProjectDtoFields {
  id = 'id',
  projectName = 'projectName',
  deadline = 'deadline',
  hours = 'hours',
  taskCount = 'taskCount',
  author = 'author',
}


/**
 * @description - Project
 */
export interface ProjectDto {
  id: number;
  deadline: string;
  // authorid: number; // Hide since it's not needed
  author: UserDto;
  description: string; // Post MVP
  projectName: string;
  hours: number;
  isDeleted: boolean;
  tasks: TaskDto[];
  users: UserDto[];
}
export interface PostProjectDto {
  deadline: Date;
  projectName: string;
  description: string;
  users: string[]; // list of emails
}

export interface PutProjectDto extends Pick<PostProjectDto, 'deadline' | 'projectName' | 'description' | 'users'> {
  id: number;
}


/**
 * @description - Task
 */
export interface TaskDto {
  id: number;
  deadline: string;
  projectId: number;
  taskName: string;
  hours: number;
  taskState: TaskState;
  users: UserDto[];
  subtasks: SubtaskDto[];
}

export interface PostTaskDto {
  deadline: Date;
  projectId: number;
  taskName: string;
  hours: number;
}

export interface PutTaskDto extends Pick<PostTaskDto, 'deadline' | 'taskName' | 'hours'> {
  id: number;
}

export interface PutTaskAssignDto {
  taskId: number;
  email: string;
}

export interface PutTaskStateDto {
  taskId: number;
  taskState: TaskState;
}

/**
 * @description - SubTask
 */
export interface SubtaskDto {
  id: number;
  taskId: number;
  name: string;
  // deadline: Date;
  taskState: TaskState;
}

export interface PostSubtaskDto {
  taskId: number;
  name: string;
  // taskState: TaskState; post-mvp
}

export interface PutSubtaskDto extends Pick<PostSubtaskDto, 'name'> {
  id: number;
}

export interface PutSubtaskStateDto {
  subtaskId: number;
  taskState: TaskState;
}


/**
 * @description - Document
 */

/**
 * @description - User
 */
export interface UserDto {
  id: number;
  name: string;
  email: string;
  projects: ProjectDto[];
  tasks: TaskDto[];
  documents: Document[];
}

export enum TaskState {
  Pending,
  InProgress,
  Done,
  Approve,
};

export const taskStateToText = (taskState: TaskState) => {
  switch (taskState) {
    case TaskState.Approve: return 'Затверджено';
    case TaskState.Done: return 'Виконано';
    case TaskState.InProgress: return 'В процесі';
    case TaskState.Pending: return 'В очікуванні';
  
    default:
      return taskState;
  }
}

export interface RACIMatrixDto {
  executors: string[];
  tasks: string[];
  RACI: RACIStatus[][];
}

export enum RACIStatus {
  Responsible,
  Accountable,
  Consulted,
  Informed,
}


export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  email: string;
  fullName: string;
  password: string;
  confirmPassword: string;
}

export enum ServiceRoutes {
  OWNED_PROJECTS = '/owned-projects',
  MY_PROJECTS = '/my-projects',
}
