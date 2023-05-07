import { ErrorMessage, Formik, FormikProps } from 'formik';
import { DateTime } from 'luxon';
import React, { FC } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { EditPutProjectDto, ProjectDto, ProjectDtoFields, taskStateToText } from '../../store/types';
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
import NewTaskContainer from '../../containers/NewTaskContainer/NewTaskContainer';

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

export const TDWhite = styled.td`
  padding: 12px;

  background-color: #fff;
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
  project: ProjectDto;
  putProject: EditPutProjectDto;

  onValidate: (values: EditPutProjectDto) => any;
  onSubmit: (values: EditPutProjectDto) => void;

  isEditMode: boolean;
  onEditModeSwitch: () => void;
}

const Project: FC<Props> = (params) => {
  const totalHours = params.project.tasks.reduce((acc, el) => acc + el.hours, 0)
  return (
    <GridWrapper>
      <Formik
        initialValues={params.putProject}
        enableReinitialize={true}
        validate={params.onValidate}
        onSubmit={params.onSubmit}
      >
        {({ handleSubmit }: FormikProps<EditPutProjectDto>) => (
          <>
            <Wrapper>
              <Header>
                {params.isEditMode
                  ?  <>
                      <FormStringField placeholder="Ім'я проекту" name={'projectName'} label="" />
                      <FormError name='projectName' />
                    </>
                  : <>{params.project.projectName}</>
                }
              </Header>
              <VerticalMargins>
                <VerticalGrid>
                  <HorizontalGrid>
                    <FieldGrid><Icon type='profile-white'/> <b>Автор:</b> {params.project.authorName}</FieldGrid>
                    <FieldGrid>
                      <Icon type='file'/><b>Залучені користувачі:</b>
                      {params.isEditMode
                        ?  <>
                            <FormTextareaField placeholder='Список імейлів' name={'users'} label="" />
                            <FormError name='users' />
                          </>
                        : <>{params.project.users.map(x=>x.name).join(`, `)}</>
                      }
                    </FieldGrid>
                  </HorizontalGrid>
                  <HorizontalGrid>
                    <FieldGrid>
                      <Icon type='calendar'/><b>Дедлайн:</b>
                      {params.isEditMode
                        ?  <>
                            <FormDatepicker name={'deadline'} label="" />
                            <FormError name='deadline' />
                          </>
                        : <>{DateTime.fromISO(params.project.deadline).toFormat('dd.MM.yyyy')}</>
                      }
                    </FieldGrid>
                    <FieldGrid>
                      <Icon type='file'/><b>Опис:</b>
                      {params.isEditMode
                        ? <>
                            <FormTextareaField placeholder='Опис' name={'description'} label="" />
                            <FormError name='description' />
                          </>
                        : <>{params.project.description}</>
                      }
                    </FieldGrid>
                  </HorizontalGrid>
                  <AlignCenter>
                    {!params.isEditMode
                    ? <Button buttonType='button' styleType='simple' onClick={() => params.onEditModeSwitch()}>Редагувати</Button>
                    : <Button buttonType='submit' styleType='simple' onClick={() => {
                      handleSubmit()
                      params.onEditModeSwitch();
                    }}>Зберегти</Button>}
                  </AlignCenter>
                </VerticalGrid>
              </VerticalMargins>
              <Header>Завдання</Header>
              <Table>
                <thead>
                  <TR>
                    <TH>Назва</TH>
                    <TH>Дедлайн</TH>
                    <TH>Кількість годин</TH>
                    <TH>Виконавець</TH>
                    <TH>Статус</TH>
                  </TR>
                </thead>
                <tbody>
                  {params.project.tasks.map(task => {
                    return (
                      <>
                        <TaskContainer
                          projectUsers={params.project.users}
                          task={task}
                          key={`task/${task.id}`}
                        />
                        {task.subtasks.map(subtask => {
                          return <SubtaskContainer
                            subtask={subtask}
                            key={`subtask/${subtask.id}`}
                          />
                        })}
                      </>
                    )
                  })}
                  <NewTaskContainer project={params.project}/>
                </tbody>
              </Table>
            </Wrapper>
            <Footer>
              <b>Загальна кількість годин: {totalHours}</b>
            </Footer>
          </>
        )}
      </Formik>
    </GridWrapper>
  );
};

// TODO: check that hours are counted properly

export default Project;



