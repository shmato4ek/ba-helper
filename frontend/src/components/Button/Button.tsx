import React, { FunctionComponent } from 'react';
import styled from 'styled-components';

interface Props {
  styleType?: 'simple' | 'corner' | 'none' | 'gray';
  buttonType: 'button' | 'reset' | 'submit';
  onClick?: () => any;
  children: string | React.ReactNode;
}

const InnerContent = styled.div`
  font-size: 32px;
  font-weight: 400;
  line-height: 24px;
  letter-spacing: 0.065em;
  text-align: left;
  text-decoration: none;
`;

const SimpleButtonStyled = styled.button`
  background-color: #008F31;
  padding: 10px;
  color: #fff;
  font-weight: bold;
  border-radius: 5px;

  &:hover {
    cursor: pointer;
  }
`;

const NoneButtonStyled = styled.button`
  &:hover {
    cursor: pointer;
  }
`;

const CornerButtonStyled = styled.button`
  padding: 10px;
  color: #215526;
  background: #fff;
  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);
  font-weight: bold;
  border-radius: 5px;

  &:hover {
    cursor: pointer;
  }
`;

const GrayButtonStyled = styled.button`
  background-color: #707070;
  padding: 10px;
  color: #fff;
  font-weight: bold;
  border-radius: 5px;

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
    case 'corner':
      return (
        <CornerButtonStyled
          type={buttonType}
          onClick={onClick}
        >
          <InnerContent>{children}</InnerContent>
        </CornerButtonStyled>
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
    case 'gray':
      return (
        <GrayButtonStyled
          type={buttonType}
          onClick={onClick}
        >
          <InnerContent>{children}</InnerContent>
        </GrayButtonStyled>
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
