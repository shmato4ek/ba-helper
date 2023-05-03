

import { Formik, FormikProps } from 'formik';
import React, { FC, useCallback, useState } from 'react';
import styled from 'styled-components';
import Button from '../../../components/Button/Button';
import Font from '../../../components/Font/Font';
import FormStringField from '../../../components/Form/FormStringField/FormStringField';

import * as joi from 'joi'
import * as  _ from 'lodash';
import { ValidationError } from 'joi';
import { AppAction } from '../../../store/actions';
import { useDispatch } from 'react-redux';
import { RegisterDto } from '../../../store/types';

const LoginPageStyled = styled.div`
  display: grid;
  grid-template-rows: 100px 1fr 100px;
  grid-template-columns: 100px 1fr 1fr 1fr 100px;
  grid-template-areas:
    'top top top top top'
    'left form features features right'
    'bottom bottom bottom bottom bottom';
  gap: 0px;
  background-color: #EEEEEE;
`;

const Form = styled.div`
  grid-area: form;
  height: 100%;
  background-color: rgba(126, 211, 135, 0.58);
  padding: 90px 30px;
  box-sizing: border-box;

  font-family: Roboto;
  font-size: 24px;
  font-weight: 400;
  line-height: 42px;
  letter-spacing: 0.065em;
  text-align: left;
`;

const FeaturesList = styled.div`
  grid-area: features;
  height: 100%;
`;

const CenterHorizontalDiv = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
`;

type Props = {
}

const registerInitialValues: RegisterDto = {
  email: '',
  fullName: '',
  password: '',
  confirmPassword: '',
};

const LoginPage: FC<Props> = (params) => {
  const [isLoginMode, setLoginMode] = useState(false);
  const dispatch = useDispatch();

  const onLoginValidate = useCallback((values: RegisterDto) => {
    let errors: { [key in keyof RegisterDto]?: string } = {};

    errors.email = joi.string().email({ tlds: { allow: false } }).required().validate(values.email).error?.message;
    errors.password = joi.string().min(6).max(255).required().validate(values.password).error?.message;
    errors.fullName = joi.string().max(255).required().validate(values.fullName).error?.message;

    if (isLoginMode === false) {
      errors.confirmPassword = joi.string().min(6).max(255).required().validate(values.password).error?.message;
    }

    errors = _.pickBy(errors, _.identity);

    return errors;
  }, [isLoginMode]);

  const onLoginSubmit = useCallback(
    (values: RegisterDto) => {
      console.log('--- values ---');
      console.log(JSON.stringify(values, null, 2));
      dispatch<AppAction>({
        type: isLoginMode === true ? 'LOGIN' : 'REGISTER',
        payload: values
      });
    },
    [dispatch, isLoginMode],
  );

  return (
    <LoginPageStyled>
      <Form>
        <Formik initialValues={registerInitialValues} validate={onLoginValidate} onSubmit={onLoginSubmit}>
        {({ handleSubmit }: FormikProps<RegisterDto>) => (
          <CenterHorizontalDiv>
            <Font type='h2'>{isLoginMode ? 'Login' : 'Register'}</Font>
            <FormStringField name={'email'} placeholder="Імейл" label="Імейл" />
            <FormStringField name={'password'} placeholder="Пароль" label="Пароль" />
            <FormStringField name={'fullName'} placeholder="Повне Ім'я" label="Повне Ім'я" />
            <FormStringField name={'confirmPassword'} placeholder="Підтвердіть Пароль" label="Підтвердіть Пароль" />
            {/* <Button buttonType='submit' styleType='simple'>{isLoginMode ? 'Login' : 'Register'}</Button> */}
            <button type='submit' onClick={() => handleSubmit()}>Submit</button>
          </CenterHorizontalDiv>
        )}
        </Formik>
      </Form>
      <FeaturesList />
    </LoginPageStyled>
  );
};

export default LoginPage;

