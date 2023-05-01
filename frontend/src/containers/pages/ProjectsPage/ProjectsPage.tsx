import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../../store/reducer';
import Page from '../../../components/Page/Page';
import Projects from '../../../components/Projects/Projects';

const ProjectsPage = () => {
  const dispatch = useDispatch();
  const state = useSelector((state: AppState) => state);

  useEffect(()=> {
    //
  }, [dispatch, state.actions.getMyself.success]);

  return (
    <Page>
      <Projects projects={[
          {
            id: 1,
            projectName: 'Test Project',
            approver: 'A',
            status: 'asd',
            deadline: new Date(),
          },
          {
            id: 2,
            projectName: 'Test Project 2',
            approver: 'A2',
            status: 'asd 1',
            deadline: new Date(),
          },
        ]}/>
    </Page>
  );
};

export default ProjectsPage;
