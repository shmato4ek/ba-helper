import { DateTime } from 'luxon';
import React, { FC } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { ProjectDto, ProjectDtoFields, taskStateToText } from '../../store/types';
import Button from '../Button/Button';
import Icon from '../Icon/Icon';
import { AlignCenter, VerticalGrid, VerticalMargins, Wrapper } from '../Utils/Utils';

type Props = {
  project: ProjectDto;
}

export const Header = styled.h1`
  margin-bottom: 30px;
`;

export const HorizontalGrid = styled.div`
  display: grid;
  grid-gap: 20px;
  grid-auto-flow: column;

  grid-template-columns: 400px 400px;
`;

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

const Project: FC<Props> = (params) => {
  const totalHours = params.project.tasks.reduce((acc, el) => acc + el.hours, 0)
  return (
    <GridWrapper>
      <Wrapper>
        <Header>{params.project.projectName}</Header>
        <VerticalMargins>
          <VerticalGrid>
            <HorizontalGrid>
                <div><Icon type='profile-white'/> <b>Автор:</b> {params.project.author.name}</div>
                <div><Icon type='people'/><b>Залучені користувачі:</b> {params.project.users.map(x=>x.name).join(`, `)}</div>
            </HorizontalGrid>
            <HorizontalGrid>
              <div><Icon type='calendar'/><b>Дедлайн:</b> {DateTime.fromISO(params.project.deadline).toFormat('dd.MM.yyyy')}</div>
              <div><Icon type='file'/><b>Опис:</b> {params.project.description}</div>
            </HorizontalGrid>
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
                  <TR key={`task/${task.id}`}>
                    <TD>
                      {task.taskName}
                    </TD>
                    <TD>{DateTime.fromISO(task.deadline).toFormat('dd.MM.yyyy')}</TD>
                    <TD>{task.hours}</TD>
                    <TD>{task.users[0].name}</TD>
                    <TD>{taskStateToText(task.taskState)}</TD>
                  </TR>
                  {task.subtasks.map(subtask => {
                    return <TR key={`subtask/${subtask.id}`}>
                      <TD>&emsp;{subtask.name}</TD>
                      <TD></TD>
                      <TD></TD>
                      <TD></TD>
                      <TD>{taskStateToText(task.taskState)}</TD>
                    </TR>
                  })}
                </>
              )
            })}
          </tbody>
        </Table>
      </Wrapper>
      <Footer>
        <b>Загальна кількість годин: {totalHours}</b>
        <Button buttonType='button' styleType='corner'>Редагувати</Button>
      </Footer>
    </GridWrapper>
  );
};

// TODO: check that hours are counted properly

export default Project;
