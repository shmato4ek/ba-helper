import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../../store/reducer';
import { useParams } from 'react-router';
import Project from '../../../components/Project/Project';
import NotFound from '../../../components/NotFound/NotFound';
import { AppAction } from '../../../store/actions';
import * as _ from 'lodash'
import ProjectClusterInfo from '../../../components/ProjectClusterInfo/ProjectClusterInfo';
import {ClusterType} from "../../../store/types";

const ProjectStatsPageV2 = () => {
  const dispatch = useDispatch();
  const currentProjectStats = useSelector((state: AppState) => state.currentProjectStats);
  const getProjectAction = useSelector((state: AppState) => state.actions.getProject);
  const { projectId } = useParams();

  useEffect(()=> {
    if(!getProjectAction.loading && !currentProjectStats) {
      dispatch<AppAction>({ type: 'GET_PROJECT_STATISTICS', payload: {
          id: Number(projectId as any),
          type: ClusterType.Dbscan
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

export default ProjectStatsPageV2;
