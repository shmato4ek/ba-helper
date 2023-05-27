import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../../store/reducer';
import Page from '../../../components/Page/Page';
import Projects from '../../../components/Projects/Projects';
import { useLocation } from 'react-router';
import { ProjectDtoFields, ServiceRoutes } from '../../../store/types';
import Loading from '../../../components/Loading/Loading';
import { AppAction } from '../../../store/actions';

const ProjectsPage = () => {
  const dispatch = useDispatch();
  const projects = useSelector((state: AppState) => state.projects);
  const projectsOwn = useSelector((state: AppState) => state.projectsOwn);
  const getProjectsAction = useSelector((state: AppState) => state.actions.getProjects);
  const getProjectsOwnAction = useSelector((state: AppState) => state.actions.getProjectsOwn);
  const location = useLocation();

  useEffect(()=> {
    if(location.pathname === ServiceRoutes.MY_PROJECTS && !getProjectsAction.loading && !projects) {
      dispatch<AppAction>({ type: 'GET_PROJECTS' });
    } else if(location.pathname === ServiceRoutes.OWNED_PROJECTS &&!getProjectsOwnAction.loading && !projectsOwn) {
      dispatch<AppAction>({ type: 'GET_PROJECTS_OWN' });
    }
  }, [dispatch, projects, getProjectsAction.loading, getProjectsOwnAction.loading, location.pathname, projectsOwn]);


  if(location.pathname === '/owned-projects') {
    if(!projectsOwn) {
      return <Loading />
    }

    return (
      <Projects
        mode={'owned-projects'}
        projects={projectsOwn}
        optionalFields={[ProjectDtoFields.hours, ProjectDtoFields.taskCount]}
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
      optionalFields={[ProjectDtoFields.authorName, ProjectDtoFields.hours]}
    />
  );
};

export default ProjectsPage;
