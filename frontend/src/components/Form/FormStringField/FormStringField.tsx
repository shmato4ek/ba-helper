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
  isHidden?: boolean;
  editable?: boolean;
  textAreaStyle?: React.CSSProperties | undefined
}

const FormStringField: FunctionComponent<Props> = ({
  name,
  type: argType,
  placeholder,
  label,
  isHidden,
  editable,
  textAreaStyle
}: Props) => {
  return (
    <Field name={name}>
      {({ field }: FieldProps) => (
        <input
          type={isHidden ? 'password' : 'text'}
          name={name}
          placeholder={placeholder}

          style={{
            ...textAreaStyle,
            padding: 5,
            borderRadius: 4,
          }}
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
  editable: true,
};

export default FormStringField;
