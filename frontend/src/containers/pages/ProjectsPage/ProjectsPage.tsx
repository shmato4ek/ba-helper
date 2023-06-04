import React, {useEffect} from 'react';
import {useDispatch, useSelector} from 'react-redux';

import {AppState} from '../../../store/reducer';
import Projects from '../../../components/Projects/Projects';
import {useLocation} from 'react-router';
import {ProjectDtoFields, ServiceRoutes} from '../../../store/types';
import Loading from '../../../components/Loading/Loading';
import {AppAction} from '../../../store/actions';
import {Link} from "react-router-dom";
import Button from "../../../components/Button/Button";
import {AlignLeft} from "../../../components/Utils/Utils";

const ProjectsPage = () => {
  const dispatch = useDispatch();
  const projects = useSelector((state: AppState) => state.projects);
  const projectsOwn = useSelector((state: AppState) => state.projectsOwn);
  const projectsArchive = useSelector((state: AppState) => state.projectsArchive);
  const getProjectsAction = useSelector((state: AppState) => state.actions.getProjects);
  const getProjectsOwnAction = useSelector((state: AppState) => state.actions.getProjectsOwn);
  const getProjectsArchiveAction = useSelector((state: AppState) => state.actions.getProjectsArchive);
  const location = useLocation();

  useEffect(()=> {
    if(location.pathname === ServiceRoutes.MY_PROJECTS && !getProjectsAction.loading && !projects) {
      dispatch<AppAction>({ type: 'GET_PROJECTS' });
    } else if(location.pathname === ServiceRoutes.OWNED_PROJECTS &&!getProjectsOwnAction.loading && !projectsOwn) {
      dispatch<AppAction>({ type: 'GET_PROJECTS_OWN' });
    } else if(location.pathname === ServiceRoutes.ARCHIVED_PROJECTS &&!getProjectsArchiveAction.loading && !projectsArchive) {
        dispatch<AppAction>({ type: 'GET_PROJECTS_ARCHIVE' });
    }
  }, [dispatch, projects, getProjectsAction.loading, getProjectsOwnAction.loading, getProjectsArchiveAction.loading, location.pathname, projectsOwn, projectsArchive]);


  if(location.pathname === '/owned-projects') {
    if(!projectsOwn) {
      return <Loading />
    }

    return (
        <div>
            <AlignLeft>
                <Button buttonType='button' styleType='gray'>
                    <Link
                        to="/archived-projects"
                        style={{
                            textDecoration: 'none',
                            color: 'white',
                        }}
                    >Архів</Link>
                </Button>
            </AlignLeft>
          <Projects
              mode={'owned-projects'}
              projects={projectsOwn}
              optionalFields={[ProjectDtoFields.deadline, ProjectDtoFields.hours, ProjectDtoFields.taskCount]}
          />
        </div>
    );
  }

    if(location.pathname === '/archived-projects') {
        if(!projectsArchive) {
            return <Loading />
        }

        return (
            <Projects
                mode={'archived-projects'}
                projects={projectsArchive}
                optionalFields={[ProjectDtoFields.archivedDate, ProjectDtoFields.hours, ProjectDtoFields.taskCount]}
            />
        );
    }

  if(!projects) {
    return  <Loading />
  }

  return (
    <Projects
      mode={'my-projects'}
      projects={projects}
      optionalFields={[ProjectDtoFields.deadline, ProjectDtoFields.authorName, ProjectDtoFields.hours]}
    />
  );
};

export default ProjectsPage;
