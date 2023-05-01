import React from 'react';
import {
  AddFileIcon,
  DiagramIcon,
  EyeIcon, LogoIcon, ProfileIcon, RaciIcon, TextFileIcon,
} from '../../assets/icons';
import styled from 'styled-components';

const H1 = styled.h1`
  font-family: Roboto;
  font-size: 50px;
  font-weight: 500;
  line-height: 59px;
  letter-spacing: 0.065em;
`

const H2 = styled.h2`
  font-family: Roboto;
  font-size: 36px;
  font-weight: 500;
  line-height: 59px;
  letter-spacing: 0.065em;
`

interface Props {
  type:
  | 'h1'
  | 'h2';
  style?: React.CSSProperties;
  onClick?: () => any;
  children: string | React.ReactNode;
}

const Font = ({
  type,
  style,
  onClick,
  children,
 }: Props) => {
  switch (type) {
    case 'h1':
      return <H1>{children}</H1>
    case 'h2':
      return <H2>{children}</H2>
    default:
      return <></>;
  }
};

Font.defaultProps = {
  onClick: undefined,
};

export default Font;
