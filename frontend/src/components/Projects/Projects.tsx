import React, { FC } from 'react';
import { BisMetriscDto, ProjectDto, ProjectDtoFields } from '../../store/types';
import styled from 'styled-components';
import Icon from '../Icon/Icon';
import Button from '../Button/Button';
import { DateTime } from 'luxon';

const ProjectsStyled = styled.div`
  padding: 30px 100px;
  background-color: #EEEEEE;
`;

const Table = styled.table`
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;

  width: 100%;

  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);

  overflow: scroll;
`;

const TH = styled.th`
  border: 1px solid #ddd;
  text-align: left;
  border-bottom: 1px solid #296A2F;

  padding: 12px;
`;

const TD = styled.td`
  padding: 12px;

  border-bottom: 1px solid #296A2F;
`;

const TR = styled.tr`
  background-color: #f2f2f2;

  height: 10px;
`;

type Props = {
  projects: ProjectDto[];
}

const projectFieldInfo = {
  [ProjectDtoFields.hours]: 'Кількість годин',
  [ProjectDtoFields.status]: 'Статус',
  [ProjectDtoFields.approver]: 'Затверджувач',
}

const Projects: FC<Props> = (params) => {
  
  const optionalFields: ProjectDtoFields[] = [];

  if(params.projects.length !== 0) {
    if(params.projects[0].approver)
      optionalFields.push(ProjectDtoFields.approver)
    if(params.projects[0].hours)
      optionalFields.push(ProjectDtoFields.hours)
    if(params.projects[0].status)
      optionalFields.push(ProjectDtoFields.status)
  }

  return (
    <ProjectsStyled>
      <Table>
        <thead>
          <TR>
            <TH>Назва</TH>
            <TH>Дедлайн</TH>
            {optionalFields.map(x => <TH key={x}>{projectFieldInfo[x as keyof typeof projectFieldInfo]}</TH>)}
          </TR>
        </thead>
        <tbody>
          {params.projects.map(project => {
            return (
              <TR key={project.id}>
                <TD>{project.projectName}</TD>
                <TD>{DateTime.fromJSDate(project.deadline).toFormat('MMM yyyy')}</TD>
                {optionalFields.map(x => <TD key={x}>{project[x as keyof typeof projectFieldInfo]}</TD>)}
              </TR>
            )
          })}
        </tbody>
      </Table> 
    </ProjectsStyled>
  );
};

export default Projects;
