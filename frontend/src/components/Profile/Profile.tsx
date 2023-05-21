import { ErrorMessage, Formik, FormikProps } from 'formik';
import { DateTime } from 'luxon';
import React, { FC } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { EditPutProjectDto, EditPutUserDto, Me, ProjectDto, ProjectDtoFields, PutUserDto, taskStateToText } from '../../store/types';
import Button from '../Button/Button';
import Icon from '../Icon/Icon';
import { AlignCenter, VerticalGrid, VerticalMargins, Wrapper } from '../Utils/Utils';
import { PutProjectDto } from '../../store/types'
import FormDatepicker from '../Form/FormDatepicker/FormDatepicker';
import FormStringField from '../Form/FormStringField/FormStringField';
import FormTextareaField from '../Form/FormTextareaField/FormTextareaField';
import FormError from '../Form/FormError/FormError';
import TaskContainer from '../../containers/TaskContainer/TaskContainer';
import SubtaskContainer from '../../containers/SubtaskContainer/SubtaskContainer';
import { FieldGrid } from '../Project/Project';

export const Header = styled.h1`
  margin-bottom: 30px;
`;

export const HorizontalGrid = styled.div`
  display: grid;
  grid-auto-flow: column;

  grid-template-columns: min-content;
  grid-gap: 30px;

  align-items: center;
`;

const GridWrapper = styled.div`
  display: grid;
  grid-gap: 20px;
  grid-template-rows: 1fr auto;
`;

const Footer = styled.footer`
  padding: 20px;

  display: grid;
  grid-template-columns: auto 1fr auto;
  align-items: center;
  font-size: 32px;
`;

type Props = {
  user: Me;
  putUser: EditPutUserDto;

  onDeleteUser: () => any;
  onValidate: (values: EditPutUserDto) => any;
  onSubmit: (values: EditPutUserDto) => void;
}

const Profile: FC<Props> = (params) => {
  return (
    <GridWrapper>
      <Formik
        initialValues={params.putUser}
        enableReinitialize={true}
        validate={params.onValidate}
        onSubmit={params.onSubmit}
      >
        {({ handleSubmit }: FormikProps<EditPutUserDto>) => (
          <>
            <Wrapper>
              <Header>
                Налаштування профілю
              </Header>
              <VerticalMargins>
                <VerticalGrid>
                  <HorizontalGrid>
                    <Icon type='profile' style={{width: 185, height: 185 }} />
                    <VerticalGrid>
                      <b>Ім'я та Прізвище</b>
                      <div>
                        <FormStringField name={'name'} placeholder="Користувач 1" label="" />
                        <FormError name='name'/>
                      </div>
                    </VerticalGrid>
                    <VerticalGrid>
                      <b>Пошта</b>
                      <div>
                        <FormStringField name={'email'} placeholder="email@example.com" label="" />
                        <FormError name='email'/>
                      </div>
                    </VerticalGrid>
                  </HorizontalGrid>
                  <hr style={{ backgroundColor: 'green', height: 2}}/>
                  <HorizontalGrid>
                    <Header>
                      Налаштування профілю
                    </Header>
                    <VerticalGrid>
                      <b>Старий пароль</b>
                      <div>
                        <FormStringField name={'oldPassword'} placeholder="******" label="" isHidden />
                        <FormError name='oldPassword'/>
                      </div>
                    </VerticalGrid>
                    <VerticalGrid>
                      <b>Новий пароль</b>
                      <div>
                        <FormStringField name={'password'} placeholder="******" label="" isHidden />
                        <FormError name='password'/>
                      </div>
                      <b>Підтвердити новий пароль</b>
                      <div>
                        <FormStringField name={'passwordConfirm'} placeholder="******" label="" isHidden />
                        <FormError name='passwordConfirm'/>
                      </div>
                    </VerticalGrid>
                  </HorizontalGrid>
                </VerticalGrid>
              </VerticalMargins>
              <AlignCenter>
                <Button buttonType='button' styleType='simple'>
                  <Link
                    to={`/profile/statistics`}
                    style={{
                      textDecoration: 'none',
                      color: 'white',
                    }}
                  >Моя статистика</Link>
                </Button>
              </AlignCenter>
            </Wrapper>
            <Footer>
              <Button buttonType='button' styleType='gray' onClick={() => {
                params.onDeleteUser()
              }}>Видалити акаунт</Button>
              <div></div>
              <Button buttonType='button' styleType='simple' onClick={() => {
                handleSubmit()
              }}>
                Зберегти
              </Button>
            </Footer>
          </>
        )}
      </Formik>
    </GridWrapper>
  );
};

export default Profile;
