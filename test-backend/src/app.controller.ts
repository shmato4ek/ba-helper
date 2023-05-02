import {
  Controller,
  Get,
  InternalServerErrorException,
  Param,
  Post,
  Request,
  UnauthorizedException,
} from '@nestjs/common';
import { Request as ERequest } from 'express';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  private readonly meDto = {
    id: 1,
    email: 'ruslan@gmail.com',
    name: 'string',
  };

  @Post('/auth/register')
  register() {
    return {
      user: this.meDto,
      token: { accessToken: this.meDto.email },
    };
  }

  @Post('/auth/login')
  login() {
    return {
      user: this.meDto,
      token: { accessToken: this.meDto.email },
    };
  }

  @Get('/auth/me')
  async me(@Request() request: ERequest) {
    if (!request.headers['x-auth-token']) {
      return this.meDto;
    }

    throw new UnauthorizedException('Not authorized');
  }

  @Get('/project/user')
  getProjectUser(): ProjectDto[] {
    return [AlphaProject, BetaProject];
  }

  @Get('/project/user/own')
  getProjectUserOwn(): ProjectDto[] {
    return [AlphaProject, BetaProject];
  }

  @Get('/project/:id')
  getProjectById(@Param('id') id: string): ProjectDto {
    console.log('/project/:id');
    console.log(id);

    const foundProject = [AlphaProject, BetaProject].find(
      (x) => x.id === Number(id),
    );

    if (!foundProject) {
      throw new InternalServerErrorException('Project not found');
    }

    return foundProject;
  }
}

export enum TaskState {
  Pending,
  InProgress,
  Done,
  Approve,
}

export const AlphaUser: UserDto = {
  id: 1,
  email: 'alpha@gmail.com',
  name: 'Б. Джонсонюк',
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

export const AlphaTask1: TaskDto = {
  id: 1,
  deadline: new Date(),
  hours: 15,
  projectId: 1,
  taskName: 'Завдання 1',
  taskState: TaskState.InProgress,
  users: [AlphaUser],
  subtasks: [
    {
      id: 1,
      name: 'Підзавдання 1',
      taskId: 1,
      taskState: TaskState.Done,
    },
    {
      id: 2,
      name: 'Підзавдання 2',
      taskId: 1,
      taskState: TaskState.Pending,
    },
  ],
};

export const AlphaTask2: TaskDto = {
  id: 2,
  deadline: new Date(),
  hours: 15,
  projectId: 1,
  taskName: 'Завдання 2',
  taskState: TaskState.InProgress,
  users: [AlphaUser],
  subtasks: [],
};

export const AlphaProject: ProjectDto = {
  id: 1,
  projectName: 'Проект 1',
  description: 'Тестовий опис проекту 1',
  author: AlphaUser,
  hours: 30,
  isDeleted: false,
  tasks: [AlphaTask1, AlphaTask2],
  users: [AlphaUser, BetaUser],
  deadline: new Date(),
};

export const BetaProject: ProjectDto = {
  id: 2,
  projectName: 'Проект 2',
  description: 'Тестовий опис проекту 2',
  author: BetaUser,
  hours: 11,
  isDeleted: false,
  tasks: [],
  users: [],
  deadline: new Date(),
};

/**
 * @description - Project
 */
export interface ProjectDto {
  id: number;
  deadline: Date;
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

export interface PutProjectDto
  extends Pick<
    PostProjectDto,
    'deadline' | 'projectName' | 'description' | 'users'
  > {
  id: number;
}

/**
 * @description - Task
 */
export interface TaskDto {
  id: number;
  deadline: Date;
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

export interface PutTaskDto
  extends Pick<PostTaskDto, 'deadline' | 'taskName' | 'hours'> {
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
  // deadline: Date; removed
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
