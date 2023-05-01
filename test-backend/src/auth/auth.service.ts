import {
  BadRequestException,
  HttpException,
  Injectable,
  InternalServerErrorException,
  NotFoundException,
  UnauthorizedException,
} from '@nestjs/common';
import User from 'src/models/user.model';
import { InjectModel } from '@nestjs/sequelize';
import { ApiConfigService } from 'src/api-config/api-config.service';

@Injectable()
export class AuthService {
  constructor(
    @InjectModel(User) private userModel: typeof User,
    private configService: ApiConfigService,
  ) {}

  /**
   * @description - Validates User Auth token
   *
   * @returns
   */
  public async userAuthentication(userAuthToken?: string): Promise<User> {
    try {
      if (!userAuthToken) {
        throw new UnauthorizedException('User auth token is not provided');
      }

      const user = await this.userModel.findOne({
        where: {
          email: userAuthToken,
        },
        raw: true,
      });

      if (!user) {
        throw new NotFoundException(
          `User with this email is not found ${userAuthToken}`,
        );
      }

      return user;
    } catch (error) {
      if (
        error.code === 'auth/id-token-expired' ||
        (error.code === 'auth/argument-error' &&
          error.errorInfo.message.includes(
            'Most likely the ID token is expired',
          ))
      ) {
        throw new UnauthorizedException(
          'This user auth token is invalid for some reason, most likely expired',
        );
      } else if (error instanceof HttpException) {
        throw error;
      }

      throw new InternalServerErrorException(
        'Unexpected error with authentication has happened',
      );
    }
  }
}
