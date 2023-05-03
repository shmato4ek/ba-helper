import React, { useCallback, useState } from 'react';
import { useDispatch } from 'react-redux';

import * as yup from 'yup'
import * as _ from 'lodash'
import { CreateErrorObject, EditSubtaskDto, PutSubtaskDto, SubtaskDto, TaskState, taskStates, taskStateToText } from '../../store/types';
import { validateStraight } from '../../yup';
import { PutSubtask, PutSubtaskState } from '../../store/actions';
import { TD, TR } from '../../components/Project/Project';
import FormStringField from '../../components/Form/FormStringField/FormStringField';
import FormError from '../../components/Form/FormError/FormError';
import { Formik, FormikProps } from 'formik';
import Icon from '../../components/Icon/Icon';
import Button from '../../components/Button/Button';
import { AlignCenter } from '../../components/Utils/Utils';
import FormDropdown from '../../components/Form/FormDropdown/FormDropdown';

interface Props {
  subtask: SubtaskDto;
}

const SubtaskContainer = ({
  subtask,
}: Props) => {
  const dispatch = useDispatch();
  const [isEditMode, setIsEditMode] = useState<boolean>(false);

  const onValidate = useCallback((values: EditSubtaskDto) => {
    console.log('Subtask values validate');
    console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<EditSubtaskDto> = {};

    formErrors.name = validateStraight(yup.string().max(255, "Ім'я повинно бути не більше 255 символів").required("Обов'язково"), values.name);

    formErrors = _.pickBy(formErrors, _.identity);

    return formErrors;
  }, []);

  const onSubmit = useCallback((values: EditSubtaskDto) => {
    const putSubtaskDto: PutSubtaskDto = {
      id: subtask.id,
      name: values.name,
    };

    console.log('Task Page values submit');
    console.log(JSON.stringify(putSubtaskDto, null, 2));

    dispatch<PutSubtask>({
      type: 'PUT_SUBTASK',
      payload: putSubtaskDto,
    });
  }, [dispatch, subtask]);

  const onEditModeSwitch = useCallback(() => {
    setIsEditMode(!isEditMode);
  }, [isEditMode]);

  const onSubtaskStateChoose = useCallback((taskState: TaskState) => {
    console.log('Subtask state submit');
    console.log(JSON.stringify(taskState, null, 2));

    dispatch<PutSubtaskState>({
      type: 'PUT_SUBTASK_STATE',
      payload: {
        subtaskId: subtask.id,
        taskState,
      }
    })
  }, [dispatch, subtask.id]);

  const editSubtask: EditSubtaskDto = {
    name: subtask.name,
  };

  return (
    <Formik
      initialValues={editSubtask}
      enableReinitialize={true}
      validate={onValidate}
      onSubmit={onSubmit}
    >
    {({ handleSubmit }: FormikProps<EditSubtaskDto>) => (
      <>
        <TR key={`subtask/${subtask.id}`}>
          <TD>&emsp;
            {isEditMode
              ?  <>
                  <FormStringField placeholder="Ім'я субтаски" name={'name'} label="" />
                  <FormError name='name' />
                </>
              : <>{subtask.name}</>}
          </TD>
          <TD></TD>
          <TD></TD>
          <TD></TD>
          <TD>
            <FormDropdown
              name='taskState'
              placeholder="Стан завдання"
              label=""
              options={taskStates.map(x => x)}
              labels={taskStates.map(x => taskStateToText(x))}
              onOptionChoose={onSubtaskStateChoose as any}
            />
            <FormError name='taskState' />
          </TD>
          {
          <AlignCenter>
            {isEditMode
              ? <Button buttonType='button' styleType='none' onClick={() => {
                  handleSubmit();
                  onEditModeSwitch();
                }}>
                <Icon type='save' style={{width: 30, height: 30 }} />
              </Button> 
              : <Button buttonType='button' styleType='none' onClick={() => onEditModeSwitch()}>
                <Icon type='edit-pencil' style={{width: 30, height: 30 }} />
              </Button>}
          </AlignCenter>
        }
        </TR>
      </>
    )}
    </Formik>
  );
};

export default SubtaskContainer;
