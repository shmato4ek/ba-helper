import React, { FC } from 'react';
import { ProjectDto, ProjectDtoFields } from '../../store/types';
import styled from 'styled-components';
import { DateTime } from 'luxon';
import { Link } from 'react-router-dom';
import Button from '../Button/Button';
import { AlignCenter } from '../Utils/Utils';

export const ProjectsStyled = styled.div`
  padding: 30px 100px;
  background-color: #fff;
`;

export const Table = styled.table`
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;

  width: 100%;

  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);

  margin-bottom: 20px;

  overflow: scroll;
`;

export const TH = styled.th`
  border: 1px solid #ddd;
  text-align: left;
  border-bottom: 1px solid #296A2F;

  padding: 12px;
`;

export const TD = styled.td`
  padding: 12px;

  border-bottom: 1px solid #296A2F;
`;

export const TR = styled.tr`
  background-color: #fff;

  height: 10px;
`;

type Props = {
  projects: ProjectDto[];
  optionalFields: ProjectDtoFields[];
}

const projectFieldInfo = {
  [ProjectDtoFields.hours]: 'Кількість годин',
  [ProjectDtoFields.authorName]: 'Затверджувач',
  [ProjectDtoFields.taskCount]: 'Кількість завдань',
}

const Projects: FC<Props> = (params) => {
  return (
    <ProjectsStyled>
      <Table>
        <thead>
          <TR>
            <TH>Назва</TH>
            <TH>Дедлайн</TH>
            {params.optionalFields.map(x => <TH key={x}>{projectFieldInfo[x as keyof typeof projectFieldInfo]}</TH>)}
          </TR>
        </thead>
        <tbody>
          {params.projects.map(project => {
            return (
              <TR key={project.id}>
                <TD>
                  <Link to={`/projects/${project.id}`}>
                    {project.projectName}
                  </Link>  
                </TD>
                <TD>{DateTime.fromISO(project.deadline).toFormat('dd.MM.yyyy')}</TD>
                {params.optionalFields.map(x => {
                  let fieldData;
                  if(x === 'authorName') {
                    fieldData = project.authorName;
                  } else if(x === 'taskCount') {
                    fieldData = project.tasks.length;
                  } else {
                    fieldData = project[x as keyof ProjectDto]
                  }

                  return <TD key={x}>{fieldData as any}</TD>
                })}
              </TR>
            )
          })}
        </tbody>
      </Table>
      <AlignCenter>
        <Button buttonType='button' styleType='simple'>
          <Link
            to="/projects/new"
            style={{
              textDecoration: 'none',
              color: 'white',
            }}
          >Створити</Link>
        </Button>
      </AlignCenter>
    </ProjectsStyled>
  );
};

export default Projects;
