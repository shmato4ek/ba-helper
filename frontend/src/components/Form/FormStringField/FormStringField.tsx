import { ErrorMessage, Field, FieldProps } from 'formik';
import React, { FunctionComponent, useState } from 'react';
import Icon from '../../Icon/Icon';
import FormError from '../FormError/FormError';

interface Props {
  name: string;
  type?: 'button'
  | 'checkbox'
  | 'color'
  | 'date'
  | 'datetime-local'
  | 'email'
  | 'file'
  | 'hidden'
  | 'image'
  | 'month'
  | 'number'
  | 'password'
  | 'radio'
  | 'range'
  | 'reset'
  | 'search'
  | 'submit'
  | 'tel'
  | 'text'
  | 'time'
  | 'url'
  | 'week';
  placeholder: string;
  label?: string;
  isHideIconShown?: boolean;
  editable?: boolean;
}

const FormStringField: FunctionComponent<Props> = ({
  name,
  type: argType,
  placeholder,
  label,
  isHideIconShown,
  editable,
}: Props) => {
  const [isHidden, setIsHidden] = useState(true);

  let type: string = argType || 'text';
  if (isHideIconShown && (type === 'text' || type === 'password')) {
    if (isHidden) type = 'password';
    else type = 'text';
  }

  return (
    <Field name={name}>
      {({ field }: FieldProps) => (
        <input
          type={type}
          name={name}
          placeholder={placeholder}
          // className={`${styles.field} ${!editable && styles.uneditable}`}
          // For nullable values in db, so that react won't yell at us for using null value in html input
          value={field.value ?? ''}
          onChange={field.onChange}
          onBlur={field.onBlur}
          readOnly={!editable}
        />
      )}
    </Field>
  );
};

FormStringField.defaultProps = {
  type: 'text',
  label: undefined,
  isHideIconShown: false,
  editable: true,
};

export default FormStringField;
