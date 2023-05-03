import { ErrorMessage } from 'formik';
import React from 'react';

interface InternalProps {
  children: React.ReactNode;
}

const InternalFormError = ({ children }: InternalProps) => <div>{children}</div>;

interface Props {
  name: string;
}

export const FormError = ({ name }: Props) => <ErrorMessage
  component={InternalFormError as React.FunctionComponent<{}>}
  name={name}
/>

export default FormError;
