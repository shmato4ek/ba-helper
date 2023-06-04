import React, { useCallback, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import * as yup from 'yup'
import * as _ from 'lodash'
import {
  CreateErrorObject,
  EditPutTaskDto,
  PutTaskDto,
  TaskDto,
  TaskState,
  taskStates,
  taskStateToText,
  taskTopics, taskTopicToText,
  UserDto
} from '../../store/types';
import { validateStraight } from '../../yup';
import {DeleteTask, PutTask, PutTaskApprove, PutTaskAssign, PutTaskState} from '../../store/actions';
import { TD, TDWhite, TR } from '../../components/Project/Project';
import { DateTime } from 'luxon';
import { Formik, FormikProps } from 'formik';
import { AlignCenter } from '../../components/Utils/Utils';
import Button from '../../components/Button/Button';
import Icon from '../../components/Icon/Icon';
import FormStringField from '../../components/Form/FormStringField/FormStringField';
import FormError from '../../components/Form/FormError/FormError';
import FormDatepicker from '../../components/Form/FormDatepicker/FormDatepicker';
import FormDropdown from '../../components/Form/FormDropdown/FormDropdown';

interface Props {
  task: TaskDto;
  projectUsers: UserDto[];
  canEdit: boolean;
  canEditState: boolean;
}

const TaskContainer = ({
  task,
  projectUsers,
  canEdit,
  canEditState,
}: Props) => {
  const dispatch = useDispatch();
  const [isEditMode, setIsEditMode] = useState<boolean>(false);

  const onValidate = useCallback((values: EditPutTaskDto) => {
    console.log('Task Page values validate');
    console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<EditPutTaskDto> = {};

    formErrors.hours = validateStraight(yup.number().typeError('Введіть правильне число').nullable(), values.hours);
    formErrors.deadline = validateStraight(yup.date().typeError('Введіть правильну дату').nullable(), values.deadline);
    formErrors.taskName = validateStraight(yup.string().max(255, "Ім'я повинно бути не більше 255 символів").required("Обов'язково"), values.taskName);

    formErrors = _.pickBy(formErrors, _.identity);

    return formErrors;
  }, []);

  const onSubmit = useCallback((values: EditPutTaskDto) => {
    const putTaskDto: PutTaskDto = {
      id: task.id,
      deadline: values.deadline,
      taskName: values.taskName,
      hours: Number(values.hours)
    };

    console.log('Task values submit');
    console.log(JSON.stringify(putTaskDto, null, 2));

    dispatch<PutTask>({
      type: 'PUT_TASK',
      payload: putTaskDto,
    });
  }, [dispatch, task]);

  const onEditModeSwitch = useCallback(() => {
    setIsEditMode(!isEditMode);
  }, [isEditMode]);

  const onTaskStateChoose = useCallback((taskState: TaskState) => {
    console.log('Task state submit');
    console.log(JSON.stringify(taskState, null, 2));

    if(taskState !== TaskState.Approve) {
      dispatch<PutTaskState>({
        type: 'PUT_TASK_STATE',
        payload: {
          taskId: task.id,
          taskState,
        }
      });
    } else {
      dispatch<PutTaskApprove>({
        type: 'PUT_TASK_APPROVE',
        payload: {
          taskId: task.id,
        }
      });
    }

  }, [dispatch, task.id]);

  const onTaskAssign = useCallback((email: string) => {
    console.log('Task assign');
    console.log(JSON.stringify(email, null, 2));

    dispatch<PutTaskAssign>({
      type: 'PUT_TASK_ASSIGN',
      payload: {
        taskId: task.id,
        email,
      }
    })
  }, [dispatch, task.id]);

  const onDelete = useCallback(() => {
    console.log('Task deleted');

    dispatch<DeleteTask>({
      type: 'DELETE_TASK',
      payload: {
        taskId: task.id
      }
    })
  }, [dispatch, task.id]);

  const editTask: EditPutTaskDto = {
    deadline: new Date(task.deadline) as any,
    taskName: task.taskName,
    hours: task.hours,
    taskState: task.taskState,
    assignedUser: task.users.length ? task.users[0].email : ''
  };

  return (
    <Formik
      initialValues={editTask}
      enableReinitialize={true}
      validate={onValidate}
      onSubmit={onSubmit}
    >
    {({ handleSubmit, values }: FormikProps<EditPutTaskDto>) => (
      <>
        <TR key={`task/${task.id}`}>
          <TD>
            {canEdit && isEditMode
              ?  <>
                  <FormStringField placeholder="Ім'я таски" name={'taskName'} label="" />
                  <FormError name='taskName' />
                </>
              : <>{task.taskName}</>}
          </TD>
          <TD>
            {canEdit && isEditMode
              ?  <>
                  <FormDatepicker name={'deadline'} label="" />
                  <FormError name='deadline' />
                </>
              : <>{DateTime.fromISO(task.deadline).toFormat('dd.MM.yyyy')}</>}
          </TD>
          <TD>
            {canEdit && isEditMode
              ?  <>
                  <FormStringField placeholder="К-ість годин" name={'hours'} label="" />
                  <FormError name='hours' />
                </>
              : <>{task.hours}</>}
          </TD>
          <TD>
            {canEdit && isEditMode
                ?  <>
                  <FormDropdown
                      name='tag'
                      placeholder="Тег завдання"
                      label=""
                      options={taskTopics.map(x => x)}
                      labels={taskTopics.map(x => taskTopicToText(x))}
                      onOptionChoose={() => {}}
                  />
                  <FormError name='taskState' />
                </>
                : <>{task.tags[0] ? taskTopicToText(task.tags[0]) : "No tags"}</>}
          </TD>
          <TD>
            {canEdit && isEditMode
              ?  <>
                  <FormDropdown
                    name='assignedUser'
                    placeholder="Виконавець"
                    label=""
                    options={projectUsers.map(x => x.email)}
                    labels={projectUsers.map(x => x.name)}
                    onOptionChoose={onTaskAssign as any}
                  />
                  <FormError name='assignedUser' />
                </>
              : <>{task.users[0] ? task.users[0].name : "Не призначено"}</>}
          </TD>
          <TD>
            {isEditMode && task.taskState !== TaskState.Approve
              ?  <>
                  <FormDropdown
                    name='taskState'
                    placeholder="Стан завдання"
                    label=""
                    options={canEdit ? [1,2,3,4].map(x => x) : [1,2,3].map(x => x) }
                    labels={canEdit ? [1,2,3,4].map(x => taskStateToText(x)) : [1,2,3].map(x => taskStateToText(x))}
                    onOptionChoose={onTaskStateChoose as any}
                  />
                  <FormError name='taskState' />
                </>
              : <>{taskStateToText(task.taskState)}</>}
          </TD>
          <TDWhite>
          {(canEdit || canEditState) && (isEditMode
                ? <Button buttonType='button' styleType='none' onClick={() => {
                    handleSubmit();
                    onEditModeSwitch();
                  }}>
                  <Icon type='save' style={{width: 30, height: 30 }} />
                </Button> 
                : <Button buttonType='button' styleType='none' onClick={() => onEditModeSwitch()}>
                  <Icon type='edit-pencil' style={{width: 30, height: 30 }} />
                </Button>
          )}
          </TDWhite>
          <TDWhite>
            {(canEdit || canEditState) && isEditMode && (
                <Button buttonType='button' styleType='none' onClick={onDelete as any}>
                    <Icon type='trash-can' style={{width: 30, height: 30 }} />
                </Button>
            )}
          </TDWhite>
        </TR>
      </>
    )}
    </Formik>
  );
};

export default TaskContainer;
