import { Formik, FormikProps } from 'formik';
import React, { FC } from 'react';
import styled from 'styled-components';
import { EditPostProjectDto } from '../../store/types';
import Button from '../Button/Button';
import Icon from '../Icon/Icon';
import { AlignCenter, VerticalGrid, VerticalMargins, Wrapper } from '../Utils/Utils';
import FormDatepicker from '../Form/FormDatepicker/FormDatepicker';
import FormStringField from '../Form/FormStringField/FormStringField';
import FormTextareaField from '../Form/FormTextareaField/FormTextareaField';
import FormError from '../Form/FormError/FormError';

export const Header = styled.h1`
  margin-bottom: 30px;
`;

export const HorizontalGrid = styled.div`
  display: grid;
  grid-gap: 20px;
  grid-auto-flow: column;

  grid-template-columns: 400px 400px;

  align-items: center;
`;

export const FieldGrid = styled.div`
  display: grid;
  grid-auto-flow: column;
  grid-auto-columns: max-content;
  align-items: center;

  grid-gap: 10px;
`

export const Table = styled.table`
  width: 100%;
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;


  overflow: scroll;
`;

export const TH = styled.th`
  text-align: left;

  padding: 12px;
`;

export const TD = styled.td`
  padding: 12px;

  background-color: #C8EFCC;
  border-bottom: 1px solid black;
`;

export const TR = styled.tr`

  height: 10px;
`;

const GridWrapper = styled.div`
  display: grid;
  grid-gap: 20px;
  grid-template-rows: 1fr auto;
`;

const Footer = styled.footer`
  padding: 20px;
  background: rgba(0, 143, 49, 0.72);

  display: grid;
  grid-template-columns: 1fr auto;
  align-items: center;
  color: white;
  font-size: 32px;
`;

type Props = {
  postProject: EditPostProjectDto;

  onValidate: (values: EditPostProjectDto) => any;
  onSubmit: (values: EditPostProjectDto) => void;
}

const PostProject: FC<Props> = (params) => {
  return (
    <GridWrapper>
      <Formik
        initialValues={params.postProject}
        enableReinitialize={true}
        validate={params.onValidate}
        onSubmit={params.onSubmit}
      >
        {({ handleSubmit }: FormikProps<EditPostProjectDto>) => (
          <>
            <Wrapper>
              <Header>
                <FormStringField placeholder="Ім'я проекту" name={'projectName'} label="" />
                <FormError name='projectName' />
              </Header>
              <VerticalMargins>
                <VerticalGrid>
                  <HorizontalGrid>
                    <FieldGrid><Icon type='profile-white'/> <b>Автор:</b>Ви</FieldGrid>
                    <FieldGrid>
                      <Icon type='file'/><b>Залучені користувачі:</b>
                      <FormTextareaField placeholder='Список імейлів' name={'users'} label="" />
                      <FormError name='users' />
                    </FieldGrid>
                  </HorizontalGrid>
                  <HorizontalGrid>
                    <FieldGrid>
                      <Icon type='calendar'/><b>Дедлайн:</b>
                      <FormDatepicker name={'deadline'} label="" />
                      <FormError name='deadline' />
                    </FieldGrid>
                    <FieldGrid>
                      <Icon type='file'/><b>Опис:</b>
                      <FormTextareaField placeholder='Опис' name={'description'} label="" />
                      <FormError name='description' />
                    </FieldGrid>
                  </HorizontalGrid>
                  <AlignCenter>
                    <Button buttonType='submit' styleType='simple' onClick={() => {
                      handleSubmit()
                    }}>Створити</Button>
                  </AlignCenter>
                </VerticalGrid>
              </VerticalMargins>
            </Wrapper>
          </>
        )}
      </Formik>
    </GridWrapper>
  );
};

export default PostProject;



