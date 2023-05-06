import React, { useCallback, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../../store/reducer';
import { useParams } from 'react-router';
import Project from '../../../components/Project/Project';
import NotFound from '../../../components/NotFound/NotFound';
import { AppAction, PutProject } from '../../../store/actions';
import { CreateErrorObject, EditPutProjectDto, PutProjectDto } from '../../../store/types';
import * as yup from 'yup'
import { validateStraight } from '../../../yup';
import * as _ from 'lodash'

const ProjectPage = () => {
  const dispatch = useDispatch();
  const currentProject = useSelector((state: AppState) => state.currentProject);
  const getProjectAction = useSelector((state: AppState) => state.actions.getProject);
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

  const onValidate = useCallback((values: EditPutProjectDto) => {
    console.log('Project Page values validate');
    console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<EditPutProjectDto> = {};

    formErrors.users = '';
    const usersEmailList = values.users.split(',').map(x => x.trim())
    for (let i = 0; i < usersEmailList.length; i++) {
      const userEmail = usersEmailList[i];

      const error = validateStraight(yup.string().email('Введіть правильний імейл'), userEmail);
      formErrors.users = error;
    }

    formErrors.deadline = validateStraight(yup.date().typeError('Введіть правильну дату').nullable(), values.deadline);
    formErrors.description = validateStraight(yup.string().max(255, 'Опис повинен бути не більше 255 символів').required("Обов'язково"), values.description);

    formErrors = _.pickBy(formErrors, _.identity);

    return formErrors;
  }, []);

  const onSubmit = useCallback((values: EditPutProjectDto) => {
    if(!currentProject) return;

    const putProjectDto: PutProjectDto = {
      id: currentProject.id,
      deadline: values.deadline,
      description: values.description,
      projectName: values.projectName,
      users: values.users.split(',').map(x => x.trim())
    };

    console.log('Project Page values submit');
    console.log(JSON.stringify(putProjectDto, null, 2));

    dispatch<PutProject>({
      type: 'PUT_PROJECT',
      payload: putProjectDto,
    });
  }, [dispatch, currentProject]);

  const onEditModeSwitch = useCallback(() => {
    setIsEditMode(!isEditMode);
  }, [isEditMode]);

  if(!currentProject) {
    return <NotFound />
  }

  const putProject = {
    id: currentProject.id,
    deadline: new Date(currentProject.deadline) as any,
    description: currentProject.description,
    projectName: currentProject.projectName,
    users: currentProject.users.map(x => x.email).join(',')
  };

  return (
    <Project
      putProject={putProject}
      isEditMode={isEditMode}
      project={currentProject}
      onValidate={onValidate}
      onSubmit={onSubmit}
      onEditModeSwitch={onEditModeSwitch}
    />
  );
};

export default ProjectPage;
