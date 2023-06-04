import React, {FC, useCallback} from 'react';
import { ProjectDto, ProjectDtoFields } from '../../store/types';
import styled from 'styled-components';
import { DateTime } from 'luxon';
import { Link } from 'react-router-dom';
import Button from '../Button/Button';
import { AlignCenter } from '../Utils/Utils';
import Icon from "../Icon/Icon";
import {DeleteProject, DeleteTask, PutProjectArchive, PutProjectUnarchive} from "../../store/actions";
import {useDispatch} from "react-redux";

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

export const TDNarrow = styled.td`
  padding: 6px;

  border-bottom: 1px solid #296A2F;
`;

export const TR = styled.tr`
  background-color: #fff;

  height: 10px;
`;

type Props = {
  mode: 'my-projects' | 'owned-projects' | 'archived-projects';
  projects: ProjectDto[];
  optionalFields: ProjectDtoFields[];
}

const projectFieldInfo = {
  [ProjectDtoFields.deadline]: 'Дедлайн',
  [ProjectDtoFields.archivedDate]: 'Дата архівації',
  [ProjectDtoFields.hours]: 'Кількість годин',
  [ProjectDtoFields.authorName]: 'Затверджувач',
  [ProjectDtoFields.taskCount]: 'Кількість завдань',
}

const Projects: FC<Props> = (params) => {
  const dispatch = useDispatch();

  const isOwned = params.mode === 'owned-projects';
  const isArchived = params.mode === 'archived-projects';

  const onArchive = useCallback((projectId: number) => {
    console.log('Project archived');

    dispatch<PutProjectArchive>({
      type: 'PUT_PROJECT_ARCHIVE',
      payload: {
        projectId: projectId
      }
    })
  }, [dispatch]);

  const onUnarchive = useCallback((projectId: number) => {
    console.log('Project unarchived');

    dispatch<PutProjectUnarchive>({
      type: 'PUT_PROJECT_UNARCHIVE',
      payload: {
        projectId: projectId
      }
    })
  }, [dispatch]);

  const onDelete = useCallback((projectId: number) => {
    console.log('Project deleted');

    dispatch<DeleteProject>({
      type: 'DELETE_PROJECT',
      payload: {
        projectId: projectId
      }
    })
  }, [dispatch]);

  return (
    <ProjectsStyled>
      <Table>
        <thead>
          <TR>
            <TH>Назва</TH>
            {params.optionalFields.map(x => <TH key={x}>{projectFieldInfo[x as keyof typeof projectFieldInfo]}</TH>)}
            {(isOwned || isArchived) &&
            <TH></TH>}
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
                {params.optionalFields.map(x => {
                  let fieldData;
                  if(x === 'authorName') {
                    fieldData = project.authorName;
                  } else if(x === 'taskCount') {
                    fieldData = project.tasks.length;
                  } else if(x === 'deadline') {
                    fieldData = DateTime.fromISO(project.deadline).toFormat('dd.MM.yyyy');
                  } else if(x === 'archivedDate') {
                    if (project.archivedDate != null) {
                      fieldData = DateTime.fromISO(project.archivedDate).toFormat('dd.MM.yyyy');
                    } else {
                      fieldData = "Невідомо"
                    }
                  } else if(x === 'hours') {
                    fieldData = project.hours ?? "0"
                  } else {
                    fieldData = project[x as keyof ProjectDto]
                  }

                  return <TD key={x}>{fieldData as any}</TD>
                })}
                {isOwned &&
                    <TDNarrow>
                      <AlignCenter>
                        <Button buttonType='button' styleType='none' onClick={() => { onArchive(project.id) }}>
                          <Icon type='archive' style={{width: 30, height: 30 }} />
                        </Button>
                      </AlignCenter>
                    </TDNarrow>
                    }
                {isArchived &&
                    <TDNarrow>
                      <AlignCenter>
                        <span>
                          <Button buttonType='button' styleType='none' onClick={() => { onUnarchive(project.id) }}>
                            <Icon type='arrow-up' style={{width: 30, height: 30, marginRight: "1rem" }} />
                          </Button>
                          <Button buttonType='button' styleType='none' onClick={() => { onDelete(project.id) }}>
                            <Icon type='trash-can' style={{width: 30, height: 30 }} />
                          </Button>
                        </span>
                      </AlignCenter>
                    </TDNarrow>
                }
              </TR>

            )
          })}
        </tbody>
      </Table>
      <AlignCenter>
        {params.mode === 'owned-projects' &&
        <Button buttonType='button' styleType='simple'>
          <Link
            to="/projects/new"
            style={{
              textDecoration: 'none',
              color: 'white',
            }}
          >Створити</Link>
        </Button>
        }
      </AlignCenter>
    </ProjectsStyled>
  );
};

export default Projects;
