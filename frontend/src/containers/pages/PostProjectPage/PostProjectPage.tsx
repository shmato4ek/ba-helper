import React, { useCallback, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../../store/reducer';
import Project from '../../../components/Project/Project';
import NotFound from '../../../components/NotFound/NotFound';
import { PostProject as PostProjectAction } from '../../../store/actions';
import { CreateErrorObject, EditPostProjectDto, EditPutProjectDto, PostProjectDto } from '../../../store/types';
import * as yup from 'yup'
import { validateStraight } from '../../../yup';
import * as _ from 'lodash'
import PostProject from '../../../components/PostProject/PostProject';
import { useNavigate } from 'react-router';

const PostProjectPage = () => {
  const dispatch = useDispatch();
  const [isEditMode, setIsEditMode] = useState<boolean>(false); 
  const navigate = useNavigate()

  const onValidate = useCallback((values: EditPutProjectDto) => {
    console.log('Create Project Page values validate');
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
    const postProjectDto: PostProjectDto = {
      deadline: values.deadline,
      description: values.description,
      projectName: values.projectName,
      users: values.users.split(',').map(x => x.trim())
    };

    console.log('Create Project Page values submit');
    console.log(JSON.stringify(postProjectDto, null, 2));

    dispatch<PostProjectAction>({
      type: 'POST_PROJECT',
      payload: postProjectDto,
      navigate
    });
  }, [dispatch]);

  const postProject: EditPostProjectDto = {
    deadline: new Date() as any,
    description: '',
    projectName: '',
    users: ''
  };

  return (
    <PostProject
      postProject={postProject}
      onValidate={onValidate}
      onSubmit={onSubmit}
    />
  );
};

export default PostProjectPage;
