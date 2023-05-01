/**
 * @description - config that should always be present in environment variables where application is run
 */
export class EnvConfig {
  node_env: 'local' | 'test' | 'development' | 'staging' | 'production';

  port: number;

  db_port: number;
  db_password: string;
  db_username: string;
  db_name: string;
  db_host: string;

  front_host: string;
  backend_host: string;
}
