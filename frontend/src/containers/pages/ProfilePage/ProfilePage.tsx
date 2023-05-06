import React, { useCallback } from 'react';
import { useDispatch } from 'react-redux';

import * as yup from 'yup'
import { validateStraight } from '../../../yup';
import * as _ from 'lodash'
import Profile from '../../../components/Profile/Profile';
import { CreateErrorObject, EditPutUserDto, PutUserDto } from '../../../store/types';
import { DeleteUser, PutUser } from '../../../store/actions';
import { AppState } from '../../../store/reducer';
import { useSelector } from 'react-redux';
import { redirect, useNavigate } from 'react-router-dom';
import NotFound from '../../../components/NotFound/NotFound';

const ProfilePage = () => {
  const dispatch = useDispatch();
  const me = useSelector((state: AppState) => state.me);
  const navigate = useNavigate()

  const onValidate = useCallback((values: EditPutUserDto) => {
    console.log('Profile Page values validate');
    console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<EditPutUserDto> = {};

    formErrors.email = validateStraight(yup.string().email('Invalid Email').required("Обов'язково"), values.email);
    formErrors.password = validateStraight(yup.string().min(6, 'Пароль має мати як мінімум 6 символів').max(255, 'Пароль повинен бути меншим за 255 символів').required("Обов'язково"), values.password);
    formErrors.passwordConfirm = validateStraight(yup.string().min(6, 'Пароль має мати як мінімум 6 символів').max(255, 'Пароль повинен бути меншим за 255 символів').nullable(), values.passwordConfirm);
    formErrors.oldPassword = validateStraight(yup.string().min(6, 'Пароль має мати як мінімум 6 символів').max(255, 'Пароль повинен бути меншим за 255 символів').nullable(), values.oldPassword);

    if(values.password !== values.passwordConfirm) {
      formErrors.passwordConfirm = 'Підтвердження пароля повинно бути однаковим з паролем'
    }

    formErrors = _.pickBy(formErrors, _.identity);

    return formErrors;
  }, []);

  const onSubmit = useCallback((values: EditPutUserDto) => {
    const putUserDto: PutUserDto = {
      email: values.email,
      name: values.name,
      oldPassword: values.oldPassword,
      password: values.password
    };

    console.log('Profile Page values submit');
    console.log(JSON.stringify(putUserDto, null, 2));

    dispatch<PutUser>({
      type: 'PUT_USER',
      payload: putUserDto,
    });
  }, [dispatch]);

  const onDeleteUser = useCallback(() => {
    console.log('Delete user');

    dispatch<DeleteUser>({
      type: 'DELETE_USER',
      navigate
    });
  }, [dispatch, navigate]);


  if(!me) {
    return <NotFound />
  }

  const putUser: EditPutUserDto = {
    email: me.email,
    name: me.name,
    oldPassword: '',
    passwordConfirm: '',
    password: ''
  };

  return (
    <Profile
      onDeleteUser={onDeleteUser}
      putUser={putUser}
      onValidate={onValidate}
      onSubmit={onSubmit}
    />
  );
};

export default ProfilePage;
