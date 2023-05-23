/**
 * Data types that are result of frontend quires
 * @description Stored in redux. Serve as single source of truth about data
 * on frontend. Are different from view data types, that are transformed version
 * of these data types, user for showing data
 */
export interface Me extends UserDto {}

export enum ProjectDtoFields {
  id = "id",
  projectName = "projectName",
  deadline = "deadline",
  hours = "hours",
  taskCount = "taskCount",
  authorName = "authorName",
}

/**
 * @description - Project
 */
export interface ProjectDto {
  id: number;
  deadline: string;
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
  deadline: string;
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

/**
 * @description
 *    - temporary object to store UI edit state before sending well-formated PUT request
 */
export interface EditPostProjectDto
  extends Pick<PostProjectDto, "deadline" | "projectName" | "description"> {
  users: string;
}

export interface PutProjectDto
  extends Pick<
    PostProjectDto,
    "deadline" | "projectName" | "description" | "users"
  > {
  id: number;
}

/**
 * @description
 *    - temporary object to store UI edit state before sending well-formated PUT request
 */
export interface EditPutProjectDto
  extends Pick<PutProjectDto, "deadline" | "projectName" | "description"> {
  users: string;
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
  tags: TaskTopic[];
  hours: number;
}

export interface EditPostTaskDto
  extends Pick<PostTaskDto, "deadline" | "taskName" | "hours" > {
    tag: TaskTopic;
}

export interface PutTaskDto
  extends Pick<PostTaskDto, "deadline" | "taskName" | "hours"> {
  id: number;
}

export interface EditPutTaskDto
  extends Pick<PutTaskDto, "deadline" | "taskName" | "hours"> {
  taskState: TaskState;
  assignedUser: string;
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

export interface PutSubtaskDto extends Pick<PostSubtaskDto, "name"> {
  id: number;
}

export interface EditSubtaskDto extends Pick<PostSubtaskDto, "name"> {}

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

export interface PutUserDto {
  name: string;
  email: string;
  oldPassword: string;
  password: string;
}

export interface EditPutUserDto
  extends Pick<PutUserDto, "name" | "email" | "password" | "oldPassword"> {
  passwordConfirm: string;
}

export interface StatisticDataInfo {
  taskTopic: number;
  taskQuality: number;
  taskCount: number;
}

export enum TaskState {
  Pending,
  InProgress,
  Done,
  Approve,
}

export const taskStates: TaskState[] = [0, 1, 2, 3];

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

export interface PutGlossary extends Pick<Glossary, "term" | "definition"> {}

export interface UserStory {
  id: number;
  documentId: number;
  name: string;
  userStoryFormulas: USFormula[];
  acceptanceCriterias: USAcceptanceCriteria[];
}

export interface PutUserStory {
  name: string;
  userStoryFormulas: PutUSFormula[];
  acceptanceCriterias: PutUSAcceptanceCriteria[];
}

export const getInitPutUserStory = () => ({
  name: "",
  acceptanceCriterias: [{ text: "" }],
  userStoryFormulas: [{ text: "" }],
})

export interface USFormula {
  id: number;
  userStoryId: number;
  text: string;
}

export const getInitUSFormula = () => ({ text: "" })

export interface PutUSFormula extends Pick<USFormula, "text"> {}

export interface EditPutUSFormula {
  as: string;
  iWantTo: string;
  soThat: string;
}

export interface USAcceptanceCriteria {
  id: number;
  userStoryId: number;
  text: string;
}

export const getInitUSAcceptanceCriteria = () => ({ text: "" })

export interface PutUSAcceptanceCriteria
  extends Pick<USAcceptanceCriteria, "text"> {}

export interface EditPutUSAcceptanceCriteria {
  given: string;
  when: string;
  then: string;
}

export interface PostDocumentDto
  extends Pick<DocumentDto, "name" | "projectAim"> {
  glossaries: Pick<Glossary, "term" | "definition">[];
  userStories: {
    name: string;
    userStoryFormulas: Pick<USFormula, "text">[];
    acceptanceCriterias: Pick<USAcceptanceCriteria, "text">[];
  }[];
}

export const taskStateToText = (taskState: TaskState) => {
  switch (taskState) {
    case TaskState.Approve:
      return "Затверджено";
    case TaskState.Done:
      return "Виконано";
    case TaskState.InProgress:
      return "В процесі";
    case TaskState.Pending:
      return "В очікуванні";

    default:
      return taskState;
  }
};

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

export const taskTopics: TaskTopic[] = [1,2,3,4,5,6,7,8]

export const taskTopicToText = (taskState: TaskTopic) => {
  switch (taskState) {
    case TaskTopic.Design:
      return "Дизайн";
    case TaskTopic.Frontend:
      return "Фронтенд";
    case TaskTopic.Backend:
      return "Бекенд";
    case TaskTopic.Testing:
      return "Тестування";
    case TaskTopic.Deploy:
      return "Деплой";
    case TaskTopic.ProcessDesing:
      return "Проектування процесу";
    case TaskTopic.Documentation:
      return "Створення документації";
    case TaskTopic.PlanningAndAnalysis:
      return "Планування та аналіз";

    default:
      return taskState;
  }
};

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
  name: string;
  password: string;
}

export interface EditRegisterDto {
  email: string;
  name: string;
  password: string;
  confirmPassword: string;
}

export enum ServiceRoutes {
  OWNED_PROJECTS = "/owned-projects",
  MY_PROJECTS = "/my-projects",
}

export type CreateErrorObject<T extends { [key: string]: any }> = {
  [actionName in keyof T]?: string | string[];
};
