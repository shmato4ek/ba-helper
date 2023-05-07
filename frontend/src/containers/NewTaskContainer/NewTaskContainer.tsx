import React, { useCallback, useState } from 'react';
import { useDispatch } from 'react-redux';

import * as yup from 'yup'
import * as _ from 'lodash'
import { CreateErrorObject, EditPostTaskDto, PostTaskDto, ProjectDto } from '../../store/types';
import { validateStraight } from '../../yup';
import { PostTask } from '../../store/actions';
import { TD, TDWhite, TR } from '../../components/Project/Project';
import { Formik, FormikProps } from 'formik';
import { AlignCenter } from '../../components/Utils/Utils';
import Button from '../../components/Button/Button';
import Icon from '../../components/Icon/Icon';
import FormStringField from '../../components/Form/FormStringField/FormStringField';
import FormError from '../../components/Form/FormError/FormError';
import FormDatepicker from '../../components/Form/FormDatepicker/FormDatepicker';

interface Props {
  project: ProjectDto;
}

const NewTaskContainer = ({
  project,
}: Props) => {
  const dispatch = useDispatch();
  const onValidate = useCallback((values: EditPostTaskDto) => {
    console.log('Task Page values validate');
    console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<EditPostTaskDto> = {};

    formErrors.hours = validateStraight(yup.number().typeError('Введіть правильне число').nullable(), values.hours);
    formErrors.deadline = validateStraight(yup.date().typeError('Введіть правильну дату').nullable(), values.deadline);
    formErrors.taskName = validateStraight(yup.string().max(255, "Ім'я повинно бути не більше 255 символів").required("Обов'язково"), values.taskName);

    formErrors = _.pickBy(formErrors, _.identity);

    return formErrors;
  }, []);

  const onSubmit = useCallback((values: EditPostTaskDto) => {
    const postTaskDto: PostTaskDto = {
      projectId: project.id,
      deadline: values.deadline,
      taskName: values.taskName,
      hours: Number(values.hours)
    };

    console.log('Task values submit');
    console.log(JSON.stringify(postTaskDto, null, 2));

    dispatch<PostTask>({
      type: 'POST_TASK',
      payload: postTaskDto,
    });
  }, [dispatch, project.id]);

  const editTask: EditPostTaskDto = {
    deadline: new Date() as any,
    taskName: '',
    hours: 0,
  };

  console.log('@editTask');
  console.log(JSON.stringify(editTask, null, 2));

  return (
    <Formik
      initialValues={editTask}
      enableReinitialize={true}
      validate={onValidate}
      onSubmit={onSubmit}
    >
    {({ handleSubmit }: FormikProps<EditPostTaskDto>) => (
      <>
        <TR key={`task/new`}>
          <TD>
            <FormStringField placeholder="Ім'я субтаски" name={'taskName'} label="" />
            <FormError name='taskName' />
          </TD>
          <TD>
            <FormDatepicker name={'deadline'} label="" />
            <FormError name='deadline' />
          </TD>
          <TD>
            <FormStringField placeholder="К-ість годин" name={'hours'} label="" />
            <FormError name='hours' />
          </TD>
          <TD></TD>
          <TD></TD>
          {
          <TDWhite>
            <Button buttonType='button' styleType='none' onClick={() => handleSubmit()}>
              <Icon type='green-plus' style={{width: 30, height: 30 }} />
            </Button>
          </TDWhite>
        }
        </TR>
      </>
    )}
    </Formik>
  );
};

export default NewTaskContainer;
