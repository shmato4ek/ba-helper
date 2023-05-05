import { ErrorMessage, Field, FieldProps } from 'formik';
import React, { FunctionComponent } from 'react';
import styled from 'styled-components';

import FormError from '../FormError/FormError';

interface Props {
  name: string;
  placeholder: string;
  label?: string;
  textAreaStyle?: React.CSSProperties | undefined
}

export const Container = styled.div`
  flex: 1 0 0px;

  max-width: 100%;

  display: flex;
  flex-direction: column;

  position: relative;
`;

export const FieldContainer = styled.div`
  display: flex;
  position: relative;
`

const FormTextareaField: FunctionComponent<Props> = ({
  name,
  placeholder,
  label,
  textAreaStyle
}: Props) => (
  <Container>
    {label && <p>{label}</p>}
    <Field name={name}>
      {({ field, form }: FieldProps) => (
        <FieldContainer>
          <textarea
            name={name}
            placeholder={placeholder}
            style={textAreaStyle}
            // className={styles.field}
            value={field.value}
            onChange={field.onChange}
            onBlur={field.onBlur}
          />
        </FieldContainer>
      )}
    </Field>
  </Container>
);

FormTextareaField.defaultProps = {
  label: undefined,
};

export default FormTextareaField;
