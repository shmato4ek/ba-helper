import {
  Controller,
  Get,
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
  getHello(): string {
    return this.appService.getHello();
  }
}
