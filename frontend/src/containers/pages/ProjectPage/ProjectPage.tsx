import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../../store/reducer';
import Page from '../../../components/Page/Page';
import { useLocation, useParams } from 'react-router';
import Project from '../../../components/Project/Project';
import NotFound from '../../../components/NotFound/NotFound';
import { AppAction } from '../../../store/actions';


const ProjectPage = () => {
  const dispatch = useDispatch();
  const currentProject = useSelector((state: AppState) => state.currentProject);
  const getProjectAction = useSelector((state: AppState) => state.actions.getProject);
  const location = useLocation();
  const { projectId } = useParams();
  const [isEditMode, setIsEditMode] = useState<boolean>(false);

  useEffect(()=> {
    if(!getProjectAction.loading && !currentProject) {
      dispatch<AppAction>({ type: 'GET_PROJECT', payload: {
          id: Number(projectId as any)
        }
      });
    }
  }, [dispatch, projectId, getProjectAction, currentProject]);

  if(!currentProject) {
    return <NotFound />
  }

  return (
    <Project
      project={currentProject}
    />
  );
};

export default ProjectPage;
