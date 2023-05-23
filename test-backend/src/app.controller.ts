import { Body, Controller, Get, InternalServerErrorException, Param, Post, Put, Request, UnauthorizedException } from '@nestjs/common';
import { Request as ERequest } from 'express';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  private projects = [AlphaProject, BetaProject];

  private readonly meDto = {
    id: 1,
    email: 'ruslan@gmail.com',
    name: 'Ruslan Plastun',
  };

  @Post('/register')
  register() {
    return {
      user: this.meDto,
      token: { accessToken: this.meDto.email },
    };
  }

  @Post('/login')
  login() {
    return {
      user: this.meDto,
      token: { accessToken: this.meDto.email },
    };
  }

  @Get('/auth/me')
  async me(@Request() request: ERequest) {
    if (request.headers['x-auth-token']) {
      return this.meDto;
    }

    throw new UnauthorizedException('Not authorized');
  }

  @Get('/document/user')
  getDocumentsUser(): DocumentDto[] {
    return Documents;
  }

  @Get('/project/user')
  getProjectUser(): ProjectDto[] {
    return [AlphaProject, BetaProject];
  }

  @Get('/project/user/own')
  getProjectUserOwn(): ProjectDto[] {
    return [AlphaProject, BetaProject];
  }

  @Post('/project')
  postProject(@Body() params: PostProjectDto): ProjectDto {
    console.log('POST /project');
    console.log(params);

    const newProject: ProjectDto = {
      id: 3,
      projectName: params.projectName,
      description: params.description,
      authorName: 'BetaUser',
      hours: 0,
      isDeleted: false,
      canEdit: true,
      tasks: [],
      users: [],
      deadline: params.deadline,
    };

    this.projects.push(newProject);

    return newProject;
  }

  @Put('/project')
  putProjectById(@Body() params: PutProjectDto): ProjectDto {
    console.log('PUT /project');
    console.log(params);

    AlphaProject.deadline = params.deadline;
    AlphaProject.projectName = params.projectName;
    AlphaProject.description = params.description;
    AlphaProject.users = AllUsers.filter((x) => params.users.includes(x.email));

    return AlphaProject;
  }

  @Get('/user/statistics/me')
  getMeStatistics(@Param('id') id: string): StatisticDataInfo[] {
    console.log('/profile/statistics/me');
    console.log(id);

    return meStatistics;
  }

  @Get('/project/statistics/:id')
  getProjectStatisticsById(@Param('id') id: string): ClusterInfo[] {
    console.log('/project/:id');
    console.log(id);

    return clusters;
  }

  @Get('/project/:id')
  getProjectById(@Param('id') id: string): ProjectDto {
    console.log('/project/:id');
    console.log(id);

    const foundProject = [AlphaProject, BetaProject].find((x) => x.id === Number(id));

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

export const AllUsers = [AlphaUser, BetaUser];

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
  authorName: 'AlphaUser',
  hours: 30,
  isDeleted: false,
  canEdit: true,
  tasks: [AlphaTask1, AlphaTask2],
  users: [AlphaUser, BetaUser],
  deadline: new Date(),
};

export const BetaProject: ProjectDto = {
  id: 2,
  projectName: 'Проект 2',
  description: 'Тестовий опис проекту 2',
  authorName: 'BetaUser',
  hours: 11,
  isDeleted: false,
  canEdit: false,
  tasks: [],
  users: [],
  deadline: new Date(),
};

export const Documents: DocumentDto[] = [
  {
    id: 1,
    isDeleted: false,
    name: 'Document 1',
    projectAim: 'Projetc Aim 1',
    userId: 1,
    glossaries: [],
    userStories: [],
  },
];

export const clusters: ClusterInfo[] = [
  {
    data: [
      {
        id: 1,
        clusterId: 1,
        quality: 0.412323,
        topic: 3,
      },
      {
        id: 1,
        clusterId: 2,
        quality: 0.67,
        topic: 3,
      },
    ],
    projectName: 'Project 1',
    users: [AlphaUser, BetaUser],
  },
  {
    data: [
      {
        id: 2,
        clusterId: 2,
        quality: 0.45,
        topic: 3,
      },
    ],
    projectName: 'Project 2',
    users: [BetaUser],
  },
  {
    data: [
      {
        id: 3,
        clusterId: 3,
        quality: 0.45,
        topic: 3,
      },
    ],
    projectName: 'Project 3',
    users: [BetaUser],
  },
];

export enum TaskTopic {
  Design = 1,
  Frontend = 2,
  Backend = 3,
  Testing = 4,
  Deploy = 5,
  ProcessDesing = 6,
  Documentation = 7,
  PlanningAndAnalysis = 8,
}

export const meStatistics: StatisticDataInfo[] = [
  {
    taskCount: 3,
    taskQuality: 0.34,
    taskTopic: TaskTopic.Frontend,
  },
];

/**
 * @description - Project
 */
export interface ProjectDto {
  id: number;
  deadline: Date;
  // authorid: number; // Hide since it's not needed
  authorName: string;
  description: string; // Post MVP
  projectName: string;
  hours: number;
  isDeleted: boolean;
  canEdit: boolean;
  tasks: TaskDto[];
  users: UserDto[];
}
export interface PostProjectDto {
  deadline: Date;
  projectName: string;
  description: string;
  users: string[]; // list of emails
}

export interface ClusterInfo {
  projectName: string;
  users: UserDto[];
  data: ClusterData[];
}

export interface ClusterData {
  id: number;
  clusterId: number;
  topic: number;
  quality: number;
}

export interface PutProjectDto extends Pick<PostProjectDto, 'deadline' | 'projectName' | 'description' | 'users'> {
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

export interface DocumentDto {
  id: number;
  userId: number;
  name: string;
  projectAim: string;
  isDeleted: boolean;
  glossaries: Glossary[];
  userStories: UserStory[];
}

export interface Glossary {
  id: number;
  documentId: number;
  term: string;
  definition: string;
}

export interface UserStory {
  id: number;
  documentId: number;
  name: string;
  userStoryFormulas: USFormula[];
  acceptanceCriterias: USAcceptanceCriteria[];
}

export interface USFormula {
  id: number;
  userStoryId: number;
  text: string;
}

export interface USAcceptanceCriteria {
  id: number;
  userStoryId: number;
  text: string;
}

export interface PostDoucmentDto extends Pick<DocumentDto, 'name' | 'projectAim'> {
  glossaries: Pick<Glossary, 'term' | 'definition'>;
  userStories: {
    name: string;
    userStoryFormulas: Pick<USFormula, 'text'>[];
    acceptanceCriterias: Pick<USAcceptanceCriteria, 'text'>[];
  }[];
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

export interface StatisticDataInfo {
  taskTopic: TaskTopic;
  taskQuality: number;
  taskCount: number;
}

export enum RACIStatus {
  Responsible,
  Accountable,
  Consulted,
  Informed,
}
