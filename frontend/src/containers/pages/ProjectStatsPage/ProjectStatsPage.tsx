import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../../store/reducer';
import { useParams } from 'react-router';
import Project from '../../../components/Project/Project';
import NotFound from '../../../components/NotFound/NotFound';
import { AppAction } from '../../../store/actions';
import * as _ from 'lodash'
import ProjectClusterInfo from '../../../components/ProjectClusterInfo/ProjectClusterInfo';

const ProjectStatsPage = () => {
  const dispatch = useDispatch();
  const currentProjectStats = useSelector((state: AppState) => state.currentProjectStats);
  const getProjectAction = useSelector((state: AppState) => state.actions.getProject);
  const { projectId } = useParams();

  console.log('@currentProjectStats');
  console.log(JSON.stringify(currentProjectStats, null, 2));

  useEffect(()=> {
    if(!getProjectAction.loading && !currentProjectStats) {
      dispatch<AppAction>({ type: 'GET_PROJECT_STATISTICS', payload: {
          id: Number(projectId as any)
        }
      });
    }
  }, [dispatch, projectId, getProjectAction, currentProjectStats]);

  if(!currentProjectStats) {
    return <NotFound />
  }

  return (
    <ProjectClusterInfo
      currentProjectStats={currentProjectStats}
    />
  );
};

export default ProjectStatsPage;
