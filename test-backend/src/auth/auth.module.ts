import { Module } from '@nestjs/common';
import { AuthService } from './auth.service';
import User from 'src/models/user.model';
import { SequelizeModule } from '@nestjs/sequelize';

@Module({
  imports: [SequelizeModule.forFeature([User])],
  providers: [AuthService],
  exports: [AuthService],
  controllers: [],
})
export class AuthModule {}
