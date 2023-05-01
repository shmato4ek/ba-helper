/**
 * Data types that are result of frontend quires
 * @description Stored in redux. Serve as single source of truth about data
 * on frontend. Are different from view data types, that are transformed version
 * of these data types, user for showing data
 */
export interface Myself extends UserDto {

}

export interface ProviderForEnduser {
  id: string;

  name: string;
  address: string;
  email: string;
  phone_number: string;
  website: string;

  description: string;
}

export enum NavigationTabOption {
  DASHBOARD = 'dashboard',
  SETTINGS = 'settings',
  SERVICES = 'services',
  BLOG = 'blog',
  ABOUT_US = 'about-us',
  CONTACT_US = 'contact-us',
  LOG_OUT = 'log-out',
}



export interface BisMetriscDto {
  balance: { balance: number; period: number}[]
}

export enum ProjectDtoFields {
  id = 'id',
  projectName = 'projectName',
  deadline = 'deadline',
  hours = 'hours',
  approver = 'approver',
  status = 'status',
}

export interface ProjectDto {
  id: number;
  projectName: string;
  deadline: Date;
  hours?: number;
  approver?: string;
  status?: string;
  tasks?: TaskDto[];
  users?: UserDto[];
}

export interface TaskDto {

}

export interface UserDto {
  
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
