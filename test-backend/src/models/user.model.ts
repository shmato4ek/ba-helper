import {
  Column,
  CreatedAt,
  Default,
  IsEmail,
  IsUUID,
  Model,
  PrimaryKey,
  Table,
  Unique,
  UpdatedAt,
  Scopes,
  DefaultScope,
} from 'sequelize-typescript';
import { Includeable, Op } from 'sequelize';
import { v4 as uuidv4 } from 'uuid';

export enum UserScope {
  WithDepartment = 'WithDepartment',
  WithAll = 'WithAll',
}

export interface UserWithAllFilters {
  id?: string | string[];
  departmentId?: string[];
  country?: string[] | string;
  legalLocation?: string[] | string;
  email?: string[] | string;
  active?: boolean;

  includeDepartment?: boolean;
}

@DefaultScope(() => ({
  attributes: {
    exclude: ['refreshToken'],
  },
}))
@Scopes(() => ({
  [UserScope.WithAll]: ({
    id,
    active,
    departmentId,
    country,
    legalLocation,
    email,
  }: UserWithAllFilters = {}) => {
    const includes: Includeable[] = [];

    return {
      where: {
        ...(active !== undefined && {
          deactivatedAt: active ? null : { [Op.not]: null },
        }),
        ...(id && { id }),
        ...(departmentId && { departmentId }),
        ...(country && { country }),
        ...(legalLocation && { legalLocation }),
        ...(email && { email }),
      },
      include: includes,
      attributes: {
        exclude: ['refreshToken'],
      },
    };
  },
}))
@Table({
  modelName: 'User',
  timestamps: true,
  tableName: 'User',
})
export default class User extends Model<User, Partial<User>> {
  @IsUUID(4)
  @PrimaryKey
  @Default(uuidv4)
  @Column
  id: string;

  @Unique
  @IsEmail
  @Column({ allowNull: false })
  email: string;

  @Column({ allowNull: false })
  name: string;

  @CreatedAt
  createdAt: Date;

  @UpdatedAt
  updatedAt: Date;
}
