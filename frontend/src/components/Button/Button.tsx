import React, { FunctionComponent } from 'react';
import styled from 'styled-components';

interface Props {
  styleType?: 'simple' | 'none';
  buttonType: 'button' | 'reset' | 'submit';
  onClick?: () => any;
  children: string | React.ReactNode;
}

const InnerContent = styled.div`
  font-family: Roboto;
  font-size: 32px;
  font-weight: 400;
  line-height: 38px;
  letter-spacing: 0.065em;
  text-align: left;
`;

const SimpleButtonStyled = styled.button`
  background-color: #008F31;
  padding: 10px;
  color: #fff;

  &:hover {
    cursor: pointer;
  }
`;

const NoneButtonStyled = styled.button`
  &:hover {
    cursor: pointer;
  }
`;

const Button: FunctionComponent<Props> = ({
  styleType = 'simple',
  buttonType,
  onClick,
  children,
}: Props) => {

  switch (styleType) {
    case 'none':
      return (
        <NoneButtonStyled
          type={buttonType}
          onClick={onClick}
        >
          <InnerContent>{children}</InnerContent>
        </NoneButtonStyled>
      );
    case 'simple':
      return (
        <SimpleButtonStyled
          type={buttonType}
          onClick={onClick}
        >
          <InnerContent>{children}</InnerContent>
        </SimpleButtonStyled>
      );

    default:
      return (
        <div>There is no such contentType as: {styleType}</div>
      );
  }
};

Button.defaultProps = {
  onClick: undefined,
};

export default Button;
