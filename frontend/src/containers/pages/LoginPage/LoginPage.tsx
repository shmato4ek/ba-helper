

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
import { EditRegisterDto, LoginDto, RegisterDto } from '../../../store/types';
import { useNavigate } from 'react-router';
import FormError from '../../../components/Form/FormError/FormError';

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

const FieldGrid = styled.div`
  width: 100%;
  margin-bottom: 20px;
  display: grid;
  align-items: center;
  justify-items: center;
`

type Props = {
}

const registerInitialValues: EditRegisterDto = {
  email: '',
  name: '',
  password: '',
  confirmPassword: '',
};

const LoginPage: FC<Props> = (params) => {
  const [isLoginMode, setLoginMode] = useState(false);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const onLoginValidate = useCallback((values: EditRegisterDto) => {
    let errors: { [key in keyof EditRegisterDto]?: string } = {};

    errors.email = joi.string().email({ tlds: { allow: false } }).required().validate(values.email).error?.message;
    errors.password = joi.string().min(6).max(255).required().validate(values.password).error?.message;
    
    if (!isLoginMode) {
      errors.name = joi.string().max(255).required().validate(values.name).error?.message;
      errors.confirmPassword = joi.string().min(6).max(255).required().validate(values.password).error?.message;

      if(values.password !== values.confirmPassword) {
        errors.confirmPassword = 'Підтвердження пароля повинно бути однаковим з паролем'
      }
    }

    console.log('Login errors');
    console.log(JSON.stringify(errors, null, 2));

    errors = _.pickBy(errors, _.identity);

    return errors;
  }, [isLoginMode]);

  const onLoginSubmit = useCallback(
    (values: EditRegisterDto) => {
      console.log('--- values ---');
      console.log(JSON.stringify(values, null, 2));

      if(isLoginMode) {
        const loginDto: LoginDto = {
          email: values.email,
          password: values.password
        }

        dispatch<AppAction>({
          type: 'LOGIN',
          payload: loginDto,
          navigate,
        });
      } else {
        const registerDto: RegisterDto = {
          email: values.email,
          name: values.name,
          password: values.password
        }

        dispatch<AppAction>({
          type: 'REGISTER',
          payload: registerDto,
          navigate,
        });
      }
    },
    [dispatch, isLoginMode],
  );

  return (
    <LoginPageStyled>
      <Form>
        <Formik initialValues={registerInitialValues} validate={onLoginValidate} onSubmit={onLoginSubmit}>
        {({ handleSubmit }: FormikProps<EditRegisterDto>) => (
          <CenterHorizontalDiv>
            <Font type='h2'>{isLoginMode ? 'Login' : 'Register'}</Font>
            <FormStringField name={'email'} placeholder="Імейл" label="Імейл" textAreaStyle={{width: '100%', marginBottom: 10}} />
            <FormError name='email'/>
            <FormStringField name={'password'} placeholder="Пароль" label="Пароль" textAreaStyle={{width: '100%', marginBottom: 10}} isHidden/>
            <FormError name='password'/>
            {!isLoginMode &&
              <>
                <FormStringField name={'confirmPassword'} placeholder="Підтвердіть Пароль" label="Підтвердіть Пароль" textAreaStyle={{width: '100%', marginBottom: 10}} isHidden/>
                <FormError name='confirmPassword'/>
                <FormStringField name={'name'} placeholder="Повне Ім'я" label="Повне Ім'я" textAreaStyle={{width: '100%', marginBottom: 10}}/>
                <FormError name='name'/>
              </>
            }
            <FieldGrid>
              <Button buttonType='submit' styleType='simple' onClick={() => handleSubmit()}>{isLoginMode ? 'Login' : 'Register'}</Button>
            </FieldGrid>
            <button type='button' onClick={() => setLoginMode(!isLoginMode)} style={{ fontSize: 18}}>
              {isLoginMode ? 'Switch to Register' : 'Switch to Login'}
            </button>
          </CenterHorizontalDiv>
        )}
        </Formik>
      </Form>
      <FeaturesList />
    </LoginPageStyled>
  );
};

export default LoginPage;

