import { ErrorMessage, Field, FieldProps } from 'formik';
import React, { useRef, useState } from 'react';
import FormError from '../FormError/FormError';

import styles from './FormDropdown.module.scss';
import Icon from '../../Icon/Icon';
import { useOutsideClick } from '../../../hooks/useOutsideClick';
import styled from 'styled-components';
import { TaskState } from '../../../store/types';

interface Props {
  name: string;
  defaultValue?: string;
  placeholder?: string;
  label?: string;
  options: number[] | string[];
  labels: string[];

  onOptionChoose?: (taskState: string | number) => any;
}

export const Container = styled.div`
  flex: 1 0 0px;
  height: 100%;
  position: relative;
  background-color: #fff;

  display: flex;
  flex-direction: column;
`;

export const FieldLabel = styled.div`
  font-weight: 600;
  font-size: 14px;

  margin-bottom: 8px;
  color: #7198ae;
`;

export const OptionsContainer = styled.div`
  flex: 1 0 0px;
  border-radius: 15px;
  margin-top: 30px;
  height: 200px;
  position: absolute;
  width: 100%;
  display: flex;
  flex-direction: column;

  z-index: 2;
`;

export const Button = styled.button`
  display: flex;
  justify-content: space-between;
  align-items: center;

  border: 1px solid #e0e6ef;

  &:hover {
    cursor: pointer;
  }
`;

export const Option = styled.button`
  background-color: #fff;
  padding: 14px 20px;
  color: #000000;

  &:hover {
    background: #f8fafc;
    cursor: pointer;
  }
`;

const FormDropdown = ({
  name,
  defaultValue,
  placeholder,
  label,
  options,
  labels,
  onOptionChoose
}: Props) => {
  const [isOpen, setIsOpen] = useState(false);
  const ref = useRef<HTMLDivElement>(null);
  useOutsideClick(ref, () => setIsOpen(false));

  return (
    <Container ref={ref}>
      {label && <FieldLabel>{label}</FieldLabel>}
      <Field name={name}>
        {({ field, form }: FieldProps) => (
          <>
            <Button
              type="button"
              onClick={() => setIsOpen(!isOpen)}
            >
              <div>
                {labels[options.findIndex((option) => option === field.value)] ||
                  labels[options.findIndex((option) => option === defaultValue)] ||
                  placeholder}
              </div>
            </Button>
            {isOpen && (
              <OptionsContainer>
                {options.map((option, optionIndex) => (
                  <Option
                    key={option}
                    onClick={() => {
                      form.setFieldValue(name, option);
                      setIsOpen(false);
                      if(onOptionChoose) onOptionChoose(option);
                    }}
                  >
                    {labels[optionIndex]}
                  </Option>
                ))}
              </OptionsContainer>
            )}
          </>
        )}
      </Field>
      <ErrorMessage
        component={FormError as React.FunctionComponent<{}>}
        name={name}
      />
    </Container>
  );
};

FormDropdown.defaultProps = {
  defaultValue: undefined,
  placeholder: undefined,
  label: undefined,
};


export default FormDropdown;
